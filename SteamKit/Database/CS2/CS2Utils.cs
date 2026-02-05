using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using static SteamKit.SteamEnum;

namespace SteamKit.Database.CS2
{
    /// <summary>
    /// CSGOUtils
    /// </summary>
    public static class CS2Utils
    {
        private static VDFDatabase ItemsGame => CS2Database.Default?.ItemsGame ?? CS2Database.Single?.ItemsGame ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");
        private static VDFDatabase ItemsGameCDN => CS2Database.Default?.ItemsGameCDN ?? CS2Database.Single?.ItemsGameCDN ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game_cdn");
        private static IDictionary<Language, VDFDatabase> Resources => CS2Database.Default?.Resources ?? CS2Database.Single?.Resources ?? throw new Exception("未获取到可用数据库,请先加载数据库,resource");

        /// <summary>
        /// 查询物品定义
        /// </summary>
        /// <param name="defIndex">defIndex</param>
        /// <returns></returns>
        public static ItemDefinition? QueryItemDef(uint defIndex)
        {
            string? weaponHud = null;

            JObject? obj = ItemsGame.Content.Value["items"][defIndex.ToString()].ToObject()?.Value<JObject>(defIndex.ToString());
            var key = obj?.Value<string>("name");
            var baseItem = obj?.Value<string>("baseitem") == "1";

            if (obj?.HasValues ?? false)
            {
                if (!obj.ContainsKey("item_name"))
                {
                    string prefabVal = obj.Value<string>("prefab")?.Split(' ')?.LastOrDefault() ?? "";
                    obj = ItemsGame.Content.Value["prefabs"][prefabVal].ToObject().Value<JObject>(prefabVal);
                }
                weaponHud = obj?.Value<string>("item_name")?.Replace("#", "");
            }

            var result = new ItemDefinition
            {
                DefIndex = defIndex,
                Key = key ?? "",
                BaseItem = baseItem,
                ItemName = weaponHud
            };
            return result;
        }

        /// <summary>
        /// 查询物品定义
        /// </summary>
        /// <param name="key">Name</param>
        /// <returns></returns>
        public static ItemDefinition? QueryItemDef(string key)
        {
            string? weaponHud = null;

            var keyValue = ItemsGame.Content.Value["items"].Children.FirstOrDefault(c => c["name"].Value == key);
            if (keyValue == null)
            {
                return null;
            }

            uint.TryParse(keyValue.Name, out uint defIndex);

            var obj = keyValue.ToObject()?.Value<JObject>(keyValue.Name!);
            var baseItem = obj?.Value<string>("baseitem") == "1";
            if (obj?.HasValues ?? false)
            {
                if (!obj.ContainsKey("item_name"))
                {
                    string prefabVal = obj.Value<string>("prefab")?.Split(' ')?.LastOrDefault() ?? "";
                    obj = ItemsGame.Content.Value["prefabs"][prefabVal].ToObject().Value<JObject>(prefabVal);
                }
                weaponHud = obj?.Value<string>("item_name")?.Replace("#", "");
            }

            var result = new ItemDefinition
            {
                DefIndex = defIndex,
                Key = key,
                BaseItem = baseItem,
                ItemName = weaponHud
            };
            return result;
        }

        /// <summary>
        /// 查询物品定义名称
        /// </summary>
        /// <param name="defIndex">defIndex</param>
        /// <param name="language">language</param>
        /// <returns></returns>
        public static string? QueryItemDefName(uint defIndex, Language language = Language.English)
        {
            var def = QueryItemDef(defIndex);
            if (string.IsNullOrWhiteSpace(def?.ItemName))
            {
                return null;
            }

            return QueryItemDefName(def, language);
        }

        /// <summary>
        /// 查询物品定义名称
        /// </summary>
        /// <param name="itemDef">物品定义</param>
        /// <param name="language">language</param>
        /// <returns></returns>
        public static string? QueryItemDefName(ItemDefinition itemDef, Language language = Language.English)
        {
            var codeName = itemDef.ItemName?.Replace("#", "")!;
            var defName = QueryName(codeName, language);
            return defName;
        }

        /// <summary>
        /// 查询皮肤材料
        /// </summary>
        /// <param name="paintIndex">paintIndex</param>
        /// <returns></returns>
        public static PaintKits? QueryPaint(uint paintIndex)
        {
            var obj = ItemsGame.Content.Value["paint_kits"][paintIndex.ToString()].ToObject()?.Value<JObject>(paintIndex.ToString());
            var result = obj?.ToObject<PaintKits>();
            return result;
        }

