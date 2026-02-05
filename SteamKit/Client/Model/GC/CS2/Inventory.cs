using static SteamKit.Client.Model.GC.CS2.CEconItemPreviewDataBlock;

namespace SteamKit.Client.Model.GC.CS2
{
    /// <summary>
    /// 库存
    /// </summary>
    public class Inventory : CSOEconItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="econItem"></param>
        public Inventory()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public uint position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public uint paintindex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public uint paintseed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public float paintwear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public uint tradable_after { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public uint trade_protected_escrow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public uint? kill_eater_value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public uint? kill_eater_score_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public uint quest_id { get; set; }

        /// <summary>
        /// 所在的库存箱Id
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public ulong casket_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public uint casket_contained_item_count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public List<Sticker> stickers { get; } = new List<Sticker>();

        /// <summary>
        /// 
        /// </summary>
        [ProtoBuf.ProtoIgnore]
        public List<Sticker> keychains { get; } = new List<Sticker>();

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defIndex">属性定义</param>
        /// <param name="decode">解码属性</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="value">返回属性值</param>
        /// <returns></returns>
        public bool TryGetAttribute<T>(uint defIndex, Func<CSOEconItemAttribute, T> decode, T? defaultValue, out T? value)
        {
            return SOMsgExtensions.TryGetAttribute(this.attribute, defIndex, decode, defaultValue, out value);
        }
    }
}
