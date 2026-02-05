namespace SteamKit.Client.Model
{

    /// <summary>
    /// 消息序列化处理
    /// </summary>
    public interface ISteamSerializable
    {
        /// <summary>
        /// 序列化消息
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream);

        /// <summary>
        /// 反序列化消息
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream);
    }
}