        /// <summary>
        /// 查询皮肤名称
        /// </summary>
        /// <param name="paintIndex">paintIndex</param>
        /// <param name="language">language</param>
        /// <returns></returns>
        public static string? QueryPaintName(uint paintIndex, Language language = Language.English)
        {
            var paint = QueryPaint(paintIndex);
            if (string.IsNullOrWhiteSpace(paint?.name))
            {
                return null;
            }

            return QueryPaintName(paint, language);
        }

        /// <summary>
        /// 查询皮肤名称
        /// </summary>
        /// <param name="paint">皮肤</param>
        /// <param name="language">language</param>
        /// <returns></returns>
        public static string? QueryPaintName(PaintKits paint, Language language = Language.English)
        {
            var codeName = paint.description_tag?.Replace("#", "")!;
            var seekName = QueryName(codeName, language);
            return seekName;
        }

        /// <summary>
        /// 查询皮肤材料标签
        /// </summary>
        /// <param name="paintIndex"></param>
        /// <returns></returns>
        public static string QueryPaintLabel(uint paintIndex)
        {
            var paint = QueryPaint(paintIndex);
            if (string.IsNullOrWhiteSpace(paint?.name))
            {
                return "";
            }

            if (Regex.IsMatch(paint.name, "^am_doppler_phase1|^am_gamma_doppler_phase1", RegexOptions.IgnoreCase))
            {
                return "Phase1";
            }
            if (Regex.IsMatch(paint.name, "^am_doppler_phase2|^am_gamma_doppler_phase2", RegexOptions.IgnoreCase))
            {
                return "Phase2";
            }
            if (Regex.IsMatch(paint.name, "^am_doppler_phase3|^am_gamma_doppler_phase3", RegexOptions.IgnoreCase))
            {
                return "Phase3";
            }
            if (Regex.IsMatch(paint.name, "^am_doppler_phase4|^am_gamma_doppler_phase4", RegexOptions.IgnoreCase))
            {
                return "Phase4";
            }

            if (paint.name.StartsWith("am_emerald_marbleized", StringComparison.CurrentCultureIgnoreCase))
            {
                return "绿宝石";
            }
            if (paint.name.StartsWith("am_ruby_marbleized", StringComparison.CurrentCultureIgnoreCase))
            {
                return "红宝石";
            }
            if (paint.name.StartsWith("am_sapphire_marbleized", StringComparison.CurrentCultureIgnoreCase))
            {
                return "蓝宝石";
            }
            if (paint.name.StartsWith("am_blackpearl_marbleized", StringComparison.CurrentCultureIgnoreCase))
            {
                return "黑珍珠";
            }

            return "";
        }

        /// <summary>
        /// 查询品质
        /// </summary>
        /// <param name="rarity">rarity</param>
        /// <returns></returns>
        public static RarityKits? QuetyRarity(uint rarity)
        {
            RarityKits? result;
            foreach (var item in ItemsGame.Content.Value["rarities"].Children)
            {
                result = item.ToObject().Value<JObject>(item.Name!)?.ToObject<RarityKits>();
                if (result?.value == rarity)
                {
                    result.name = item.Name!;
                    return result;
                }

                result = null;
            }

            return null;
        }

        /// <summary>
        /// 查询品质
        /// </summary>
        /// <param name="rarity">rarity</param>
        /// <returns></returns>
        public static RarityKits? QuetyRarity(string rarity)
        {
            var kit = ItemsGame.Content.Value["rarities"][rarity];
            if (string.IsNullOrWhiteSpace(kit?.Name))
            {
                return null;
            }

            RarityKits result = kit.ToObject().Value<JToken>(rarity)!.ToObject<RarityKits>()!;
            result.name = rarity;
            return result;
        }

        /// <summary>
        /// 查询类别
        /// </summary>
        /// <param name="quality">类别</param>
        /// <returns></returns>
        public static QualitiyKits? QueryQualitiy(uint quality)
        {
            QualitiyKits? result;
            foreach (var item in ItemsGame.Content.Value["qualities"].Children)
            {
                result = item.ToObject().Value<JObject>(item.Name!)?.ToObject<QualitiyKits>();
                if (result?.value == quality)
                {
                    result.name = item.Name!;
                    return result;
                }

                result = null;
            }

            return null;
        }

        /// <summary>
        /// 查询印花或者布章
        /// </summary>
        /// <param name="stickerId">stickerId</param>
        /// <returns></returns>
        public static StickerKits? QueryStickerKits(uint stickerId)
        {
            JObject stickerKits = ItemsGame.Content.Value["sticker_kits"].ToObject().Value<JObject>("sticker_kits")!;
            JObject? sticker = stickerKits.Value<JObject>(stickerId.ToString());
            if (sticker?.HasValues ?? false)
            {
                StickerKits? result = sticker.ToObject<StickerKits>();
                return result;
            }

            return null;
        }

