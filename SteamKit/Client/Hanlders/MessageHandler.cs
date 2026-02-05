using SteamKit.Client.Model;

namespace SteamKit.Client.Hanlders
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MessageHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageHandler()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected SteamClient? Client { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        protected internal virtual void Setup(SteamClient client)
        {
            this.Client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetMsg"></param>
        protected internal abstract Task HandleMsgAsync(IServerMsg packetMsg);
    }
}
