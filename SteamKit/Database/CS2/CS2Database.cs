
using System.Collections.Concurrent;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using SteamKit.Factory;

namespace SteamKit.Database.CS2
{
    /// <summary>
    /// csgo数据库
    /// </summary>
    public partial class CS2Database
    {
        private static ConcurrentDictionary<string, CS2Database> dbs = new ConcurrentDictionary<string, CS2Database>();
        private static CS2Database resourcedb;

        static CS2Database()
        {
            var assembly = Assembly.GetExecutingAssembly();
            resourcedb = new CS2Database(assembly).LoadDatabase();
        }

        /// <summary>
        /// 获取一个默认数据库
        /// </summary>
        public static CS2Database? Default => dbs.Values.FirstOrDefault(c => c.IsDefault);

        /// <summary>
        /// 获取任意一个数据库
        /// </summary>
        /// <returns></returns>
        public static CS2Database? Single => dbs.Values.FirstOrDefault() ?? resourcedb;

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CS2Database? GetDatabase(string name)
        {
            dbs.TryGetValue(name, out var database);
            return database;
        }

        /// <summary>
        /// 设置数据库
        /// </summary>
        /// <param name="name">数据库名称</param>
        /// <param name="dbPath">数据库目录</param>
        /// <param name="isDefault">是否默认数据库</param>
        public static CS2Database SetDatabase(string name, string dbPath, bool isDefault)
        {
            return SetDatabase(name, dbPath, TimeSpan.FromHours(12), isDefault);
        }