        /// <summary>
        /// 查询挂件
        /// </summary>
        /// <param name="keychainId"></param>
        /// <returns></returns>
        public static KeychainKits? QueryKeychainKits(uint keychainId)
        {
            JObject keychainKits = ItemsGame.Content.Value["keychain_definitions"].ToObject().Value<JObject>("keychain_definitions")!;
            JObject? keychain = keychainKits.Value<JObject>(keychainId.ToString());
            if (keychain?.HasValues ?? false)
            {
                KeychainKits? result = keychain.ToObject<KeychainKits>();
                return result;
            }

            return null;
        }

        /// <summary>
        /// 查询音乐盒
        /// </summary>
        /// <param name="musicId"></param>
        /// <returns></returns>
        public static MusicKits? QueryMusicKits(uint musicId)
        {
            JObject musics = ItemsGame.Content.Value["music_definitions"].ToObject().Value<JObject>("music_definitions")!;
            JObject? music = musics.Value<JObject>(musicId.ToString());
            if (music?.HasValues ?? false)
            {
                MusicKits? result = music.ToObject<MusicKits>();
                return result;
            }

            return null;
        }

        /// <summary>
        /// 查询收藏品定义
        /// </summary>
        /// <param name="hashName">收藏品HashName</param>
        /// <returns></returns>
        public static CollectiblesDefinition? QueryCollection(string hashName)
        {
            JObject itemSets = ItemsGame.Content.Value["item_sets"].ToObject().Value<JObject>("item_sets")!;
            JObject? set = itemSets.Value<JObject>(hashName);
            if (set?.HasValues ?? false)
            {
                CollectiblesDefinition? result = set.ToObject<CollectiblesDefinition>();
                return result;
            }

            return null;
        }

        /// <summary>
        /// 查询高光时刻定义
        /// </summary>
        /// <param name="highlightReel">高光时刻</param>
        /// <returns></returns>
        public static HighlightReelDefinition? QueryHighlightReel(uint highlightReel)
        {
            JObject highlightReels = ItemsGame.Content.Value["highlight_reels"].ToObject().Value<JObject>("highlight_reels")!;
            JObject? HighlightReelKit = highlightReels.Value<JObject>(highlightReel.ToString());
            if (HighlightReelKit?.HasValues ?? false)
            {
                HighlightReelDefinition? result = HighlightReelKit.ToObject<HighlightReelDefinition>();
                return result;
            }

            return null;
        }

        /// <summary>
        /// 查询外观描述名称
        /// </summary>
        /// <param name="floatValue"></param>
        /// <param name="language">Language</param>
        /// <returns></returns>
        public static string? QueryWearName(double floatValue, Language language)
        {
            var floatNames = new[]{
                new{
                    range= new[]{0, 0.07 },
                    name= "SFUI_InvTooltip_Wear_Amount_0"
                },
                new{
                    range=new[] {0.07, 0.15 },
                    name= "SFUI_InvTooltip_Wear_Amount_1"
                },
                new{
                    range=new[]  { 0.15, 0.38},
                    name = "SFUI_InvTooltip_Wear_Amount_2"
                },
                new
                {
                    range = new[] { 0.38, 0.45 },
                    name = "SFUI_InvTooltip_Wear_Amount_3"
                },
                new
                {
                    range = new[] { 0.45, 1.00 },
                    name = "SFUI_InvTooltip_Wear_Amount_4"
                }
            };

            var floatName = floatNames.FirstOrDefault(c => c.range[0] < floatValue && floatValue <= c.range[1]);
            if (floatName == null)
            {
                return null;
            }

            return QueryName(floatName.name, language);
        }

        /// <summary>
        /// 查询武器图片
        /// </summary>
        /// <param name="defIndex"></param>
        /// <param name="paintIndex"></param>
        /// <returns></returns>
        public static string? QueryWeaponImg(uint defIndex, uint paintIndex)
        {
            string? weaponName = ItemsGame.Content.Value["items"][defIndex.ToString()]["name"].Value;
            PaintKits? paintData = QueryPaint(paintIndex);
            string? skinName = $"_{paintData?.name}";
            if (skinName == "_default")
            {
                skinName = "";
            }

            string imageName = $"{weaponName}{skinName}";

            string? result = ItemsGameCDN.Content.Value[imageName].Value;
            return result;
        }

