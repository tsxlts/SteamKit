using System.Collections.Concurrent;
using SteamKit.Client.Internal;
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC;
using SteamKit.Client.Model.Proto;

namespace SteamKit.Client.Hanlders
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SteamGameCoordinator : MessageHandler
    {
        private readonly IDictionary<EMsg, Func<IServerMsg, Task>> msgHandler;
        private readonly ConcurrentDictionary<(uint, uint), MessageAsyncCallbackHandler<GCMessageCallback>> gcMessageCallbacks;

        /// <summary>
        /// 
        /// </summary>
        internal SteamGameCoordinator() : base()
        {
            msgHandler = new Dictionary<EMsg, Func<IServerMsg, Task>>
            {
                { EMsg.ClientFromGC, HandleGCMsg },
            };
            gcMessageCallbacks = new ConcurrentDictionary<(uint, uint), MessageAsyncCallbackHandler<GCMessageCallback>>();
        }

        /// <summary>
        /// 注册消息回调事件
        /// </summary>
        /// <typeparam name="MsgType"></typeparam>
        /// <param name="appId">AppId</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="callback">回调事件</param>
        /// <returns></returns>
        public SteamGameCoordinator RegistCallback<MsgType>(uint appId, MsgType msgType, AsyncEventHandler<GCMessageCallback> callback) where MsgType : struct, Enum
        {
            gcMessageCallbacks.AddOrUpdate((appId, (uint)(int)Enum.ToObject(typeof(MsgType), msgType)), key => new MessageAsyncCallbackHandler<GCMessageCallback>(callback), (key, value) =>
            {
                value.Callback += callback;
                return value;
            });
            return this;
        }

        /// <summary>
        /// 移除消息回调事件
        /// </summary>
        /// <typeparam name="MsgType"></typeparam>
        /// <param name="appId">AppId</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="callback">回调事件</param>
        /// <returns></returns>
        public SteamGameCoordinator RemoveCallback<MsgType>(uint appId, MsgType msgType, AsyncEventHandler<GCMessageCallback> callback) where MsgType : struct, Enum
        {
            gcMessageCallbacks.AddOrUpdate((appId, (uint)(int)Enum.ToObject(typeof(MsgType), msgType)), key => new MessageAsyncCallbackHandler<GCMessageCallback>(), (key, value) =>
            {
                value.Callback -= callback;
                return value;
            });
            return this;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="gcMsg">gcMsg</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task SendAsync(uint appId, IGCClientMsg gcMsg, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var clientMsg = new ClientProtoBufMsg<CMsgGCClient>(EMsg.ClientToGC)
            {
                Body = new CMsgGCClient
                {
                    appid = appId,
                    msgtype = MsgUtil.MakeGCMsg(gcMsg.MsgType, gcMsg.IsProto()),
                    payload = gcMsg.Serialize(),
                }
            };
            clientMsg.Header.Proto.routing_appid = appId;

            await Client.SendAsync(clientMsg, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="gcMsg">gcMsg</param>
        public void Send(uint appId, IGCClientMsg gcMsg)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var clientMsg = new ClientProtoBufMsg<CMsgGCClient>(EMsg.ClientToGC)
            {
                Body = new CMsgGCClient
                {
                    appid = appId,
                    msgtype = MsgUtil.MakeGCMsg(gcMsg.MsgType, gcMsg.IsProto()),
                    payload = gcMsg.Serialize(),
                }
            };
            clientMsg.Header.Proto.routing_appid = appId;

            Client.Send(clientMsg);
        }

        /// <summary>
        /// 获取当前玩家数量
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<CMsgDPGetNumberOfCurrentPlayersResponse?> GetNumberOfCurrentPlayersAsync(uint appId, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var response = await Client.SendAsync<CMsgDPGetNumberOfCurrentPlayers, CMsgDPGetNumberOfCurrentPlayersResponse>(EMsg.ClientGetNumberOfCurrentPlayersDP, new CMsgDPGetNumberOfCurrentPlayers
            {
                appid = appId,
            }, cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetMsg"></param>
        protected internal override Task HandleMsgAsync(IServerMsg packetMsg)
        {
            if (msgHandler.TryGetValue(packetMsg.MsgType, out var handlerFunc))
            {
                return handlerFunc(packetMsg);
            }

            return Task.CompletedTask;
        }

        private Task HandleGCMsg(IServerMsg packetMsg)
        {
            var msg = new ServerProtoBufMsg<CMsgGCClient>(packetMsg);
            var gcMsg = MsgConvert.DeserializeGCServerMsg(msg.Body)!;

            uint appid = msg.Body.appid;
            uint gcMsgType = MsgUtil.GetGCMsg(msg.Body.msgtype);
            string gcMsgName = EMsgExtensions.GetGCMessageName(gcMsgType, appid, out var _);

            Client?.Logger?.LogDebug("Received gcMessage, jobId: {0}, gcJobId: {1}, appId: {2}, gcMsgType: {3}({4})", msg.JobID, gcMsg.JobID, appid, gcMsgType, gcMsgName);

            if (gcMessageCallbacks.TryGetValue((appid, gcMsgType), out var gcCallback))
            {
                /*
                return gcCallback.InvokeAsync(this, new GCMessageCallback
                {
                    PacketResult = MsgConvert.DeserializeGCServerMsg(msg.Body)
                }, Client?.Logger);
                */
                gcCallback.Invoke(this, new GCMessageCallback
                {
                    PacketResult = gcMsg
                }, Client?.Logger);
            }

            return Task.CompletedTask;
        }
    }
}
