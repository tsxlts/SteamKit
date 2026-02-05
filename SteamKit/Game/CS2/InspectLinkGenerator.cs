using System.IO.Hashing;
using System.Text;
using ProtoBuf;
using SteamKit.Client.Model.GC.CS2;
using SteamKit.Internal;
using SteamKit.Model;

namespace SteamKit.Game.CS2
{
    /// <summary>
    /// 检视链接生成器
    /// </summary>
    public class InspectLinkGenerator
    {
        //private const string baseUrl = "steam://rungame/730/76561202255233023/+csgo_econ_action_preview%20";
        private const string baseUrl = "steam://run/730/en/+csgo_econ_action_preview%20";

        /// <summary>
        /// 生成检视链接
        /// </summary>
        /// <param name="defIndex">模型编号</param>
        /// <param name="paintIndex">皮肤编号</param>
        /// <param name="paintSeed">图案模板</param>
        /// <param name="paintWear">磨损度</param>
        /// <param name="rarity">品质Id</param>
        /// <param name="stickers">印花</param>
        /// <param name="keychains">挂件</param>
        /// <returns></returns>
        public static string GenerateInspect(uint defIndex, uint paintIndex, uint paintSeed, float paintWear, uint rarity, List<AppliedSticker>? stickers, List<AppliedSticker>? keychains)
        {
            return GenerateInspect(defIndex, paintIndex, paintSeed, BitConverter.SingleToUInt32Bits(paintWear), rarity, stickers, keychains);
        }

        /// <summary>
        /// 生成检视链接
        /// </summary>
        /// <param name="defIndex">模型编号</param>
        /// <param name="paintIndex">皮肤编号</param>
        /// <param name="paintSeed">图案模板</param>
        /// <param name="paintWear">磨损度</param>
        /// <param name="rarity">品质Id</param>
        /// <param name="stickers">印花</param>
        /// <param name="keychains">挂件</param>
        /// <returns></returns>
        public static string GenerateInspect(uint defIndex, uint paintIndex, uint paintSeed, uint paintWear, uint rarity, List<AppliedSticker>? stickers, List<AppliedSticker>? keychains)
        {
            CEconItemPreviewDataBlock proto = new CEconItemPreviewDataBlock
            {
                defindex = defIndex,
                paintindex = paintIndex,
                paintseed = paintSeed,
                paintwear = paintWear,
                rarity = rarity
            };
            if (stickers?.Any() ?? false)
            {
                foreach (AppliedSticker sticker in stickers.OrderBy(x => x.Slot))
                {
                    CEconItemPreviewDataBlock.Sticker proto_sticker = new()
                    {
                        slot = sticker.Slot,
                        sticker_id = sticker.StickerId,
                        pattern = sticker.Pattern
                    };

                    if (sticker.OffsetX != 0)
                    {
                        proto_sticker.offset_x = sticker.OffsetX;
                    }
                    if (sticker.OffsetY != 0)
                    {
                        proto_sticker.offset_y = sticker.OffsetY;
                    }
                    if (sticker.OffsetZ != 0)
                    {
                        proto_sticker.offset_z = sticker.OffsetZ;
                    }
                    if (sticker.Rotation != 0)
                    {
                        proto_sticker.rotation = sticker.Rotation;
                    }
                    if (sticker.Wear > 0)
                    {
                        proto_sticker.wear = sticker.Wear;
                    }

                    proto.stickers.Add(proto_sticker);
                }
            }

            if (keychains?.Any() ?? false)
            {
                foreach (AppliedSticker sticker in keychains.OrderBy(x => x.Slot))
                {
                    CEconItemPreviewDataBlock.Sticker proto_sticker = new()
                    {
                        slot = sticker.Slot,
                        sticker_id = sticker.StickerId,
                        pattern = sticker.Pattern
                    };

                    if (sticker.OffsetX != 0)
                    {
                        proto_sticker.offset_x = sticker.OffsetX;
                    }
                    if (sticker.OffsetY != 0)
                    {
                        proto_sticker.offset_y = sticker.OffsetY;
                    }
                    if (sticker.OffsetZ != 0)
                    {
                        proto_sticker.offset_z = sticker.OffsetZ;
                    }
                    if (sticker.Rotation != 0)
                    {
                        proto_sticker.rotation = sticker.Rotation;
                    }
                    if (sticker.Wear > 0)
                    {
                        proto_sticker.wear = sticker.Wear;
                    }

                    proto.keychains.Add(proto_sticker);
                }
            }

            return GenerateInspect(proto);
        }

        /// <summary>
        /// 生成检视链接
        /// </summary>
        /// <param name="proto">资产信息</param>
        /// <returns></returns>
        public static string GenerateInspect(CEconItemPreviewDataBlock proto)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, proto);
                byte[] payload = stream.ToArray();
                payload = new byte[] { 0 }.Concat(payload).ToArray();

                uint crc = Crc32.HashToUInt32(payload);

                uint x_crc = crc & 0xffff ^ (uint)stream.Length * crc;

                byte[] crcBuffer = BitConverter.GetBytes((x_crc & 0xffffffff) >>> 0);
                Array.Reverse(crcBuffer);

                byte[] buffer = payload.Concat(crcBuffer).ToArray();

                StringBuilder hexBuilder = new StringBuilder(buffer.Length * 2);
                foreach (byte b in buffer)
                {
                    hexBuilder.AppendFormat("{0:X2}", b);
                }

                var inspect = hexBuilder.ToString().ToUpper();
                return $"{baseUrl}{inspect}";
            }
        }

        /// <summary>
        /// 解析检视参数
        /// </summary>
        /// <param name="inspectParams">检视参数</param>
        /// <returns></returns>
        public static CEconItemPreviewDataBlock GetInspectParameters(string inspectParams)
        {
            var buffer = Utils.HexStringToByteArray(inspectParams);
            var payload = buffer.Take(buffer.Length - 4).ToArray();
            var proto = Serializer.Deserialize<CEconItemPreviewDataBlock>(new MemoryStream(payload.Skip(1).ToArray()));
            return proto;
        }
    }
}
