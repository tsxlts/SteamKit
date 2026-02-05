
namespace SteamKit.Model
{
    /// <summary>
    /// 应用印花对应
    /// </summary>
    public class AppliedSticker
    {
        /// <summary>
        /// 印花Id
        /// </summary>
        public uint StickerId { get; set; }

        /// <summary>
        /// 槽位
        /// </summary>
        public uint Slot { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        public float OffsetX { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public float OffsetY { get; set; }

        /// <summary>
        /// Z坐标
        /// </summary>
        public float OffsetZ { get; set; }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// 磨损度
        /// </summary>
        public float Wear { get; set; }

        /// <summary>
        /// 模版
        /// </summary>
        public uint Pattern { get; set; }
    }
}