        /// <summary>
        /// 查询物品HashName
        /// </summary>
        /// <param name="defIndex">defIndex</param>
        /// <param name="quality">quality</param>
        /// <param name="killeaterValue">killeaterValue</param>
        /// <param name="paintIndex">paintIndex</param>
        /// <param name="floatValue">floatValue</param>
        /// <returns></returns>
        public static string? QueryHashName(uint defIndex, uint quality, uint? killeaterValue, uint paintIndex, float floatValue)
        {
            //★ StatTrak™ Falchion Knife | Doppler (Minimal Wear)
            //弯刀（★ StatTrak™） | 多普勒 (略有磨损)

            string? defType = QueryItemDefName(defIndex, Language.English);

            string? qualityName = null;
            if (new uint[] { 3, 9, 12 }.Contains(quality))
            {
                QualitiyKits? qualityData = QueryQualitiy(quality);
                if (qualityData != null)
                {
                    string codeName = qualityData.name!;
                    qualityName = QueryName(codeName, Language.English);

                    if (quality == 3 && killeaterValue.HasValue)
                    {
                        qualityName = $"{qualityName} {QueryName("strange", Language.English)}";
                    }
                }
            }

            string? seekName = null;
            PaintKits? paintData = QueryPaint(paintIndex);
            if (paintData != null)
            {
                string codeName = paintData.description_tag?.Replace("#", "")!;
                seekName = QueryName(codeName, Language.English);
                if (seekName == "-")
                {
                    seekName = null;
                }
            }

            string? wearName = null;
            if (!string.IsNullOrWhiteSpace(seekName))
            {
                wearName = QueryWearName(floatValue, Language.English);
            }

            string? def = !string.IsNullOrWhiteSpace(qualityName) ? $"{qualityName} {defType}" : defType;
            string? seek = !string.IsNullOrWhiteSpace(wearName) ? $"{seekName} ({wearName})" : seekName;
            var keys = new List<string?> { def, seek };
            keys.RemoveAll(string.IsNullOrWhiteSpace);

            var hashName = string.Join(" | ", keys);
            return hashName;
        }

        /// <summary>
        /// 查询物品名称
        /// </summary>
        /// <param name="defIndex">defIndex</param>
        /// <param name="quality">quality</param>
        /// <param name="killeaterValue">killeaterValue</param>
        /// <param name="paintIndex">paintIndex</param>
        /// <param name="floatValue">floatValue</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string? QueryName(uint defIndex, uint quality, uint? killeaterValue, uint paintIndex, float floatValue, Language language)
        {
            //★ StatTrak™ Falchion Knife | Doppler (Minimal Wear)
            //弯刀（★ StatTrak™） | 多普勒 (略有磨损)

            string? defType = QueryItemDefName(defIndex, language);

            string? qualityName = null;
            if (new uint[] { 3, 9, 12 }.Contains(quality))
            {
                QualitiyKits? qualityData = QueryQualitiy(quality);
                if (qualityData != null)
                {
                    string codeName = qualityData.name!;
                    qualityName = QueryName(codeName, language);

                    if (quality == 3 && killeaterValue.HasValue)
                    {
                        qualityName = $"{qualityName} {QueryName("strange", language)}";
                    }
                }
            }

            string? seekName = null;
            PaintKits? paintData = QueryPaint(paintIndex);
            if (paintData != null)
            {
                string codeName = paintData.description_tag?.Replace("#", "")!;
                seekName = QueryName(codeName, language);
                if (seekName == "-")
                {
                    seekName = null;
                }
            }

            string? wearName = null;
            if (!string.IsNullOrWhiteSpace(seekName))
            {
                wearName = QueryWearName(floatValue, language);
            }

            List<string?> keys;
            switch (language)
            {
                case Language.English:
                    string? def = !string.IsNullOrWhiteSpace(qualityName) ? $"{qualityName} {defType}" : defType;
                    string? seek = !string.IsNullOrWhiteSpace(wearName) ? $"{seekName} ({wearName})" : seekName;
                    keys = new List<string?> { def, seek };
                    break;

                case Language.Schinese:
                case Language.Tchinese:
                    def = !string.IsNullOrWhiteSpace(qualityName) ? $"{defType}（{qualityName}）" : defType;
                    seek = !string.IsNullOrWhiteSpace(wearName) ? $"{seekName} ({wearName})" : seekName;
                    keys = new List<string?> { def, seek };
                    break;

                default:
                    def = !string.IsNullOrWhiteSpace(qualityName) ? $"{defType} ({qualityName})" : defType;
                    seek = !string.IsNullOrWhiteSpace(wearName) ? $"{seekName} ({wearName})" : seekName;
                    keys = new List<string?> { def, seek };
                    break;
            }

            keys.RemoveAll(string.IsNullOrWhiteSpace);
            var name = string.Join(" | ", keys);
            return name;
        }