        /// <summary>
        /// 设置数据库
        /// </summary>
        /// <param name="name">数据库名称</param>
        /// <param name="dbPath">数据库目录</param>
        /// <param name="refreshPeriod">自动刷新间隔</param>
        /// <param name="isDefault">是否默认数据库</param>
        public static CS2Database SetDatabase(string name, string dbPath, TimeSpan refreshPeriod, bool isDefault)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(name));
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(dbPath));

            if (refreshPeriod <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(refreshPeriod));
            }

            Directory.CreateDirectory(dbPath);
            Directory.CreateDirectory(Path.Combine(dbPath, "resource"));

            return dbs.AddOrUpdate(name, (key) => new CS2Database(dbPath, refreshPeriod) { IsDefault = isDefault }, (key, value) =>
            {
                value.Dispose();
                return new CS2Database(dbPath, refreshPeriod) { IsDefault = isDefault };
            });
        }

        /// <summary>
        /// 设置远程数据库地址
        /// </summary>
        /// <param name="items_game_url"></param>
        /// <param name="items_game_cdn_url"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        public CS2Database SetRemoteDatabase(string items_game_url, string items_game_cdn_url, IDictionary<Enums.Language, string> resources)
        {
            this.items_game.SetRemoteUrl(items_game_url);
            this.items_game.SetRemoteUrl(items_game_cdn_url);

            foreach (var resource in resources)
            {
                SetResourceRemoteDatabase(resource.Key, resource.Value);
            }

            return this;
        }

        /// <summary>
        /// 设置远程数据库地址
        /// </summary>
        /// <param name="language"></param>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        public CS2Database SetRemoteDatabase(Enums.Language language, string resourceUrl)
        {
            SetResourceRemoteDatabase(language, resourceUrl);
            return this;
        }

        /// <summary>
        /// 加载数据库
        /// </summary>
        /// <returns></returns>
        public CS2Database LoadDatabase()
        {
            if (loaded)
            {
                return this;
            }

            loaded = true;
            ReloadDatabase();
            return this;
        }

        /// <summary>
        /// 设置日志处理器
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public CS2Database SetLogger(ILogger logger)
        {
            this.logger = logger;
            return this;
        }

        /// <summary>
        /// 刷新数据库
        /// </summary>
        public async Task RefreshAsync(CancellationToken cancellationToken = default)
        {
            Task[] tasks =
            [
                items_game.RefreshAsync(this.logger, cancellationToken),
                items_game_cdn.RefreshAsync(this.logger, cancellationToken),

                ..this.csgo_resources.Select(c=>c.RefreshAsync(this.logger, cancellationToken)),
            ];
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 属性
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AttributeDefinition> Attributes()
        {
            var attributes = itemsGame?.Content?.Value["attributes"] ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");
            foreach (var attribute in attributes?.Children ?? new List<Internal.KeyValue>())
            {
                if (!uint.TryParse(attribute.Name, out var Id) || Id == 0)
                {
                    continue;
                }

                var def = attribute.ToObject().Value<JToken>(attribute.Name)!;

                yield return new AttributeDefinition
                {
                    Id = Id,
                    Name = def.Value<string>("name") ?? "",
                    AttributeClass = def.Value<string>("attribute_class") ?? "",
                    Group = def.Value<string>("group") ?? "",
                    DescriptionFormat = def.Value<string>("description_format") ?? "",
                    DescriptionString = def.Value<string>("description_string") ?? "",
                    EffectType = def.Value<string>("effect_type") ?? "",
                    Hidden = def.Value<string>("hidden") ?? "",
                    StoredAsInteger = def.Value<string>("stored_as_integer") ?? ""
                };
            }
        }

        /// <summary>
        /// 物品定义
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public IEnumerable<Items> Items(Enums.Language language)
        {
            var items = itemsGame?.Content?.Value["items"] ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");
            foreach (var item in items.Children)
            {
                if (!uint.TryParse(item.Name, out var defIndex) || defIndex == 0)
                {
                    continue;
                }

                var def = CS2Utils.QueryItemDef(defIndex);
                if (def == null)
                {
                    continue;
                }

                var hashName = CS2Utils.QueryItemDefName(def, Enums.Language.English);
                var name = CS2Utils.QueryItemDefName(def, language);

                yield return new Items
                {
                    DefIndex = defIndex,
                    Key = def.Key,
                    HashName = hashName ?? "",
                    Name = name ?? "",
                    BaseItem = def.BaseItem,
                };
            }
        }

        /// <summary>
        /// 印花或者布章
        /// </summary>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public IEnumerable<Sticker> Stickers(Enums.Language language)
        {
            var stickerKits = itemsGame?.Content?.Value["sticker_kits"] ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");

            foreach (var sticker in stickerKits.Children ?? new List<Internal.KeyValue>())
            {
                if (!uint.TryParse(sticker.Name, out var Id) || Id == 0)
                {
                    continue;
                }

                var kits = sticker.ToObject().Value<JToken>(sticker.Name)!.ToObject<StickerKits>()!;
                if (!Regex.IsMatch(kits.item_name!, @"^(#?)PatchKit_") && !Regex.IsMatch(kits.item_name!, @"^(#?)StickerKit_"))
                {
                    continue;
                }

                string hashName = CS2Utils.QueryStickerHashName(kits);
                string name = CS2Utils.QueryStickerName(kits, language);

                yield return new Sticker
                {
                    Id = Id,
                    HashName = hashName,
                    Name = name
                };
            }
        }

        /// <summary>
        /// 印花
        /// </summary>
        /// <param name="language">语言类型</param>
        /// <param name="stickerId">印花Id</param>
        /// <returns></returns>
        public Sticker? Sticker(uint stickerId, Enums.Language language)
        {
            var hashName = CS2Utils.QueryStickerHashName(stickerId);
            if (string.IsNullOrWhiteSpace(hashName))
            {
                return null;
            }

            var name = CS2Utils.QueryStickerName(stickerId, language) ?? "";

            return new Sticker
            {
                Id = stickerId,
                HashName = hashName,
                Name = name
            };
        }

        /// <summary>
        /// 挂件
        /// </summary>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public IEnumerable<Keychain> Keychains(Enums.Language language)
        {
            var keychainDefinitions = itemsGame?.Content?.Value["keychain_definitions"] ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");
            foreach (var keychain in keychainDefinitions.Children ?? new List<Internal.KeyValue>())
            {
                if (!uint.TryParse(keychain.Name, out var Id) || Id == 0)
                {
                    continue;
                }

                var kits = keychain.ToObject().Value<JToken>(keychain.Name)!.ToObject<KeychainKits>()!;

                string hashName = CS2Utils.QueryKeychainHashName(kits);
                string name = CS2Utils.QueryKeychainName(kits, language);

                yield return new Keychain
                {
                    Id = Id,
                    HashName = hashName,
                    Name = name
                };
            }
        }

        /// <summary>
        /// 挂件
        /// </summary>
        /// <param name="keychainId">挂件Id</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public Keychain? Keychain(uint keychainId, Enums.Language language)
        {
            var hashName = CS2Utils.QueryKeychainHashName(keychainId);
            if (string.IsNullOrWhiteSpace(hashName))
            {
                return null;
            }

            var name = CS2Utils.QueryKeychainName(keychainId, language) ?? "";

            return new Keychain
            {
                Id = keychainId,
                HashName = hashName,
                Name = name
            };
        }

        /// <summary>
        /// 皮肤
        /// </summary>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public IEnumerable<Paint> Paints(Enums.Language language)
        {
            var paintKits = itemsGame?.Content?.Value["paint_kits"] ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");
            foreach (var paint in paintKits.Children ?? new List<Internal.KeyValue>())
            {
                if (!uint.TryParse(paint.Name, out var Id) || Id == 0)
                {
                    continue;
                }

                var kits = paint.ToObject().Value<JToken>(paint.Name)!.ToObject<PaintKits>()!;

                var hashName = CS2Utils.QueryPaintName(kits, Enums.Language.English);
                var name = CS2Utils.QueryPaintName(kits, language);
                yield return new Paint
                {
                    PaintIndex = Id,
                    HashName = hashName ?? "",
                    Name = name ?? ""
                };
            }
        }

        /// <summary>
        /// 皮肤
        /// </summary>
        /// <param name="paintIndex">皮肤编号</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public Paint? Paint(uint paintIndex, Enums.Language language)
        {
            var kits = CS2Utils.QueryPaint(paintIndex);
            if (kits == null)
            {
                return null;
            }

            var hashName = CS2Utils.QueryPaintName(kits, Enums.Language.English);
            var name = CS2Utils.QueryPaintName(kits, language);
            return new Paint
            {
                PaintIndex = paintIndex,
                HashName = hashName ?? "",
                Name = name ?? ""
            };
        }

        /// <summary>
        /// 收藏品
        /// </summary>
        /// <param name="setHashName">收藏品HashName</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public Collectibles? Collectibles(string setHashName, Enums.Language language)
        {
            var set = CS2Utils.QueryCollection(setHashName);
            if (set == null)
            {
                return null;
            }

            var enName = CS2Utils.QueryName(set.name.Replace("#", ""), Enums.Language.English);
            var name = CS2Utils.QueryName(set.name.Replace("#", ""), language);
            var description = CS2Utils.QueryName(set.set_description.Replace("#", ""), language);
            var rarities = GetSetRarities(setHashName, language);

            return new Collectibles
            {
                HashName = setHashName,
                EnName = enName ?? "",
                Name = name ?? "",
                Description = description ?? "",
                IsCollection = "1".Equals(set.is_collection),
                Rarities = rarities
            };
        }

        /// <summary>
        /// 收藏品
        /// </summary>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public IEnumerable<Collectibles> Collectibles(Enums.Language language)
        {
            var itemSets = itemsGame?.Content?.Value["item_sets"] ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");
            foreach (var itemSet in itemSets.Children ?? new List<Internal.KeyValue>())
            {
                if (string.IsNullOrWhiteSpace(itemSet.Name))
                {
                    continue;
                }

                var set = itemSet.ToObject().Value<JToken>(itemSet.Name)!.ToObject<CollectiblesDefinition>()!;

                var enName = CS2Utils.QueryName(set.name.Replace("#", ""), Enums.Language.English);
                var name = CS2Utils.QueryName(set.name.Replace("#", ""), language);
                var description = CS2Utils.QueryName(set.set_description.Replace("#", ""), language);
                var rarities = GetSetRarities(itemSet.Name, language);

                yield return new Collectibles
                {
                    HashName = itemSet.Name,
                    EnName = enName ?? "",
                    Name = name ?? "",
                    Description = description ?? "",
                    IsCollection = "1".Equals(set.is_collection),
                    Rarities = rarities
                };
            }
        }

        /// <summary>
        /// 武器箱
        /// </summary>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<WeaponCase> WeaponCases(Enums.Language language)
        {
            var items = itemsGame?.Content?.Value["items"] ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");
            foreach (var item in items.Children ?? new List<Internal.KeyValue>())
            {
                if (!uint.TryParse(item.Name, out var Id) || Id == 0)
                {
                    continue;
                }

                var @case = item.ToObject().Value<JObject>(item.Name)!;
                var weaponHud = @case.Value<string>("item_name")?.Replace("#", "");
                if (string.IsNullOrWhiteSpace(weaponHud))
                {
                    continue;
                }

                if (!@case.TryGetValue("attributes", StringComparison.OrdinalIgnoreCase, out var attributes) || !(attributes is JObject atrtibutesObj))
                {
                    continue;
                }
                if (!atrtibutesObj.TryGetValue("set supply crate series", StringComparison.OrdinalIgnoreCase, out var crateSeries))
                {
                    continue;
                }
                if (!uint.TryParse(crateSeries.Value<string>("value"), out var series))
                {
                    continue;
                }

                var keyDefIndex = 0u;
                if (@case.TryGetValue("associated_items", StringComparison.OrdinalIgnoreCase, out var key) && (key is JContainer container) && (container.First is JProperty property))
                {
                    uint.TryParse(property.Name, out keyDefIndex);
                }

                var hashName = CS2Utils.QueryName(weaponHud, Enums.Language.English)!;
                var name = CS2Utils.QueryName(weaponHud, language);

                var setHahsName = @case.Value<string>("name")!;
                var rarities = GetSetRarities(setHahsName, language);

                yield return new WeaponCase
                {
                    DefIndex = Id,
                    HashName = hashName,
                    Name = name ?? "",
                    CrateSeries = series,
                    KeyDefIndex = keyDefIndex,
                    Rarities = rarities
                };
            }
        }

        /// <summary>
        /// 武器箱钥匙
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public IEnumerable<WeaponCaseKey> WeaponCaseKeys(Enums.Language language)
        {
            var items = itemsGame?.Content?.Value["items"] ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");
            foreach (var item in items.Children ?? new List<Internal.KeyValue>())
            {
                if (!uint.TryParse(item.Name, out var Id) || Id == 0)
                {
                    continue;
                }

                var keyPrefab = "weapon_case_key";

                var caseKey = item.ToObject().Value<JObject>(item.Name)!;
                var prefab = caseKey.Value<string>("prefab");
                if (string.IsNullOrWhiteSpace(prefab))
                {
                    continue;
                }
                var prefabs = prefab.Split(' ');
                if (!prefabs.Contains(keyPrefab))
                {
                    continue;
                }

                var key = caseKey.Value<string>("name");
                var weaponHud = caseKey.Value<string>("item_name")?.Replace("#", "");
                if (string.IsNullOrWhiteSpace(weaponHud))
                {
                    var prefabDef = itemsGame?.Content?.Value["prefabs"][keyPrefab];
                    weaponHud = prefabDef?["item_name"]?.Value?.Replace("#", "");
                }
                if (string.IsNullOrWhiteSpace(weaponHud))
                {
                    continue;
                }

                var hashName = CS2Utils.QueryName(weaponHud, Enums.Language.English)!;
                var name = CS2Utils.QueryName(weaponHud, language);

                var setHahsName = caseKey.Value<string>("name")!;
                var rarities = GetSetRarities(setHahsName, language);

                yield return new WeaponCaseKey
                {
                    DefIndex = Id,
                    Key = key ?? "",
                    HashName = hashName,
                    Name = name ?? "",
                };
            }
        }

        /// <summary>
        /// 品质
        /// </summary>
        /// <param name="rarityhashName">品质HashName</param>
        /// <param name="language"></param>
        /// <returns></returns>
        public Rarity? Rarity(string rarityhashName, Enums.Language language)
        {
            var kit = CS2Utils.QuetyRarity(rarityhashName);
            if (kit == null)
            {
                return null;
            }

            var result = new Rarity
            {
                HashName = rarityhashName,
                Key = kit.loc_key ?? "",
                Name = "",
                WeaponKey = kit.loc_key_weapon ?? "",
                WeaponName = "",
                CharacterKey = kit.loc_key_character ?? "",
                CharacterName = "",
                NextRarity = kit.next_rarity ?? ""
            };

            if (!string.IsNullOrEmpty(result.Key))
            {
                result.Name = CS2Utils.QueryName(result.Key, language) ?? "";
            }
            if (!string.IsNullOrEmpty(result.WeaponKey))
            {
                result.WeaponName = CS2Utils.QueryName(result.WeaponKey, language) ?? "";
            }
            if (!string.IsNullOrEmpty(result.CharacterKey))
            {
                result.CharacterName = CS2Utils.QueryName(result.CharacterKey, language) ?? "";
            }
            return result;
        }

        /// <summary>
        /// 获取收藏品包含的物品品质
        /// </summary>
        /// <param name="setHashName"></param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        private List<Rarity> GetSetRarities(string setHashName, Enums.Language language)
        {
            var result = new List<Rarity>();

            var kits = itemsGame?.Content?.Value["client_loot_lists"] ?? throw new Exception("未获取到可用数据库,请先加载数据库,items_game");
            var raitiykit = itemsGame.Content?.Value["rarities"].Children;

            var hashName = setHashName;
            var list = kits?.Children.Where(c => c.Name?.StartsWith($"{hashName}_") ?? false) ?? new List<Internal.KeyValue>();
            if (!list.Any())
            {
                hashName = Regex.Replace(hashName, @"^set_", "crate_");
                list = kits?.Children.Where(c => c.Name?.StartsWith($"{hashName}_") ?? false) ?? new List<Internal.KeyValue>();
            }

            Match match;
            string rarityHashName;
            Rarity? rarity;
            foreach (var item in list)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                {
                    continue;
                }

                match = Regex.Match(item.Name, @$"^{hashName}_(?<rarity>[^_]+)$");
                if (!match.Success)
                {
                    continue;
                }

                rarityHashName = match.Groups["rarity"].Value;
                rarity = Rarity(rarityHashName, language);
                if (rarity == null)
                {
                    continue;
                }

                result.Add(rarity);
            }

            return result;
        }

        /// <summary>
        /// 设置Resource远程数据库地址
        /// </summary>
        /// <param name="language"></param>
        /// <param name="resourceUrl"></param>
        private void SetResourceRemoteDatabase(Enums.Language language, string resourceUrl)
        {
            var index = this.csgo_resources.FindIndex(c => c.Language == language);
            if (index < 0)
            {
                return;
            }

            this.csgo_resources[index].SetRemoteUrl(resourceUrl);
        }
    }
}
