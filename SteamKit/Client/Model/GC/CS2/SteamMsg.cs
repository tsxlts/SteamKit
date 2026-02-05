
namespace SteamKit.Client.Model.GC.CS2
{
    /// <summary>
    /// 
    /// </summary>
    public class CMsgCraftingRequest : ISteamSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public CraftingRecipe recipe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ulong> item_ids { get; } = new List<ulong>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
                {
                    writer.Write((short)recipe);
                    writer.Write((short)item_ids.Count);

                    foreach (var itemId in item_ids)
                    {
                        writer.Write(itemId);
                    }

                    stream.Write(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                recipe = (CraftingRecipe)reader.ReadInt16();
                var idCount = reader.ReadUInt16();

                for (var i = 0; i < idCount; i++)
                {
                    var id = reader.ReadUInt64(); // grab the next id
                    item_ids.Add(id);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CMsgCraftingResponse : ISteamSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public CraftingRecipe recipe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint unknown { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ulong> item_ids { get; } = new List<ulong>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
                {
                    writer.Write((short)recipe);
                    writer.Write(unknown);
                    writer.Write((short)item_ids.Count);
                    foreach (var itemId in item_ids)
                    {
                        writer.Write(itemId);
                    }

                    stream.Write(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                recipe = (CraftingRecipe)reader.ReadInt16();
                unknown = reader.ReadUInt32();

                var idCount = reader.ReadUInt16();

                for (var i = 0; i < idCount; i++)
                {
                    var id = reader.ReadUInt64();
                    item_ids.Add(id);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CMsgClientToGCUnlockCrateRequest : ISteamSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public ulong crate_item_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong key_item_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(key_item_id);
                    writer.Write(crate_item_id);
                    stream.Write(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                key_item_id = reader.ReadUInt64();
                crate_item_id = reader.ReadUInt64();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CMsgClientToGCUnlockCrateResponse : ISteamSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public EGCMsgResponse result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Item> granted_items { get; } = new List<Item>();

        /// <summary>
        /// 
        /// </summary>
        public partial class Item
        {
            /// <summary>
            /// 
            /// </summary>
            public ulong item_id { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public uint def_index { get; set; }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
                {
                    writer.Write((short)granted_items.Count);
                    foreach (Item item in granted_items)
                    {
                        writer.Write(item.item_id);
                        writer.Write(item.def_index);
                    }

                    writer.Write((uint)result);
                    stream.Write(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void Deserialize(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                var itemCount = reader.ReadInt16();
                for (var i = 0; i < itemCount; i++)
                {
                    var id = reader.ReadUInt64();
                    var defIndex = reader.ReadUInt32();

                    granted_items.Add(new Item
                    {
                        item_id = id,
                        def_index = defIndex
                    });
                }

                result = (EGCMsgResponse)reader.ReadUInt32();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CraftingRecipe : short
    {
        /// <summary>
        /// 错误
        /// </summary>
        None = -1,

        /// <summary>
        /// 10x 消费级 → 1x 工业级
        /// </summary>
        ConsumerGradeToIndustrialGrade = 0,

        /// <summary>
        /// 10x 工业级 → 1x 军规级
        /// </summary>
        IndustrialGradeToMilSpecGrade = 1,

        /// <summary>
        /// 10x 军规级 → 1x 受限
        /// </summary>
        MilSpecGradeToRestricted = 2,

        /// <summary>
        /// 10x 受限 → 1x 保密
        /// </summary>
        RestrictedToClassified = 3,

        /// <summary>
        /// 10x 保密 → 1x 隐秘
        /// </summary>
        ClassifiedToCovert = 4,

        /// <summary>
        /// 5x 隐秘 → 1x 稀有手套/匕首
        /// </summary>
        CovertToSpecial = 5,

        /// <summary>
        /// 10x StatTrak消费级 → 1x StatTrak工业级
        /// </summary>
        StatTrakConsumerGradeToStatTrakIndustrialGrade = 10,

        /// <summary>
        /// 10x StatTrak工业级 → 1x StatTrak军规级
        /// </summary>
        StatTrakIndustrialGradeToStatTrakMilSpecGrade = 11,

        /// <summary>
        /// 10x StatTrak军规级 → 1x StatTrak受限
        /// </summary>
        StatTrakMilSpecGradeToStatTrakRestricted = 12,

        /// <summary>
        /// 10x StatTrak受限 → 1x StatTrak保密
        /// </summary>
        StatTrakRestrictedToStatTrakClassified = 13,

        /// <summary>
        /// 10x StatTrak保密 → 1x StatTrak隐秘
        /// </summary>
        StatTrakClassifiedToStatTrakCovert = 14,

        /// <summary>
        /// 5x StatTrak隐秘 → 1x StatTrak稀有手套/匕首
        /// </summary>
        StatTrakCovertToStatTrakSpecial = 15,
    }
}
