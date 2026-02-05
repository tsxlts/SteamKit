using ProtoBuf;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 服务端方法响应
    /// </summary>
    public class ServiceMethodResponse
    {
        internal ServiceMethodResponse(string rpcName, byte[] body)
        {
            RpcName = rpcName;
            Body = body;
        }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName
        {
            get { return RpcName.Split('.')[0]; }
        }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName
        {
            get { return RpcName.Substring(ServiceName.Length + 1).Split('#')[0]; }
        }

        /// <summary>
        /// 调用方法全称
        /// </summary>
        public string RpcName { get; private set; } = string.Empty;

        /// <summary>
        /// 获取消息体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetBody<T>() where T : IExtensible, new()
        {
            using MemoryStream ms = new MemoryStream(Body);
            {
                return Serializer.Deserialize<T>(ms);
            }
        }

        /// <summary>
        /// 消息体
        /// </summary>
        private byte[] Body { get; set; } = new byte[0];
    }
}