        #region 音乐盒
        /// <summary>
        /// 查询音乐盒HashName
        /// </summary>
        /// <param name="musicId">音乐盒Id</param>
        /// <param name="quality">quality</param>
        /// <returns></returns>
        public static string? QueryMusicKitHashName(uint musicId, uint quality)
        {
            var music = QueryMusicKits(musicId);
            if (music == null)
            {
                return null;
            }

            return QueryMusicKitHashName(music, quality);
        }

        /// <summary>
        /// 查询音乐盒HashName
        /// </summary>
        /// <param name="music">音乐盒</param>
        /// <param name="quality">quality</param>
        /// <returns></returns>
        public static string QueryMusicKitHashName(MusicKits music, uint quality)
        {
            //StatTrak™ Music Kit | Skog, III-Arena
            //音乐盒（StatTrak™） | Skog - III-竞技场

            StringBuilder builder = new StringBuilder();

            string? qualityName = null;
            if (new uint[] { 3, 9, 12 }.Contains(quality))
            {
                QualitiyKits? qualityData = QueryQualitiy(quality);
                if (qualityData != null)
                {
                    string codeName = qualityData.name!;
                    qualityName = QueryName(codeName, Language.English);
                }
            }

            string? defType = QueryName("CSGO_Type_MusicKit", Language.English);
            string? def = !string.IsNullOrWhiteSpace(qualityName) ? $"{qualityName} {defType}" : defType;
            builder.Append($"{def} | ");

            string? name = QueryName(music.loc_name!.Replace("#", ""), Language.English);
            builder.Append($"{name}");

            return builder.ToString().Trim();
        }

        /// <summary>
        /// 查询音乐盒名称
        /// </summary>
        /// <param name="musicId">音乐盒Id</param>
        /// <param name="quality">quality</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string? QueryMusicKitName(uint musicId, uint quality, Language language)
        {
            var music = QueryMusicKits(musicId);
            if (music == null)
            {
                return null;
            }

            return QueryMusicKitName(music, quality, language);
        }

        /// <summary>
        /// 查询音乐盒名称
        /// </summary>
        /// <param name="music">音乐盒Id</param>
        /// <param name="quality">quality</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string QueryMusicKitName(MusicKits music, uint quality, Language language)
        {
            StringBuilder builder = new StringBuilder();

            string? qualityName = null;
            if (new uint[] { 3, 9, 12 }.Contains(quality))
            {
                QualitiyKits? qualityData = QueryQualitiy(quality);
                if (qualityData != null)
                {
                    string codeName = qualityData.name!;
                    qualityName = QueryName(codeName, language);
                }
            }

            string? defType = QueryName("CSGO_Type_MusicKit", language);

            switch (language)
            {
                case Language.English:
                    string? def = !string.IsNullOrWhiteSpace(qualityName) ? $"{qualityName} {defType}" : defType;
                    builder.Append($"{def} | ");
                    break;

                case Language.Schinese:
                case Language.Tchinese:
                    def = !string.IsNullOrWhiteSpace(qualityName) ? $"{defType}（{qualityName}）" : defType;
                    builder.Append($"{def} | ");
                    break;

                default:
                    def = !string.IsNullOrWhiteSpace(qualityName) ? $"{defType} ({qualityName})" : defType;
                    builder.Append($"{def} | ");
                    break;
            }

            string? name = QueryName(music.loc_name!.Replace("#", ""), language);
            builder.Append($"{name}");

            return builder.ToString().Trim();
        }
        #endregion

        #region 涂鸦
        /// <summary>
        /// 查询涂鸦HashName
        /// </summary>
        /// <param name="sprayId">涂鸦Id</param>
        /// <param name="tintId">颜色</param>
        /// <returns></returns>
        public static string? QuerySprayHashName(uint sprayId, uint tintId)
        {
            var spray = QueryStickerKits(sprayId);
            if (spray == null)
            {
                return null;
            }

            return QuerySprayHashName(spray, tintId);
        }

        /// <summary>
        /// 查询涂鸦HashName
        /// </summary>
        /// <param name="spray">涂鸦</param>
        /// <param name="tintId">颜色Id</param>
        /// <returns></returns>
        public static string QuerySprayHashName(StickerKits spray, uint tintId)
        {
            StringBuilder builder = new StringBuilder();

            string? defType = QueryName("CSGO_Tool_Spray", Language.English);
            builder.Append($"{defType} | ");

            string? name = QueryName(spray.item_name!.Replace("#", ""), Language.English);
            builder.Append(name);

            if (tintId > 0)
            {
                string? tintValue = QueryName($"Attrib_SprayTintValue_{tintId}", Language.English);
                if (!string.IsNullOrWhiteSpace(tintValue))
                {
                    builder.Append($" ({tintValue})");
                }
            }

            return builder.ToString().Trim();
        }

