using SteamKit.Client.Model;
using SteamKit.Client.Model.GC;

namespace SteamKit.Client.Internal.Msg
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="THeader"></typeparam>
    public abstract class GCClientBaseMsg<THeader> : IGCClientMsg where THeader : ISteamSerializable, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool IsProto();

        /// <summary>
        /// 
        /// </summary>
        public uint MsgType { get; }

        /// <summary>
        /// 
        /// </summary>
        public abstract ulong TargetJobID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract ulong SourceJobID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public THeader Header { get; }

        /// <summary>
        /// 
        /// </summary>
        public MemoryStream Payload { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="payloadReserve">The number of bytes to initialize the payload capacity to.</param>
        public GCClientBaseMsg(uint msgType, int payloadReserve = 0)
        {
            MsgType = msgType;
            Header = new THeader();
            Payload = new MemoryStream(payloadReserve);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract byte[] Serialize();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public abstract void Deserialize(byte[] data);
    }
}
