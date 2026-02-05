using SteamKit.Internal;

namespace SteamKit
{
    /// <summary>
    /// MessageObject
    /// </summary>
    public class MessageObject
    {
        /// <summary>
        /// 
        /// </summary>
        public KeyValue KeyValues { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public MessageObject() : this("MessageObject")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public MessageObject(string root)
        {
            ArgumentNullException.ThrowIfNull(root);
            KeyValues = new KeyValue(root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public bool ReadFromStream(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);
            return KeyValues.TryReadAsBinary(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        public void WriteToStream(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            KeyValues.Save(stream, true);
        }
    }
}