        /// <summary>
        /// 查询涂鸦名称
        /// </summary>
        /// <param name="sprayId">涂鸦Id</param>
        /// <param name="tintId">颜色</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string? QuerySprayName(uint sprayId, uint tintId, Language language)
        {
            var spray = QueryStickerKits(sprayId);
            if (spray == null)
            {
                return null;
            }

            return QuerySprayName(spray, tintId, language);
        }

        /// <summary>
        /// 查询涂鸦HashName
        /// </summary>
        /// <param name="spray">涂鸦</param>
        /// <param name="tintId">颜色Id</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string QuerySprayName(StickerKits spray, uint tintId, Language language)
        {
            StringBuilder builder = new StringBuilder();

            string? defType = QueryName("CSGO_Tool_Spray", language);
            builder.Append($"{defType} | ");

            string? name = QueryName(spray.item_name!.Replace("#", ""), language);
            builder.Append(name);

            if (tintId > 0)
            {
                string? tintValue = QueryName($"Attrib_SprayTintValue_{tintId}", language);
                if (!string.IsNullOrWhiteSpace(tintValue))
                {
                    builder.Append($" ({tintValue})");
                }
            }

            return builder.ToString().Trim();
        }
        #endregion

        #region 印花 、布章
        /// <summary>
        /// 查询印花或者布章HashName
        /// </summary>
        /// <param name="stickerId">印花Id</param>
        /// <returns></returns>
        public static string? QueryStickerHashName(uint stickerId)
        {
            var sticker = QueryStickerKits(stickerId);
            if (sticker == null)
            {
                return null;
            }

            return QueryStickerHashName(sticker);
        }

        /// <summary>
        /// 查询印花或者布章HashName
        /// </summary>
        /// <param name="sticker">印花</param>
        /// <returns></returns>
        public static string QueryStickerHashName(StickerKits sticker)
        {
            var type = GetStickerKitType(sticker, Language.English);

            StringBuilder builder = new StringBuilder();
            builder.Append($"{type.EnName} | ");

            string? name = QueryName(sticker.item_name!.Replace("#", ""), Language.English);
            builder.Append(name);

            return builder.ToString().Trim();
        }

        /// <summary>
        /// 查询印花或者布章Name
        /// </summary>
        /// <param name="stickerId">印花Id</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string? QueryStickerName(uint stickerId, Language language)
        {
            var sticker = QueryStickerKits(stickerId);
            if (sticker == null)
            {
                return null;
            }

            return QueryStickerName(sticker, language);
        }

        /// <summary>
        /// 查询印花或者布章Name
        /// </summary>
        /// <param name="sticker">印花</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string QueryStickerName(StickerKits sticker, Language language)
        {
            var type = GetStickerKitType(sticker, language);

            StringBuilder builder = new StringBuilder();
            builder.Append($"{type.Name} | ");

            string? name = QueryName(sticker.item_name!.Replace("#", ""), language);
            builder.Append(name);

            return builder.ToString().Trim();
        }
        #endregion

        #region 印花板
        /// <summary>
        /// 查询印花板HashName
        /// </summary>
        /// <param name="stickerId">印花Id</param>
        /// <returns></returns>
        public static string? QueryStickerSlabHashName(uint stickerId)
        {
            var sticker = QueryStickerKits(stickerId);
            if (sticker == null)
            {
                return null;
            }

            return QueryStickerSlabHashName(sticker);
        }

        /// <summary>
        /// 查询印花板HashName
        /// </summary>
        /// <param name="sticker">印花</param>
        /// <returns></returns>
        public static string QueryStickerSlabHashName(StickerKits sticker)
        {
            var stickerSlab = QueryKeychainKits(37);
            var defType = "Sticker Slab";
            if (stickerSlab != null)
            {
                defType = QueryName(stickerSlab.loc_name!.Replace("#", ""), Language.English);
            }

            StringBuilder builder = new StringBuilder();
            builder.Append($"{defType} | ");

            string? name = QueryName(sticker.item_name!.Replace("#", ""), Language.English);
            builder.Append(name);

            return builder.ToString().Trim();
        }

