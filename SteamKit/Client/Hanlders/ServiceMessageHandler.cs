using SteamKit.Client.Internal;
using SteamKit.Client.Internal.Msg;
using SteamKit.Client.Model;

namespace SteamKit.Client.Hanlders
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceMessageHandler : MessageHandler
    {
        private readonly IDictionary<EMsg, Func<IServerMsg, Task>> msgHandler;

        private readonly MessageAsyncCallbackHandler<ServiceMethodNotification> serviceMethodNotificationCallback;
        private readonly MessageAsyncCallbackHandler<ServiceMethodResponse> serviceMethodResponseCallback;

        /// <summary>
        /// 
        /// </summary>
        internal ServiceMessageHandler() : base()
        {
            msgHandler = new Dictionary<EMsg, Func<IServerMsg, Task>>
            {
                { EMsg.ServiceMethod, HandleNotification },
                { EMsg.ServiceMethodResponse, HandleResponse }
            };

            serviceMethodNotificationCallback = new MessageAsyncCallbackHandler<ServiceMethodNotification>();
            serviceMethodResponseCallback = new MessageAsyncCallbackHandler<ServiceMethodResponse>();
        }

        /// <summary>
        /// 注册服务端方法响应事件
        /// <para>
        /// <see cref="SteamClient.ServiceMethodCallAsync{TServer, TResponse}(System.Linq.Expressions.Expression{Func{TServer, TResponse}}, uint, CancellationToken)"/>
        /// </para>
        /// <para>
        /// <see cref="SteamClient.ServiceMethodCallAsync{TRequest, TResponse}(string, string, TRequest, uint, CancellationToken)"/>
        /// </para>
        /// </summary>
        /// <param name="callback">回调事件</param>
        /// <returns></returns>
        public ServiceMessageHandler RegistServiceResponse(AsyncEventHandler<ServiceMethodResponse> callback)
        {
            serviceMethodResponseCallback.Callback += callback;
            return this;
        }

        /// <summary>
        /// 注册服务端方法通知事件
        /// <para>
        /// <see cref="SteamClient.ServiceNotificationCallAsync{TRequest}(string, string, TRequest, uint, CancellationToken)"/>
        /// </para>
        /// </summary>
        /// <param name="callback">回调事件</param>
        /// <returns></returns>
        public ServiceMessageHandler RegistServiceNotification(AsyncEventHandler<ServiceMethodNotification> callback)
        {
            serviceMethodNotificationCallback.Callback += callback;
            return this;
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

        private Task HandleResponse(IServerMsg packetMsg)
        {
            var serverMsg = new ServerProtoBufMsg(packetMsg.MsgType, packetMsg.GetData());
            var jobName = serverMsg.Header.Proto.target_job_name;
            if (string.IsNullOrEmpty(jobName))
            {
                return Task.CompletedTask;
            }

            byte[] body = new byte[serverMsg.Data.Length - serverMsg.BodyOffset];
            if (body.Length > 0)
            {
                Array.Copy(serverMsg.Data, serverMsg.BodyOffset, body, 0, body.Length);
            }
            ServiceMethodResponse notification = new ServiceMethodResponse(jobName, body);
            return serviceMethodResponseCallback.InvokeAsync(this, notification, Client?.Logger);
        }

        private Task HandleNotification(IServerMsg packetMsg)
        {
            var serverMsg = new ServerProtoBufMsg(packetMsg.MsgType, packetMsg.GetData());
            var jobName = serverMsg.Header.Proto.target_job_name;
            if (string.IsNullOrEmpty(jobName))
            {
                return Task.CompletedTask;
            }

            byte[] body = new byte[serverMsg.Data.Length - serverMsg.BodyOffset];
            if (body.Length > 0)
            {
                Array.Copy(serverMsg.Data, serverMsg.BodyOffset, body, 0, body.Length);
            }
            ServiceMethodNotification notification = new ServiceMethodNotification(jobName, body);
            return serviceMethodNotificationCallback.InvokeAsync(this, notification, Client?.Logger);
        }
    }
}
