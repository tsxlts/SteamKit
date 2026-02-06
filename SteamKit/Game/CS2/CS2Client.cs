using System.Collections.Concurrent;
using System.Text;
using ProtoBuf;
using SteamKit.Client;
using SteamKit.Client.Internal;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC;
using SteamKit.Client.Model.GC.CS2;
using SteamKit.Exceptions;
using SteamKit.Game.CS2.Models;
using SteamKit.Internal;
using static SteamKit.Client.Model.GC.CS2.CEconItemPreviewDataBlock;
using static SteamKit.SteamEnum;

namespace SteamKit.Game.CS2
{
    /// <summary>
    /// CS2Client
    /// </summary>
    public class CS2Client : GameClient
    {
        /// <summary>
        /// 库存变化事件
        /// </summary>
        /// <param name="arg"></param>
        public delegate void InventoryChangedEventHandler(IEnumerable<Inventory> arg);

        private const int CSOEconItemTypeId = 1;
        private const int CSOAccountItemPersonalStoreTypeId = 6;

        private readonly List<Inventory> inventories;
        private readonly AccountItemPersonalStore personalStore;
        private readonly ConcurrentDictionary<ESOMsg, InventoryChangedEventHandler> itemHandleCallbasks;
        private readonly AutoReleaseAsyncJobCollection<JobKey> asyncJobs;
        private readonly IDictionary<object, AsyncLock> locks;
        private readonly AsyncLock soMsgHandleLock;
        private readonly AsyncLock personalStoreHandleLock;

        private readonly LoginGameJob loginGameJob;

        private GCClientLauncherType clientLauncherType;
        private uint playerCurrency = 0;
        private ulong soVersion = 0;

        private EventHandler<CMsgConnectionStatus>? gcConnectionStatusCallback;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="version">
        /// 游戏版本
        /// 游戏目录/game/csgo/steam.inf
        /// </param>
        /// <param name="buildId">
        /// 生成版本Id
        /// Steam目录/steamapps/appmanifest_*.acf
        /// </param>
        public CS2Client(uint version, uint buildId) : base(Game.AppId.CS2, version, buildId)
        {
            inventories = new List<Inventory>();
            personalStore = new AccountItemPersonalStore();

            itemHandleCallbasks = new ConcurrentDictionary<ESOMsg, InventoryChangedEventHandler>();

            asyncJobs = new AutoReleaseAsyncJobCollection<JobKey>(c => c.JobId);

            locks = new Dictionary<object, AsyncLock>();
            foreach (var msg in Enum.GetValues<ECsgoGCMsg>())
            {
                locks.Add(msg, new AsyncLock());
            }
            foreach (var msg in Enum.GetValues<EGCItemMsg>())
            {
                locks.Add(msg, new AsyncLock());
            }

            soMsgHandleLock = new AsyncLock();
            personalStoreHandleLock = new AsyncLock();

            clientLauncherType = GCClientLauncherType.GCClientLauncherType_DEFAULT;
            loginGameJob = new LoginGameJob();

            RegistGCCallback(ECsgoGCMsg.k_EMsgGCCStrike15_v2_ClientLogonFatalError, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgGCCStrike15_v2_ClientLogonFatalError>(response.PacketResult!);
                loginGameJob.Task?.SetResult(new LoginGameResponse
                {
                    Success = false,
                    Error = $"Game loading failed, ErrorCode:{msg.Body.errorcode}"
                });

                return Task.CompletedTask;
            });
            RegistGCCallback(EGCBaseClientMsg.k_EMsgGCClientWelcome, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgClientWelcome>(response.PacketResult!);
                loginGameJob.Task?.SetResult(new LoginGameResponse
                {
                    Success = true,
                    Error = null
                });

                // var g = ProtoBuf.Serializer.Deserialize<CMsgCStrike15Welcome>(new MemoryStream(msg.Body.game_data));
                // var g2 = ProtoBuf.Serializer.Deserialize<CMsgGCCStrike15_v2_MatchmakingGC2ClientHello>(new MemoryStream(msg.Body.game_data2));
                HandleClientWelcome(msg.Body);

                return Task.CompletedTask;
            });

