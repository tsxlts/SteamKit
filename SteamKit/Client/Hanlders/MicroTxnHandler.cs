using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;
using SteamKit.Client.Internal;
using SteamKit.Client.Internal.Msg;
using SteamKit.Client.Model;

namespace SteamKit.Client.Hanlders
{
    /// <summary>
    /// 
    /// </summary>
    public class MicroTxnHandler : MessageHandler
    {
        private readonly IDictionary<EMsg, Func<IServerMsg, Task>> msgHandler;

        private readonly ConcurrentDictionary<ulong, MicroTxnAuthRequest> clientMicroTxnAuthResponse;
        private readonly AutoReleaseAsyncJobCollection<JobKey> asyncJobs;

        /// <summary>
        /// 
        /// </summary>
        internal MicroTxnHandler() : base()
        {
            msgHandler = new Dictionary<EMsg, Func<IServerMsg, Task>>
            {
                { EMsg.ClientMicroTxnAuthRequest, HandleClientMicroTxnAuthRequest },
                { EMsg.ClientMicroTxnAuthorizeResponse, HandleClientMicroTxnAuthorizeResponse }
            };

            clientMicroTxnAuthResponse = new ConcurrentDictionary<ulong, MicroTxnAuthRequest>();
            asyncJobs = new AutoReleaseAsyncJobCollection<JobKey>(c => c.JobId);
        }

        /// <summary>
        /// 获取商店购买订单交易授权请求数据
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<MicroTxnAuthRequest?> QueryMicroTxnAuthRequestAsync(ulong orderId, CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!clientMicroTxnAuthResponse.TryRemove(orderId, out var txnAuthResponse))
                {
                    await Task.Delay(100);
                    continue;
                }

                return txnAuthResponse;
            }

            return null;
        }

        /// <summary>
        /// 订单支付授权
        /// </summary>
        /// <param name="transId">支付交易Id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<MicroTxnAuthorizeResponse?> ClientMicroTxnAuthorizeAsync(ulong transId, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(Client);

            var taskMsgType = EMsg.ClientMicroTxnAuthorizeResponse;
            var jobId = Client.GetJobId();

            var jobKey = new JobKey(taskMsgType, jobId);
            using (var taskReleaser = asyncJobs.AddOrUpdate<MicroTxnAuthorizeResponse>(jobKey, cancellationToken))
            {
                var task = taskReleaser.Job!;

                using var message = new ClientByteBufferMsg(EMsg.ClientMicroTxnAuthorize);
                message.SourceJobID = jobId;
                message.Writer.Write(transId);
                message.Writer.Write(1);
                await Client.SendAsync(message, cancellationToken).ConfigureAwait(false);

                var resultData = await task.ConfigureAwait(false);
                return resultData;
            }
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

        private Task HandleClientMicroTxnAuthRequest(IServerMsg packetMsg)
        {
            var msg = new ServerExtendedMsg(packetMsg!.MsgType, packetMsg.GetData());

            var messageObject = new MessageObject();
            var success = messageObject.ReadFromStream(new MemoryStream(msg.GetBody()));

            var txnAuthResponse = messageObject.KeyValues.ToObject().Value<JToken>(messageObject.KeyValues.Name!)!.ToObject<MicroTxnAuthRequest>()!;

            this.clientMicroTxnAuthResponse.AddOrUpdate(txnAuthResponse.OrderID, txnAuthResponse, (key, value) => txnAuthResponse);

            return Task.CompletedTask;
        }

        private Task HandleClientMicroTxnAuthorizeResponse(IServerMsg packetMsg)
        {
            var jobKey = new JobKey(EMsg.ClientMicroTxnAuthorizeResponse, packetMsg.JobID);
            try
            {
                using var msg = new ServerByteBufferMsg(packetMsg);
                var result = msg.Reader.ReadUInt32();
                var data = msg.Reader.ReadBytes((int)(msg.Reader.BaseStream.Length - msg.Reader.BaseStream.Position));

                asyncJobs?.SetResult(jobKey, new MicroTxnAuthorizeResponse
                {
                    Result = result,
                    Data = data,
                });
            }
            catch (Exception ex)
            {
                asyncJobs?.SetException(jobKey, ex);
            }
            return Task.CompletedTask;
        }
    }
}