        /// <summary>
        /// 查询印花板Name
        /// </summary>
        /// <param name="stickerId">印花Id</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string? QueryStickerSlabName(uint stickerId, Language language)
        {
            var sticker = QueryStickerKits(stickerId);
            if (sticker == null)
            {
                return null;
            }

            return QueryStickerSlabName(sticker, language);
        }

        /// <summary>
        /// 查询印花板Name
        /// </summary>
        /// <param name="sticker">印花</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string QueryStickerSlabName(StickerKits sticker, Language language)
        {
            var stickerSlab = QueryKeychainKits(37);
            var defType = "Sticker Slab";
            if (stickerSlab != null)
            {
                defType = QueryName(stickerSlab.loc_name!.Replace("#", ""), language);
            }

            StringBuilder builder = new StringBuilder();
            builder.Append($"{defType} | ");

            string? name = QueryName(sticker.item_name!.Replace("#", ""), language);
            builder.Append(name);

            return builder.ToString().Trim();
        }
        #endregion

        #region 挂件
        /// <summary>
        /// 查询挂件HashName
        /// </summary>
        /// <param name="keychainId">挂件Id</param>
        /// <returns></returns>
        public static string? QueryKeychainHashName(uint keychainId)
        {
            var keychain = QueryKeychainKits(keychainId);
            if (keychain == null)
            {
                return null;
            }

            return QueryKeychainHashName(keychain);
        }

        /// <summary>
        /// 查询挂件HashName
        /// </summary>
        /// <param name="keychain">挂件</param>
        /// <returns></returns>
        public static string QueryKeychainHashName(KeychainKits keychain)
        {
            StringBuilder builder = new StringBuilder();

            string? defType = QueryName("CSGO_Tool_Keychain", Language.English);
            builder.Append($"{defType} | ");

            string? name = QueryName(keychain.loc_name!.Replace("#", ""), Language.English);
            builder.Append(name);

            return builder.ToString().Trim();
        }

        /// <summary>
        /// 查询挂件Name
        /// </summary>
        /// <param name="keychainId">挂件Id</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string? QueryKeychainName(uint keychainId, Language language)
        {
            var keychain = QueryKeychainKits(keychainId);
            if (keychain == null)
            {
                return null;
            }

            return QueryKeychainName(keychain, language);
        }

        /// <summary>
        /// 查询挂件Name
        /// </summary>
        /// <param name="keychain">挂件</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string QueryKeychainName(KeychainKits keychain, Language language)
        {
            StringBuilder builder = new StringBuilder();

            string? defType = QueryName("CSGO_Tool_Keychain", language);
            builder.Append($"{defType} | ");

            string? name = QueryName(keychain.loc_name!.Replace("#", ""), language);
            builder.Append(name);

            return builder.ToString().Trim();
        }
        #endregion

        #region 挂件
        /// <summary>
        /// 查询挂件HashName
        /// </summary>
        /// <param name="keychainId">挂件Id</param>
        /// <param name="quality">挂件类别</param>
        /// <param name="highlightReel">高光时刻</param>
        /// <returns></returns>
        public static string? QueryKeychainHashName(uint keychainId, uint quality, uint highlightReel)
        {
            var keychain = QueryKeychainKits(keychainId);
            if (keychain == null)
            {
                return null;
            }

            return QueryKeychainHashName(keychain, quality, highlightReel);
        }

        /// <summary>
        /// 查询挂件HashName
        /// </summary>
        /// <param name="keychain">挂件</param>
        /// <param name="quality">挂件类别</param>
        /// <param name="highlightReel">高光时刻</param>
        /// <returns></returns>
        public static string QueryKeychainHashName(KeychainKits keychain, uint quality, uint highlightReel)
        {
            StringBuilder builder = new StringBuilder();

            string? qualityName = null;
            if (new uint[] { 3, 9, 12 }.Contains(quality))
            {
                QualitiyKits? qualityData = QueryQualitiy(quality);
                if (qualityData != null)
                {
                    string codeName = qualityData.name!;
                    qualityName = QueryName(codeName, Language.English);
                }
            }

            string? defType = QueryName("CSGO_Tool_Keychain", Language.English);
            string? def = !string.IsNullOrWhiteSpace(qualityName) ? $"{qualityName} {defType}" : defType;
            builder.Append($"{def} | ");

            string? name = QueryName(keychain.loc_name!.Replace("#", ""), Language.English);
            HighlightReelDefinition? highlightReelDefinition = QueryHighlightReel(highlightReel);
            if (highlightReelDefinition != null)
            {
                string? highlightReelName = QueryName($"HighlightReel_{highlightReelDefinition.id}", Language.English);
                if (!string.IsNullOrWhiteSpace(highlightReelName))
                {
                    name = $"{name} | {highlightReelName}";
                }
            }
            builder.Append($"{name}");

            return builder.ToString().Trim();
        }