            RegistGCCallback(EGCBaseClientMsg.k_EMsgGCClientConnectionStatus, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgConnectionStatus>(response.PacketResult!);
                var connectionStatus = msg.Body;

                Logger?.LogDebug($"Game Connection status: {connectionStatus.status}");
                Logger?.LogInformation($"Game Connection status: {connectionStatus.status}");

                if (connectionStatus.status != GCConnectionStatus.GCConnectionStatus_HAVE_SESSION && GameLoaded)
                {
                    GameExited();
                }

                gcConnectionStatusCallback?.Invoke(this, connectionStatus);

                return Task.CompletedTask;
            });

            RegistGCCallback(ESOMsg.k_ESOMsg_Create, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgSOSingleObject>(response.PacketResult!);
                HandleSOSingleObject(ESOMsg.k_ESOMsg_Create, msg.Body);

                return Task.CompletedTask;
            });
            RegistGCCallback(ESOMsg.k_ESOMsg_Update, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgSOSingleObject>(response.PacketResult!);
                HandleSOSingleObject(ESOMsg.k_ESOMsg_Update, msg.Body);

                return Task.CompletedTask;
            });
            RegistGCCallback(ESOMsg.k_ESOMsg_Destroy, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgSOSingleObject>(response.PacketResult!);
                HandleSOSingleObject(ESOMsg.k_ESOMsg_Destroy, msg.Body);

                return Task.CompletedTask;
            });
            RegistGCCallback(ESOMsg.k_ESOMsg_UpdateMultiple, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgSOMultipleObjects>(response.PacketResult!);
                HandleSOMultipleObjects(msg.Body);

                return Task.CompletedTask;
            });
            RegistGCCallback(ECsgoGCMsg.k_EMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse, (sender, response) =>
            {
                var jobKey = new JobKey(ECsgoGCMsg.k_EMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse, 0);
                try
                {
                    var msg = new GCServerProtoBufMsg<CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse>(response.PacketResult!);
                    var result = msg.Body.iteminfo;
                    var assetId = result?.itemid ?? 0;
                    jobKey = new JobKey(ECsgoGCMsg.k_EMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse, assetId);

                    asyncJobs.SetResult(jobKey, result);
                }
                catch (Exception ex)
                {
                    asyncJobs.SetException(jobKey, ex);
                }

                return Task.CompletedTask;
            });

            RegistGCCallback(EGCItemMsg.k_EMsgGCItemCustomizationNotification, (sender, response) =>
            {
                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, 0);
                try
                {
                    var msg = new GCServerProtoBufMsg<CMsgGCItemCustomizationNotification>(response.PacketResult!);
                    jobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, msg.Body.request);

                    var notificationMsg = (EGCItemCustomizationNotification)msg.Body.request;

                    asyncJobs.SetResult(jobKey, msg.Body);
                }
                catch (Exception ex)
                {
                    asyncJobs.SetException(jobKey, ex);
                }

                return Task.CompletedTask;
            });

            RegistGCCallback(EGCItemMsg.k_EMsgGCCraftResponse, (sender, response) =>
            {
                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCCraftResponse);
                try
                {
                    var msg = new GCServerMsg<CMsgCraftingResponse>(response.PacketResult!);
                    asyncJobs.SetResult(jobKey, msg.Body);
                }
                catch (Exception ex)
                {
                    asyncJobs.SetException(jobKey, ex);
                }

                return Task.CompletedTask;
            });
            RegistGCCallback(EGCItemMsg.k_EMsgGCUnlockCrateResponse, (sender, response) =>
            {
                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCUnlockCrateResponse);
                try
                {
                    var msg = new GCServerMsg<CMsgClientToGCUnlockCrateResponse>(response.PacketResult!);
                    asyncJobs.SetResult(jobKey, msg.Body);
                }
                catch (Exception ex)
                {
                    asyncJobs.SetException(jobKey, ex);
                }

                return Task.CompletedTask;
            });

            RegistGCCallback(EGCItemMsg.k_EMsgGCStoreGetUserDataResponse, (sender, response) =>
            {
                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCStoreGetUserDataResponse);
                try
                {
                    var msg = new GCServerProtoBufMsg<CMsgStoreGetUserDataResponse>(response.PacketResult!);
                    asyncJobs.SetResult(jobKey, msg.Body);
                }
                catch (Exception ex)
                {
                    asyncJobs.SetException(jobKey, ex);
                }

                return Task.CompletedTask;
            });
            RegistGCCallback(EGCItemMsg.k_EMsgGCStorePurchaseInitResponse, (sender, response) =>
            {
                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCStorePurchaseInitResponse, response.PacketResult!.JobID);
                try
                {
                    var msg = new GCServerProtoBufMsg<CMsgGCStorePurchaseInitResponse>(response.PacketResult!);
                    asyncJobs.SetResult(jobKey, msg.Body);
                }
                catch (Exception ex)
                {
                    asyncJobs.SetException(jobKey, ex);
                }

                return Task.CompletedTask;
            });
            RegistGCCallback(EGCItemMsg.k_EMsgGCStorePurchaseFinalizeResponse, (sender, response) =>
            {
                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCStorePurchaseFinalizeResponse, response.PacketResult!.JobID);
                try
                {
                    var msg = new GCServerProtoBufMsg<CMsgGCStorePurchaseFinalizeResponse>(response.PacketResult!);
                    asyncJobs.SetResult(jobKey, msg.Body);
                }
                catch (Exception ex)
                {
                    asyncJobs.SetException(jobKey, ex);
                }

                return Task.CompletedTask;
            });
            RegistGCCallback(EGCItemMsg.k_EMsgGCStorePurchaseCancelResponse, (sender, response) =>
            {
                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCStorePurchaseCancelResponse, response.PacketResult!.JobID);
                try
                {
                    var msg = new GCServerProtoBufMsg<CMsgGCStorePurchaseCancelResponse>(response.PacketResult!);
                    asyncJobs.SetResult(jobKey, msg.Body);
                }
                catch (Exception ex)
                {
                    asyncJobs.SetException(jobKey, ex);
                }

                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// 设置游戏客户端启动方式
        /// </summary>
        /// <param name="launcherType">启动方式</param>
        /// <returns></returns>
        public CS2Client WithClientLauncherType(GCClientLauncherType launcherType)
        {
            clientLauncherType = launcherType;
            return this;
        }

        /// <summary>
        /// 注册游戏连接状态通知回调事件
        /// </summary>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public CS2Client WithGCConnectionStatus(EventHandler<CMsgConnectionStatus> callback)
        {
            gcConnectionStatusCallback = callback;
            return this;
        }

        /// <summary>
        /// 注册获得新物品回调事件
        /// </summary>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public CS2Client RegistItemAcquired(InventoryChangedEventHandler callback)
        {
            itemHandleCallbasks.AddOrUpdate(ESOMsg.k_ESOMsg_Create, callback, (key, value) => value += callback);
            return this;
        }

        /// <summary>
        /// 注册物品更新回调事件
        /// </summary>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public CS2Client RegistItemChanged(InventoryChangedEventHandler callback)
        {
            itemHandleCallbasks.AddOrUpdate(ESOMsg.k_ESOMsg_Update, callback, (key, value) => value += callback);
            return this;
        }

        /// <summary>
        /// 注册物品被移除回调事件
        /// </summary>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public CS2Client RegistItemRemoved(InventoryChangedEventHandler callback)
        {
            itemHandleCallbasks.AddOrUpdate(ESOMsg.k_ESOMsg_Destroy, callback, (key, value) => value += callback);
            return this;
        }

        /// <summary>
        /// 等待游戏初始化完成
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public override async Task WaitInitAsync(CancellationToken cancellationToken = default)
        {
            await base.WaitInitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 刷新游戏客户端数据
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgClientWelcome> RefreshAsync(CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<CMsgClientWelcome>();

            AsyncEventHandler<GCMessageCallback> callback = (object? sender, GCMessageCallback response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgClientWelcome>(response.PacketResult!);
                tcs.SetResult(msg.Body);
                return Task.CompletedTask;
            };
            try
            {
                RegistGCCallback(EGCBaseClientMsg.k_EMsgGCClientWelcome, callback);

                var clientMsg = new GCClientProtoBufMsg<Client.Model.GC.CS2.CMsgClientHello>((uint)EGCBaseClientMsg.k_EMsgGCClientHello);
                clientMsg.Body.version = Version;
                clientMsg.Body.client_launcher = (uint)clientLauncherType;
                clientMsg.Body.steam_launcher = 0;
                clientMsg.Body.client_session_need = 0;
                await SendAsync(AppId, clientMsg);

                return await tcs.Task.WaitAsync(cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                RemoveGCCallback(EGCBaseClientMsg.k_EMsgGCClientWelcome, callback);
            }
        }

        /// <summary>
        /// 查询商店价格
        /// </summary>
        /// <param name="currency">货币类型</param>
        /// <param name="language">语言</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<StoreDataResponse?> QueryStorePricesAsync(int currency, Language language = Language.English, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCStoreGetUserData;
            var taskMsgType = EGCItemMsg.k_EMsgGCStoreGetUserDataResponse;
            var locker = locks[msgType];
            using (await locker.LockAsync(cancellationToken))
            {
                var request = new GCClientProtoBufMsg<CMsgStoreGetUserData>((uint)msgType, 64);
                request.Body.currency = currency;

                var jobKey = new JobKey(taskMsgType);
                using (var taskReleaser = asyncJobs.AddOrUpdate<CMsgStoreGetUserDataResponse>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job!;

                    await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                    var resultData = await task.ConfigureAwait(false);
                    if (resultData == null)
                    {
                        return null;
                    }

                    var response = new StoreDataResponse(resultData, language);
                    return response;
                }
            }
        }

        /// <summary>
        /// 商店购买初始化数据
        /// </summary>
        /// <param name="defIndex">物品Id</param>
        /// <param name="quantity">购买数量</param>
        /// <param name="localCurrencyCost">当前货币下物品总价格</param>
        /// <param name="currency">货币类型</param>
        /// <param name="supplementalData">额外数据</param>
        /// <param name="purchaseType">购买类型</param>
        /// <param name="language">语言</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCStorePurchaseInitResponse?> InitStorePurchaseAsync(uint defIndex, uint quantity, ulong localCurrencyCost, int currency, ulong supplementalData = 0, uint purchaseType = 0, Language language = Language.English, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCStorePurchaseInit;
            var taskMsgType = EGCItemMsg.k_EMsgGCStorePurchaseInitResponse;
            var locker = locks[msgType];
            using (await locker.LockAsync(cancellationToken))
            {
                var jobId = GetJobId();
                var request = new GCClientProtoBufMsg<CMsgGCStorePurchaseInit>((uint)msgType, 64);
                request.SourceJobID = jobId;
                request.Body.country = "";
                request.Body.language = (int)language;
                request.Body.currency = currency;
                request.Body.line_items.Add(new CGCStorePurchaseInit_LineItem
                {
                    item_def_id = defIndex,
                    quantity = quantity,
                    purchase_type = purchaseType,
                    cost_in_local_currency = localCurrencyCost,
                    supplemental_data = supplementalData
                });

                var jobKey = new JobKey(taskMsgType, jobId);
                using (var taskReleaser = asyncJobs.TryAdd<CMsgGCStorePurchaseInitResponse>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job;
                    if (task == null)
                    {
                        throw new ClientBusyException("你已经有一个商店购买任务正在进行中, 请等待任务结束后再进行下一个购买");
                    }

                    await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                    var resultData = await task.ConfigureAwait(false);
                    return resultData;
                }
            }
        }

        /// <summary>
        /// 完成商店购买
        /// </summary>
        /// <param name="txnId">订单编号</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCStorePurchaseFinalizeResponse?> FinalizeStorePurchaseAsync(ulong txnId, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCStorePurchaseFinalize;
            var taskMsgType = EGCItemMsg.k_EMsgGCStorePurchaseFinalizeResponse;
            var locker = locks[msgType];
            using (await locker.LockAsync(cancellationToken))
            {
                var jobId = GetJobId();
                var request = new GCClientProtoBufMsg<CMsgGCStorePurchaseFinalize>((uint)msgType, 64);
                request.SourceJobID = jobId;
                request.Body.txn_id = txnId;

                var jobKey = new JobKey(taskMsgType, jobId);
                using (var taskReleaser = asyncJobs.TryAdd<CMsgGCStorePurchaseFinalizeResponse>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job;
                    if (task == null)
                    {
                        throw new ClientBusyException("你已经有一个商店购买任务正在进行中, 请等待任务结束后再进行下一个购买");
                    }

                    await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                    var resultData = await task.ConfigureAwait(false);
                    return resultData;
                }
            }
        }

        /// <summary>
        /// 取消商店购买
        /// </summary>
        /// <param name="txnId">订单编号</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCStorePurchaseCancelResponse?> CancelStorePurchaseAsync(ulong txnId, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCStorePurchaseCancel;
            var taskMsgType = EGCItemMsg.k_EMsgGCStorePurchaseCancelResponse;
            var locker = locks[msgType];
            using (await locker.LockAsync(cancellationToken))
            {
                var jobId = GetJobId();
                var request = new GCClientProtoBufMsg<CMsgGCStorePurchaseCancel>((uint)msgType, 64);
                request.SourceJobID = jobId;
                request.Body.txn_id = txnId;

                var jobKey = new JobKey(taskMsgType, jobId);
                using (var taskReleaser = asyncJobs.TryAdd<CMsgGCStorePurchaseCancelResponse>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job;
                    if (task == null)
                    {
                        throw new ClientBusyException("你已经有一个商店购买任务正在进行中, 请等待任务结束后再进行下一个购买");
                    }

                    await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                    var resultData = await task.ConfigureAwait(false);
                    return resultData;
                }
            }
        }

        /// <summary>
        /// 检视
        /// 无法并行执行
        /// </summary>
        /// <param name="inspectLink">检视链接</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<CEconItemPreviewDataBlock?> InspectAsync(string inspectLink, CancellationToken cancellationToken)
        {
            Extensions.GetInspectParameter(inspectLink,
                param_s: out var param_s,
                param_m: out var param_m,
                param_a: out var param_a,
                param_d: out var param_d);

            return await InspectAsync(s: param_s, m: param_m, a: param_a, d: param_d, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 检视
        /// 无法并行执行
        /// </summary>
        /// <param name="s">
        /// 检视参数S
        /// <para>SteamId</para>
        /// <para>与参数M不可同时为空</para>
        /// </param>
        /// <param name="m">
        /// 检视参数M
        /// <para>市场商品Id</para>
        /// <para>与参数S不可同时为空</para>
        /// </param>
        /// <param name="a">
        /// 检视参数A
        /// <para>资产Id</para>
        /// </param>
        /// <param name="d">
        /// 检视参数D
        /// </param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CEconItemPreviewDataBlock?> InspectAsync(ulong s, ulong m, ulong a, ulong d, CancellationToken cancellationToken)
        {
            var msgType = ECsgoGCMsg.k_EMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequest;
            var taskMsgType = ECsgoGCMsg.k_EMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse;
            var inspectLocker = locks[msgType];

            var previewDataRequest = new GCClientProtoBufMsg<CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequest>((uint)msgType, 64);
            previewDataRequest.Body.param_a = a;
            previewDataRequest.Body.param_d = d;
            if (s > 0)
            {
                previewDataRequest.Body.param_s = s;
            }
            else
            {
                previewDataRequest.Body.param_m = m;
            }

            using (await inspectLocker.LockAsync(cancellationToken))
            {
                ulong assetId = previewDataRequest.Body.param_a;
                var jobKey = new JobKey(taskMsgType, assetId);
                using (var taskReleaser = asyncJobs.AddOrUpdate<CEconItemPreviewDataBlock>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job!;

                    await SendAsync(AppId, previewDataRequest, cancellationToken).ConfigureAwait(false);

                    var resultData = await task.ConfigureAwait(false);
                    return resultData;
                }
            }
        }

        /// <summary>
        /// 兑换任务奖励
        /// </summary>
        /// <param name="campaignId">活动Id</param>
        /// <param name="redeemId">兑换物品Id</param>
        /// <param name="redeemableBalance">可兑换余额</param>
        /// <param name="expectedCost">预期成本 消耗的可兑换余额</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCItemCustomizationNotification?> RedeemMissionRewardAsync(uint campaignId, uint redeemId, uint redeemableBalance, uint expectedCost, CancellationToken cancellationToken = default)
        {
            var msgType = ECsgoGCMsg.k_EMsgGCCStrike15_v2_ClientRedeemMissionReward;
            var taskMsgType = EGCItemCustomizationNotification.k_EGCItemCustomizationNotification_ClientRedeemMissionReward;
            var redeemMissionRewardLocker = locks[msgType];

            using (await redeemMissionRewardLocker.LockAsync(cancellationToken))
            {
                var redeemMissionRewardRequest = new GCClientProtoBufMsg<CMsgGCCstrike15_v2_ClientRedeemMissionReward>((uint)msgType, 64);
                redeemMissionRewardRequest.Body.campaign_id = campaignId;
                redeemMissionRewardRequest.Body.redeem_id = redeemId;
                redeemMissionRewardRequest.Body.redeemable_balance = redeemableBalance;
                redeemMissionRewardRequest.Body.expected_cost = expectedCost;
                //redeemMissionRewardRequest.Body.bid_control = 0;

                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, (uint)taskMsgType);
                using (var taskReleaser = asyncJobs.AddOrUpdate<CMsgGCItemCustomizationNotification>(jobKey, cancellationToken))
                {
                    var notifyTask = taskReleaser.Job!;

                    await SendAsync(AppId, redeemMissionRewardRequest, cancellationToken).ConfigureAwait(false);

                    var resultData = await notifyTask.ConfigureAwait(false);
                    return resultData;
                }
            }
        }

        /// <summary>
        /// 汰换合同
        /// 无法并行执行
        /// </summary>
        /// <param name="recipe">汰换类型</param>
        /// <param name="assetIds">汰换材料资产Id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgCraftingResponse?> CraftAsync(CraftingRecipe recipe, IEnumerable<ulong> assetIds, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCCraft;
            var taskMsgType = EGCItemMsg.k_EMsgGCCraftResponse;
            var craftLocker = locks[msgType];

            using (await craftLocker.LockAsync(cancellationToken))
            {
                var request = new GCClientMsg<CMsgCraftingRequest>((uint)msgType, 64);
                request.Body.recipe = recipe;
                request.Body.item_ids.AddRange(assetIds);

                var jobKey = new JobKey(taskMsgType);
                using (var taskReleaser = asyncJobs.TryAdd<CMsgCraftingResponse>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job;
                    if (task == null)
                    {
                        throw new ClientBusyException("你已经有一个汰换合同任务正在进行中, 请等待任务结束后再进行下一个汰换合同");
                    }

                    await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                    var resultData = await task.ConfigureAwait(false);
                    return resultData;
                }
            }
        }

        /// <summary>
        /// 开启箱子
        /// 无法并行执行
        /// </summary>
        /// <param name="crateId">箱子的资产Id</param>
        /// <param name="keyId">
        /// <para>钥匙的资产Id</para>
        /// <para>不需要使用钥匙传入0</para>
        /// </param>
        /// <param name="forRental">
        /// 是否租赁箱子中的物品
        /// <para>true：开启箱子,租赁箱子中的全部物品</para>
        /// <para>false：开启箱子,并随机获得箱子中的一件物品</para>
        /// </param>
        /// <param name="pointsRemaining"></param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCItemCustomizationNotification?> OpenCrateAsync(ulong crateId, ulong keyId, bool forRental = false, uint? pointsRemaining = null, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCOpenCrate;
            var taskMsgType = EGCItemMsg.k_EMsgGCUnlockCrateResponse;
            var notifyTaskMsgType = EGCItemCustomizationNotification.k_EGCItemCustomizationNotification_UnlockCrate;
            var locker = locks[msgType];

            using (await locker.LockAsync(cancellationToken))
            {
                var request = new GCClientProtoBufMsg<CMsgOpenCrate>((uint)msgType, 64);
                request.Body.tool_item_id = keyId;
                request.Body.subject_item_id = crateId;
                request.Body.for_rental = forRental;

                if (pointsRemaining.HasValue)
                {
                    request.Body.points_remaining = pointsRemaining.Value;
                }

                var jobKey = new JobKey(taskMsgType);
                using (var taskReleaser = asyncJobs.TryAdd<CMsgClientToGCUnlockCrateResponse>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job;
                    if (task == null)
                    {
                        throw new ClientBusyException("你已经有一个开启箱子任务正在进行中, 请等待任务结束后再开启下一个箱子");
                    }

                    var notifyJobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, (uint)notifyTaskMsgType);
                    using (var notifyTaskReleaser = asyncJobs.AddOrUpdate<CMsgGCItemCustomizationNotification>(notifyJobKey, cancellationToken))
                    {
                        var notifyTask = notifyTaskReleaser.Job!;

                        await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                        var result = await task.ConfigureAwait(false);
                        if (result?.result != EGCMsgResponse.k_EGCMsgResponseOK)
                        {
                            throw new ClientRequestException($"开箱请求处理失败, crateId:{crateId}, keyId:{keyId}, gcMsgResponse:{result?.result}");
                        }

                        var resultData = await notifyTask.ConfigureAwait(false);
                        return resultData;
                    }
                }
            }
        }

        /// <summary>
        /// 应用印花
        /// 无法并行执行
        /// </summary>
        /// <param name="itemId">需要应用印花的资产Id</param>
        /// <param name="stickerItemId">印花资产Id</param>
        /// <param name="stickerSlot">印花槽位</param>
        /// <param name="stickerWear">印花磨损度</param>
        /// <param name="stickerRotation">
        /// 印花旋转角度
        /// <para>-180 到 180</para>
        /// </param>
        /// <param name="stickerOffsetX">印花X坐标</param>
        /// <param name="stickerOffsetY">印花Y坐标</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCItemCustomizationNotification?> ApplyStickerAsync(ulong itemId, ulong stickerItemId, uint stickerSlot, float stickerWear, float stickerRotation, float stickerOffsetX, float stickerOffsetY, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCApplySticker;
            var taskMsgType = EGCItemCustomizationNotification.k_EGCItemCustomizationNotification_ApplySticker;
            var applyStickerLocker = locks[msgType];

            using (await applyStickerLocker.LockAsync(cancellationToken))
            {
                var applyStickerRequest = new GCClientProtoBufMsg<CMsgApplySticker>((uint)msgType, 64);
                applyStickerRequest.Body.item_item_id = itemId;
                applyStickerRequest.Body.sticker_item_id = stickerItemId;
                applyStickerRequest.Body.sticker_slot = stickerSlot;
                applyStickerRequest.Body.sticker_wear = stickerWear;
                applyStickerRequest.Body.sticker_rotation = stickerRotation;
                applyStickerRequest.Body.sticker_offset_x = stickerOffsetX;
                applyStickerRequest.Body.sticker_offset_y = stickerOffsetY;

                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, (uint)taskMsgType);
                using (var taskReleaser = asyncJobs.AddOrUpdate<CMsgGCItemCustomizationNotification>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job!;

                    await SendAsync(AppId, applyStickerRequest, cancellationToken).ConfigureAwait(false);

                    var resultData = await task.ConfigureAwait(false);
                    return resultData;
                }
            }
        }

        /// <summary>
        /// 刮去印花
        /// </summary>
        /// <param name="itemId">需要刮去印花的资产Id</param>
        /// <param name="stickerIndex">
        /// 需要刮去的印花所在位置
        /// <para>第几个印花</para>
        /// </param>
        /// <param name="currentStickerWear">
        /// 需要刮去的印花当前磨损度
        /// <para>无磨损时传入 -1</para>
        /// </param>
        /// <param name="targetStickerWear">需要刮去的印花目标磨损度</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ModifyStickerAsync(ulong itemId, uint stickerIndex, float currentStickerWear, float targetStickerWear, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCApplySticker;
            var applyStickerLocker = locks[msgType];

            using (await applyStickerLocker.LockAsync(cancellationToken))
            {
                var applyStickerRequest = new GCClientProtoBufMsg<CMsgApplySticker>((uint)msgType, 64);
                applyStickerRequest.Body.item_item_id = itemId;
                applyStickerRequest.Body.sticker_slot = stickerIndex;
                applyStickerRequest.Body.sticker_wear = currentStickerWear;
                applyStickerRequest.Body.sticker_wear_target = targetStickerWear;

                await SendAsync(AppId, applyStickerRequest, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 移除印花
        /// </summary>
        /// <param name="itemId">需要移除印花的资产Id</param>
        /// <param name="stickerIndex">
        /// 需要移除的印花所在位置
        /// <para>第几个印花</para>
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RemoveStickerAsync(ulong itemId, uint stickerIndex, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCApplySticker;
            var applyStickerLocker = locks[msgType];

            using (await applyStickerLocker.LockAsync(cancellationToken))
            {
                var applyStickerRequest = new GCClientProtoBufMsg<CMsgApplySticker>((uint)msgType, 64);
                applyStickerRequest.Body.item_item_id = itemId;
                applyStickerRequest.Body.sticker_slot = stickerIndex;
                applyStickerRequest.Body.sticker_wear = 1;
                applyStickerRequest.Body.sticker_wear_target = 1;

                await SendAsync(AppId, applyStickerRequest, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 应用挂件
        /// </summary>
        /// <param name="itemId">需要应用挂件的资产Id</param>
        /// <param name="keychainItemId">挂件资产Id</param>
        /// <param name="keychainOffsetX">挂件X坐标</param>
        /// <param name="keychainOffsetY">挂件Y坐标</param>
        /// <param name="keychainOffsetZ">挂件Z坐标</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCItemCustomizationNotification?> ApplyKeychainAsync(ulong itemId, ulong keychainItemId, float keychainOffsetX, float keychainOffsetY, float keychainOffsetZ, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCApplySticker;
            var taskMsgType = EGCItemCustomizationNotification.k_EGCItemCustomizationNotification_ApplyKeychain;
            var applyKeychainLocker = locks[msgType];

            using (await applyKeychainLocker.LockAsync(cancellationToken))
            {
                var applyKeychainRequest = new GCClientProtoBufMsg<CMsgApplySticker>((uint)msgType, 64);
                applyKeychainRequest.Body.item_item_id = itemId;
                applyKeychainRequest.Body.sticker_item_id = keychainItemId;
                applyKeychainRequest.Body.sticker_offset_x = keychainOffsetX;
                applyKeychainRequest.Body.sticker_offset_y = keychainOffsetY;
                applyKeychainRequest.Body.sticker_offset_z = keychainOffsetZ;

                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, (uint)taskMsgType);
                using (var taskReleaser = asyncJobs.AddOrUpdate<CMsgGCItemCustomizationNotification>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job!;

                    await SendAsync(AppId, applyKeychainRequest, cancellationToken).ConfigureAwait(false);

                    var resultData = await task.ConfigureAwait(false);
                    return resultData;
                }
            }
        }

        /// <summary>
        /// 应用名称标签
        /// 无法并行执行
        /// </summary>
        /// <param name="itemId">需要应用名称标签的资产Id</param>
        /// <param name="nameTagItemId">名称标签资产Id</param>
        /// <param name="name">名称</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCItemCustomizationNotification?> ApplyNameTagAsync(ulong itemId, ulong nameTagItemId, string name, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCNameItem;
            var taskMsgType = EGCItemCustomizationNotification.k_EGCItemCustomizationNotification_NameItem;
            var applyNameTagLocker = locks[msgType];

            using (await applyNameTagLocker.LockAsync(cancellationToken))
            {
                using var request = new GCClientByteBufferMsg((uint)msgType, 64);
                request.Writer.Write(nameTagItemId);
                request.Writer.Write(itemId);
                request.Writer.Write((byte)0x00);
                request.Writer.WriteCString(name);

                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, (uint)taskMsgType);
                using (var taskReleaser = asyncJobs.AddOrUpdate<CMsgGCItemCustomizationNotification>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job!;

                    await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                    var resultData = await task.ConfigureAwait(false);
                    return resultData;
                }
            }
        }

        /// <summary>
        /// 将物品存入库存存储组件
        /// 无法并行执行
        /// </summary>
        /// <param name="itemId">需要存入库存存储组件的资产Id</param>
        /// <param name="casketId">库存存储组件资产Id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCItemCustomizationNotification?> AddToCasketAsync(ulong itemId, ulong casketId, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCCasketItemAdd;
            var taskMsgType = EGCItemCustomizationNotification.k_EGCItemCustomizationNotification_CasketAdded;
            var locker = locks[msgType];

            using (await locker.LockAsync(cancellationToken))
            {
                var request = new GCClientProtoBufMsg<CMsgCasketItem>((uint)msgType, 64);
                request.Body.item_item_id = itemId;
                request.Body.casket_item_id = casketId;

                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, (uint)taskMsgType);
                using (var taskReleaser = asyncJobs.AddOrUpdate<CMsgGCItemCustomizationNotification>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job!;

                    await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                    var result = await task.ConfigureAwait(false);
                    return result;
                }
            }
        }

        /// <summary>
        /// 从库存存储组件取出物品
        /// 无法并行执行
        /// </summary>
        /// <param name="itemId">需要从库存存储组件取出的物品的资产Id</param>
        /// <param name="casketId">库存存储组件资产Id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgGCItemCustomizationNotification?> RemoveFromCasketAsync(ulong itemId, ulong casketId, CancellationToken cancellationToken = default)
        {
            var msgType = EGCItemMsg.k_EMsgGCCasketItemExtract;
            var taskMsgType = EGCItemCustomizationNotification.k_EGCItemCustomizationNotification_CasketRemoved;
            var locker = locks[msgType];

            using (await locker.LockAsync(cancellationToken))
            {
                var request = new GCClientProtoBufMsg<CMsgCasketItem>((uint)msgType, 64);
                request.Body.item_item_id = itemId;
                request.Body.casket_item_id = casketId;

                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, (uint)taskMsgType);
                using (var taskReleaser = asyncJobs.AddOrUpdate<CMsgGCItemCustomizationNotification>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job!;

                    await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                    var result = await task.ConfigureAwait(false);
                    return result;
                }
            }
        }

        /// <summary>
        /// 获取库存存储组件内物品
        /// </summary>
        /// <param name="casketId">库存存储组件资产Id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<List<Inventory>> GetCasketContentsAsync(ulong casketId, CancellationToken cancellationToken = default)
        {
            var casket = inventories.FirstOrDefault(c => c.id == casketId);
            if (casket == null)
            {
                return new List<Inventory>();
            }

            var result = inventories.Where(c => c.casket_id == casketId).ToList();
            if (result.Count == casket.casket_contained_item_count)
            {
                return result;
            }

            var msgType = EGCItemMsg.k_EMsgGCCasketItemLoadContents;
            var taskMsgType = EGCItemCustomizationNotification.k_EGCItemCustomizationNotification_CasketContents;
            var locker = locks[msgType];

            using (await locker.LockAsync(cancellationToken))
            {
                var request = new GCClientProtoBufMsg<CMsgCasketItem>((uint)msgType, 64);
                request.Body.casket_item_id = casketId;

                var jobKey = new JobKey(EGCItemMsg.k_EMsgGCItemCustomizationNotification, (uint)taskMsgType);
                using (var taskReleaser = asyncJobs.AddOrUpdate<CMsgGCItemCustomizationNotification>(jobKey, cancellationToken))
                {
                    var task = taskReleaser.Job!;

                    await SendAsync(AppId, request, cancellationToken).ConfigureAwait(false);

                    await task.ConfigureAwait(false);

                    result = inventories.Where(c => c.casket_id == casketId).ToList();
                    return result;
                }
            }
        }

        /// <summary>
        /// 获取库存
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public Task<List<Inventory>> GetInventoryAsync(CancellationToken cancellationToken = default)
        {
            var result = new List<Inventory>(inventories);
            return Task.FromResult(result);
        }

        /// <summary>
        /// AccountItemPersonalStore
        /// </summary>
        public AccountItemPersonalStore AccountItemPersonalStore => personalStore;

        /// <summary>
        /// 游戏货币类型
        /// </summary>
        public int Currency => unchecked((int)playerCurrency);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        protected async override Task<LoginGameResponse> LoadingGameInternalAsync(CancellationToken cancellationToken)
        {
            using (await loginGameJob.Lock.LockAsync(cancellationToken))
            {
                loginGameJob.Task = new AsyncJob<LoginGameResponse>(AppId, cancellationToken);

                TimerCallback timerCallback = (obj) =>
                {
                    try
                    {
                        if (!this.IsConnected())
                        {
                            loginGameJob.Task?.SetResult(new LoginGameResponse
                            {
                                Success = false,
                                Error = $"Game loading failed, Connection dropped"
                            });
                            return;
                        }

                        var clientMsg = new GCClientProtoBufMsg<Client.Model.GC.CS2.CMsgClientHello>((uint)EGCBaseClientMsg.k_EMsgGCClientHello);
                        clientMsg.Body.version = Version;
                        clientMsg.Body.client_launcher = (uint)clientLauncherType;
                        clientMsg.Body.steam_launcher = 0;
                        clientMsg.Body.client_session_need = 0;
                        Send(AppId, clientMsg);
                    }
                    catch
                    {

                    }
                };
                using (var timer = new Timer(timerCallback, this, TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(1000)))
                {
                    var result = await loginGameJob.Task.ConfigureAwait(false);
                    return result ?? new LoginGameResponse
                    {
                        Success = false,
                        Error = "Unknown",
                    };
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgClient"></param>
        private void HandleClientWelcome(CMsgClientWelcome msgClient)
        {
            this.playerCurrency = msgClient.currency;

            var soCache = msgClient.outofdate_subscribed_caches.FirstOrDefault();
            if (soCache == null)
            {
                return;
            }

            using (soMsgHandleLock.Lock(TimeSpan.FromSeconds(5)))
            {
                Interlocked.Exchange(ref soVersion, soCache.version);

                var soEconItems = soCache.objects.Where(c => c.type_id == CSOEconItemTypeId).ToList();
                if (soEconItems.Any())
                {
                    ProcessSOEconItemFromSOCache(soEconItems);
                }

                var soAccountItemPersonalStore = soCache.objects.FirstOrDefault(c => c.type_id == CSOAccountItemPersonalStoreTypeId);
                if (soAccountItemPersonalStore?.object_data?.Any() ?? false)
                {
                    ProcessSOAccountItemPersonalStore(soAccountItemPersonalStore.type_id, soAccountItemPersonalStore.object_data[0]);
                }
            }
        }

        private void HandleSOMultipleObjects(CMsgSOMultipleObjects multipleObjects)
        {
            using (soMsgHandleLock.Lock(TimeSpan.FromSeconds(5)))
            {
                if (soVersion > multipleObjects.version)
                {
                    return;
                }

                Interlocked.Exchange(ref soVersion, multipleObjects.version);

                if (!multipleObjects.objects_modified.Any())
                {
                    return;
                }

                var soEconItems = multipleObjects.objects_modified.Where(c => c.type_id == CSOEconItemTypeId).ToList();
                if (soEconItems.Any())
                {
                    ProcessSOEconItemFromSOMsg(soEconItems);
                }

                var soAccountItemPersonalStore = multipleObjects.objects_modified.FirstOrDefault(c => c.type_id == CSOAccountItemPersonalStoreTypeId);
                if (soAccountItemPersonalStore?.object_data?.Any() ?? false)
                {
                    ProcessSOAccountItemPersonalStore(soAccountItemPersonalStore.type_id, soAccountItemPersonalStore.object_data);
                }
            }
        }

        private void HandleSOSingleObject(ESOMsg msg, CMsgSOSingleObject singleObject)
        {
            using (soMsgHandleLock.Lock(TimeSpan.FromSeconds(5)))
            {
                if (soVersion > singleObject.version)
                {
                    return;
                }

                Interlocked.Exchange(ref soVersion, singleObject.version);

                switch (singleObject.type_id)
                {
                    case CSOEconItemTypeId:
                        ProcessSOEconItemFromSOMsg(msg, singleObject.type_id, singleObject.object_data);
                        break;
                }
            }
        }

        #region 库存 CSOEconItem type_id = 1
        /// <summary>
        /// 加载库存
        /// </summary>
        /// <param name="objects"></param>
        private void ProcessSOEconItemFromSOCache(List<CMsgSOCacheSubscribed.SubscribedType> objects)
        {
            List<Inventory> inventories = new List<Inventory>();
            Inventory? inventory;
            foreach (var obj in objects)
            {
                foreach (var data in obj.object_data)
                {
                    inventory = ProcessSOEconItem(obj.type_id, data);
                    if (inventory == null)
                    {
                        continue;
                    }

                    inventories.Add(inventory);
                }
            }

            this.inventories.Clear();
            this.inventories.AddRange(inventories);
        }

        /// <summary>
        /// 加载库存
        /// </summary>
        /// <param name="objects"></param>
        private void ProcessSOEconItemFromSOMsg(List<CMsgSOMultipleObjects.SingleObject> objects)
        {
            if (!objects.Any())
            {
                return;
            }

            List<Inventory> modifiedInventories = new List<Inventory>();
            Inventory? inventory;

            foreach (var obj in objects)
            {
                inventory = ProcessSOEconItem(obj.type_id, obj.object_data);
                if (inventory == null)
                {
                    continue;
                }

                modifiedInventories.Add(inventory);

                int index = inventories.FindIndex(c => c.id == inventory.id);
                if (index < 0)
                {
                    inventories.Add(inventory);
                    continue;
                }

                inventories.RemoveAt(index);
                inventories.Insert(index, inventory);
            }

            if (itemHandleCallbasks.TryGetValue(ESOMsg.k_ESOMsg_Update, out var callbask))
            {
                callbask?.Invoke(modifiedInventories);
            }

            Logger?.LogInformation("更新库存: {0}, def_index: [{1}], id: [{2}]",
                $"{ESOMsg.k_ESOMsg_UpdateMultiple}",
                string.Join(", ", modifiedInventories.Select(c => c.def_index)),
                string.Join(", ", modifiedInventories.Select(c => c.id)));
        }

        /// <summary>
        /// 加载库存
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type_id"></param>
        /// <param name="object_data"></param>
        private void ProcessSOEconItemFromSOMsg(ESOMsg msg, int type_id, byte[] object_data)
        {
            var inventory = ProcessSOEconItem(type_id, object_data);
            if (inventory == null)
            {
                return;
            }

            switch (msg)
            {
                case ESOMsg.k_ESOMsg_Create:
                case ESOMsg.k_ESOMsg_Update:
                    {
                        int index = inventories.FindIndex(c => c.id == inventory.id);
                        if (index < 0)
                        {
                            inventories.Add(inventory);
                            break;
                        }

                        inventories.RemoveAt(index);
                        inventories.Insert(index, inventory);
                    }
                    break;

                case ESOMsg.k_ESOMsg_Destroy:
                    {
                        inventories.RemoveAll(c => c.id == inventory.id);
                    }
                    break;
            }

            if (itemHandleCallbasks.TryGetValue(msg, out var callbask))
            {
                callbask?.Invoke([inventory]);
            }

            Logger?.LogInformation("更新库存: {0}, def_index: {1}, id: {2}",
                $"{msg}",
                inventory.def_index,
                inventory.id);
        }

        private Inventory? ProcessSOEconItem(int type_id, byte[] object_data)
        {
            if (type_id != CSOEconItemTypeId)
            {
                return null;
            }

            CSOEconItem csoEconItem = Serializer.Deserialize<CSOEconItem>(object_data.AsSpan());
            Inventory inventory = ProcessSOEconItem(csoEconItem);
            return inventory;
        }

        private Inventory ProcessSOEconItem(CSOEconItem econItem)
        {
            Inventory inventory = new Inventory
            {
                id = econItem.id,
                account_id = econItem.account_id,
                custom_desc = econItem.custom_desc,
                custom_name = econItem.custom_name,
                def_index = econItem.def_index,
                flags = econItem.flags,
                interior_item = econItem.interior_item,
                inventory = econItem.inventory,
                in_use = econItem.in_use,
                level = econItem.level,
                origin = econItem.origin,
                original_id = econItem.original_id,
                quality = econItem.quality,
                quantity = econItem.quantity,
                rarity = econItem.rarity,
                style = econItem.style,

                position = 0,

                paintindex = 0,
                paintseed = 0,
                paintwear = 0,
                tradable_after = 0,
                trade_protected_escrow = 0,

                kill_eater_score_type = null,
                kill_eater_value = null,

                casket_id = 0,
                quest_id = 0,
                casket_contained_item_count = 0
            };
            inventory.equipped_state.AddRange(econItem.equipped_state);
            inventory.attribute.AddRange(econItem.attribute);

            bool isNew = ((inventory.inventory >>> 30) & 1) > 0;
            inventory.position = (isNew ? 0 : inventory.inventory & 0xFFFF);

            inventory.custom_name = GetAttribute(econItem, 111, c => Encoding.UTF8.GetString(c.value_bytes.Skip(2).ToArray()), "", out var _) ?? "";

            inventory.paintindex = GetAttribute(econItem, 6, c => (uint)BitConverter.ToSingle(c.value_bytes), 0u, out var _);
            inventory.paintseed = GetAttribute(econItem, 7, c => (uint)BitConverter.ToSingle(c.value_bytes), 0u, out var _);
            inventory.paintwear = GetAttribute(econItem, 8, c => BitConverter.ToSingle(c.value_bytes), 0f, out var _);

            inventory.tradable_after = GetAttribute(econItem, 75, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _);
            inventory.trade_protected_escrow = GetAttribute(econItem, 312, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _);

            inventory.kill_eater_value = GetAttribute<uint?>(econItem, 80, c => BitConverter.ToUInt32(c.value_bytes), null, out var _);
            inventory.kill_eater_score_type = GetAttribute<uint?>(econItem, 81, c => BitConverter.ToUInt32(c.value_bytes), null, out var _);
            inventory.quest_id = GetAttribute(econItem, 168, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _);

            inventory.casket_contained_item_count = GetAttribute(econItem, 270, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _);

            ulong casketIdLow = GetAttribute(econItem, 272, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _);
            ulong casketIdHigh = GetAttribute(econItem, 273, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _);
            inventory.casket_id = (casketIdHigh << 32) | casketIdLow;

            Sticker sticker;
            for (uint i = 0; i < 5; i++)
            {
                uint defIndex = 113 + (i * 4);
                uint stickerId = GetAttribute(econItem, defIndex, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var hasSticker);
                if (!hasSticker)
                {
                    continue;
                }

                sticker = new Sticker
                {
                    sticker_id = stickerId,
                    slot = GetAttribute(econItem, 290 + i, c => BitConverter.ToUInt32(c.value_bytes), i, out var _),

                    wear = GetAttribute(econItem, 114 + (i * 4) + 0, c => BitConverter.ToSingle(c.value_bytes), 0f, out var _),
                    scale = GetAttribute(econItem, 114 + (i * 4) + 1, c => BitConverter.ToSingle(c.value_bytes), 0f, out var _),
                    rotation = GetAttribute(econItem, 114 + (i * 4) + 2, c => BitConverter.ToSingle(c.value_bytes), 0f, out var _),

                    offset_x = GetAttribute(econItem, 278 + (i * 2) + 0, c => BitConverter.ToSingle(c.value_bytes), 0f, out var _),
                    offset_y = GetAttribute(econItem, 278 + (i * 2) + 1, c => BitConverter.ToSingle(c.value_bytes), 0f, out var _),
                    offset_z = 0,

                    tint_id = GetAttribute(econItem, 233, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _),

                    pattern = 0,
                    highlight_reel = 0,
                    wrapped_sticker = 0,
                };
                inventory.stickers.Add(sticker);
            }

            uint keychainId = GetAttribute(econItem, 299, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var hasKeychain);
            if (hasKeychain)
            {
                Sticker keychain = new Sticker
                {
                    sticker_id = keychainId,

                    offset_x = GetAttribute(econItem, 300 + 0, c => BitConverter.ToSingle(c.value_bytes), 0f, out var _),
                    offset_y = GetAttribute(econItem, 300 + 1, c => BitConverter.ToSingle(c.value_bytes), 0f, out var _),
                    offset_z = GetAttribute(econItem, 300 + 2, c => BitConverter.ToSingle(c.value_bytes), 0f, out var _),

                    pattern = GetAttribute(econItem, 306, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _),
                    highlight_reel = GetAttribute(econItem, 314, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _),
                    wrapped_sticker = GetAttribute(econItem, 321, c => BitConverter.ToUInt32(c.value_bytes), 0u, out var _),

                    slot = 0,
                    wear = 0,
                    scale = 0,
                    rotation = 0,

                    tint_id = 0,
                };
                inventory.keychains.Add(keychain);
            }

            return inventory;
        }
        #endregion

        #region CSOAccountItemPersonalStore type_id = 6
        private void ProcessSOAccountItemPersonalStore(int type_id, byte[] object_data)
        {
            if (type_id != CSOAccountItemPersonalStoreTypeId)
            {
                return;
            }

            using (personalStoreHandleLock.Lock(TimeSpan.FromSeconds(5)))
            {
                CSOAccountItemPersonalStore accountItemPersonalStore = Serializer.Deserialize<CSOAccountItemPersonalStore>(object_data.AsSpan());

                personalStore.Update(accountItemPersonalStore);
            }
        }
        #endregion

        private T? GetAttribute<T>(CSOEconItem item, uint defIndex, Func<CSOEconItemAttribute, T> decode, T? defaultValue, out bool exists)
        {
            exists = SOMsgExtensions.TryGetAttribute(item.attribute, defIndex, decode, defaultValue, out var value);
            return value;
        }
    }
}
