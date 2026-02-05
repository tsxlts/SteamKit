using System.Collections.Concurrent;
using ProtoBuf;
using SteamKit.Client.Internal.Model;
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC;
using SteamKit.Client.Model.GC.TF2;
using SteamKit.Internal;

namespace SteamKit.Game.TF2
{
    /// <summary>
    /// TF2Client
    /// </summary>
    public class TF2Client : GameClient
    {
        private const int CSOEconItemTypeId = 1;

        /// <summary>
        /// 库存变化事件
        /// </summary>
        /// <param name="arg"></param>
        public delegate void InventoryChangedEventHandler(IEnumerable<CSOEconItem> arg);

        private readonly TaskCompletionSource initTask;
        private readonly List<CSOEconItem> inventories;

        private readonly ConcurrentDictionary<ESOMsg, InventoryChangedEventHandler> itemHandleCallbasks;
        private readonly AsyncLock soMsgHandleLock;

        private readonly LoginGameJob loginGameJob;

        private ulong soVersion = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="version">
        /// 游戏版本
        /// </param>
        /// <param name="buildId">
        /// 生成版本Id
        /// Steam目录/steamapps/appmanifest_*.acf
        /// </param>
        public TF2Client(uint version, uint buildId) : base(Game.AppId.TF2, version, buildId)
        {
            initTask = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            inventories = new List<CSOEconItem>();

            itemHandleCallbasks = new ConcurrentDictionary<ESOMsg, InventoryChangedEventHandler>();
            soMsgHandleLock = new AsyncLock();

            loginGameJob = new LoginGameJob();

            RegistGCCallback(EGCBaseClientMsg.k_EMsgGCClientWelcome, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgClientWelcome>(response.PacketResult!);
                loginGameJob.Task?.SetResult(new LoginGameResponse
                {
                    Success = true,
                    Error = null
                });

                initTask.TrySetResult();
                return Task.CompletedTask;
            });
            RegistGCCallback(EGCBaseClientMsg.k_EMsgGCClientGoodbye, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgClientGoodbye>(response.PacketResult!);

                if (GameLoaded)
                {
                    GameExited();
                }

                return Task.CompletedTask;
            });

            RegistGCCallback(ESOMsg.k_ESOMsg_CacheSubscriptionCheck, (sender, response) =>
            {
                var message = new GCClientProtoBufMsg<CMsgSOCacheSubscriptionRefresh>((uint)ESOMsg.k_ESOMsg_CacheSubscriptionRefresh, 64);
                message.Body.owner = SteamId!.Value;
                Send(AppId, message);

                return Task.CompletedTask;
            });
            RegistGCCallback(ESOMsg.k_ESOMsg_CacheSubscribed, (sender, response) =>
            {
                var msg = new GCServerProtoBufMsg<CMsgSOCacheSubscribed>(response.PacketResult!);
                HandleSOCache(msg.Body);

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
        }

        /// <summary>
        /// 注册获得新物品回调事件
        /// </summary>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public TF2Client RegistItemAcquired(InventoryChangedEventHandler callback)
        {
            itemHandleCallbasks.AddOrUpdate(ESOMsg.k_ESOMsg_Create, callback, (key, value) => value += callback);
            return this;
        }

        /// <summary>
        /// 注册物品更新回调事件
        /// </summary>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public TF2Client RegistItemChanged(InventoryChangedEventHandler callback)
        {
            itemHandleCallbasks.AddOrUpdate(ESOMsg.k_ESOMsg_Update, callback, (key, value) => value += callback);
            return this;
        }

        /// <summary>
        /// 注册物品被移除回调事件
        /// </summary>
        /// <param name="callback">Callback</param>
        /// <returns></returns>
        public TF2Client RegistItemRemoved(InventoryChangedEventHandler callback)
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
            await initTask.WaitAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取库存
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<List<CSOEconItem>> GetInventoryAsync(CancellationToken cancellationToken = default)
        {
            using (await soMsgHandleLock.LockAsync(cancellationToken))
            {
                var result = new List<CSOEconItem>(inventories);
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        protected override async Task<LoginGameResponse> LoadingGameInternalAsync(CancellationToken cancellationToken)
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

                        var clientMsg = new GCClientProtoBufMsg<Client.Model.GC.TF2.CMsgClientHello>((uint)EGCBaseClientMsg.k_EMsgGCClientHello);
                        clientMsg.Body.version = Version;
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
        /// <param name="soCache"></param>
        private void HandleSOCache(CMsgSOCacheSubscribed soCache)
        {
            using (soMsgHandleLock.Lock(TimeSpan.FromSeconds(5)))
            {
                Interlocked.Exchange(ref soVersion, soCache.version);

                var soEconItems = soCache.objects.Where(c => c.type_id == CSOEconItemTypeId).ToList();
                if (soEconItems.Any())
                {
                    ProcessSOEconItemFromSOCache(soEconItems);
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

        private void HandleSOMultipleObjects(CMsgSOMultipleObjects multipleObjects)
        {
            using (soMsgHandleLock.Lock(TimeSpan.FromSeconds(5)))
            {
                if (soVersion > multipleObjects.version)
                {
                    return;
                }

                Interlocked.Exchange(ref soVersion, multipleObjects.version);

                var soEconItems = multipleObjects.objects.Where(c => c.type_id == CSOEconItemTypeId).ToList();
                if (soEconItems.Any())
                {
                    ProcessSOEconItemFromSOMsg(soEconItems);
                }
            }
        }

        private void ProcessSOEconItemFromSOCache(List<CMsgSOCacheSubscribed.SubscribedType> objects)
        {
            CSOEconItem? csoEconItem;
            foreach (var obj in objects)
            {
                foreach (var data in obj.object_data)
                {
                    csoEconItem = ProcessSOEconItem(obj.type_id, data);
                    if (csoEconItem == null)
                    {
                        continue;
                    }

                    inventories.Add(csoEconItem);
                }
            }
        }

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
        }

        private void ProcessSOEconItemFromSOMsg(List<CMsgSOMultipleObjects.SingleObject> objects)
        {
            List<CSOEconItem> modifiedInventories = new List<CSOEconItem>();

            foreach (var obj in objects)
            {
                var inventory = ProcessSOEconItem(obj.type_id, obj.object_data);
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
        }

        private CSOEconItem? ProcessSOEconItem(int type_id, byte[] object_data)
        {
            if (type_id != CSOEconItemTypeId)
            {
                return null;
            }

            CSOEconItem csoEconItem = Serializer.Deserialize<CSOEconItem>(object_data.AsSpan());
            return csoEconItem;
        }
    }
}