        /// <summary>
        /// 查询挂件Name
        /// </summary>
        /// <param name="keychainId">挂件Id</param>
        /// <param name="quality">挂件类别</param>
        /// <param name="highlightReel">高光时刻</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string? QueryKeychainName(uint keychainId, uint quality, uint highlightReel, Language language)
        {
            var keychain = QueryKeychainKits(keychainId);
            if (keychain == null)
            {
                return null;
            }

            return QueryKeychainName(keychain, quality, highlightReel, language);
        }

        /// <summary>
        /// 查询挂件Name
        /// </summary>
        /// <param name="keychain">挂件</param>
        /// <param name="quality">挂件类别</param>
        /// <param name="highlightReel">高光时刻</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static string QueryKeychainName(KeychainKits keychain, uint quality, uint highlightReel, Language language)
        {
            StringBuilder builder = new StringBuilder();

            string? qualityName = null;
            if (new uint[] { 3, 9, 12 }.Contains(quality))
            {
                QualitiyKits? qualityData = QueryQualitiy(quality);
                if (qualityData != null)
                {
                    string codeName = qualityData.name!;
                    qualityName = QueryName(codeName, language);
                }
            }

            string? defType = QueryName("CSGO_Tool_Keychain", language);

            switch (language)
            {
                case Language.English:
                    string? def = !string.IsNullOrWhiteSpace(qualityName) ? $"{qualityName} {defType}" : defType;
                    builder.Append($"{def} | ");
                    break;

                case Language.Schinese:
                case Language.Tchinese:
                    def = !string.IsNullOrWhiteSpace(qualityName) ? $"{defType}（{qualityName}）" : defType;
                    builder.Append($"{def} | ");
                    break;

                default:
                    def = !string.IsNullOrWhiteSpace(qualityName) ? $"{defType} ({qualityName})" : defType;
                    builder.Append($"{def} | ");
                    break;
            }

            string? name = QueryName(keychain.loc_name!.Replace("#", ""), language);
            HighlightReelDefinition? highlightReelDefinition = QueryHighlightReel(highlightReel);
            if (highlightReelDefinition != null)
            {
                string? highlightReelName = QueryName($"HighlightReel_{highlightReelDefinition.id}", language);
                if (!string.IsNullOrWhiteSpace(highlightReelName))
                {
                    name = $"{name} | {highlightReelName}";
                }
            }
            builder.Append($"{name}");

            return builder.ToString().Trim();
        }
        #endregion

        /// <summary>
        /// 查询收藏品对应武器箱HashName
        /// </summary>
        /// <param name="setHashName">收藏品HashName</param>
        /// <returns></returns>
        public static string? QueryCollectionCrateHashName(string setHashName)
        {
            foreach (var item in ItemsGame.Content.Value["items"].Children)
            {
                var tag = item["tags"]["ItemSet"]["tag_value"];
                if (tag.Value != setHashName)
                {
                    continue;
                }

                var prefab = item["prefab"].Value;
                if (!"weapon_case".Equals(prefab, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string crateHashName = item["name"].Value!;
                return QueryName($"CSGO_{crateHashName}", Language.English);
            }

            return null;
        }

        /// <summary>
        /// 获取名字
        /// </summary>
        /// <param name="key"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? QueryName(string key, Language language)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            if (Resources.TryGetValue(language, out var db))
            {
                (db.Data.Value as JObject)!.TryGetValue(key, StringComparison.OrdinalIgnoreCase, out var token);

                var result = token?.Value<string>();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    return result;
                }
            }

            if (Resources.TryGetValue(Language.English, out db))
            {
                (db.Data.Value as JObject)!.TryGetValue(key, StringComparison.OrdinalIgnoreCase, out var token);

                var result = token?.Value<string>();
                return result;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sticker"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        private static (string? EnName, string? Name) GetStickerKitType(StickerKits sticker, Language language)
        {

            if (sticker.IsSticker())
            {
                string? defEnType = QueryName("CSGO_Tool_Sticker", Language.English);
                string? defType = QueryName("CSGO_Tool_Sticker", language);
                return (defEnType, defType);
            }
            if (sticker.IsPatch())
            {
                string? defEnType = QueryName("CSGO_Tool_Patch", Language.English);
                string? defType = QueryName("CSGO_Tool_Patch", language);
                return (defEnType, defType);
            }

            throw new NotSupportedException($"无法解析{sticker.item_name}");
        }
    }
}
