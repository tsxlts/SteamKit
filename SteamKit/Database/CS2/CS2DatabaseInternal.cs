using System.Net;
using System.Reflection;
using SteamKit.Factory;
using SteamKit.Internal;
using static SteamKit.SteamEnum;

namespace SteamKit.Database.CS2
{
    public partial class CS2Database : IDisposable
    {
        private readonly Timer timer;
        private readonly AsyncLock loadLock;

        private readonly Database items_game;
        private readonly Database items_game_cdn;
        private readonly List<Database> csgo_resources = new List<Database>();

        private ILogger? logger = null;
        private bool loaded = false;
        private VDFDatabase? itemsGame = null;
        private VDFDatabase? itemsGameCDN = null;
        private IDictionary<Language, VDFDatabase>? resources = null;

        private bool IsDefault = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        private CS2Database(Assembly assembly) :
            this(itemsGame: InitItemsGame(assembly), itemsGameCDN: InitItemsGameCDN(assembly), csgoResources: InitResources(assembly), refreshInterval: Timeout.InfiniteTimeSpan)
        {

        }

        private CS2Database(string dbPath, TimeSpan refreshInterval) :
            this(itemsGame: InitItemsGame(dbPath), itemsGameCDN: InitItemsGameCDN(dbPath), csgoResources: InitResources(dbPath), refreshInterval: refreshInterval)
        {
        }

        private CS2Database(Database itemsGame, Database itemsGameCDN, List<Database> csgoResources, TimeSpan refreshInterval)
        {
            this.loadLock = new AsyncLock();

            this.items_game = itemsGame;
            this.items_game_cdn = itemsGameCDN;
            this.csgo_resources = csgoResources;

            var dueTime = Timeout.InfiniteTimeSpan;
            var interval = Timeout.InfiniteTimeSpan;
            if (refreshInterval > TimeSpan.Zero)
            {
                dueTime = refreshInterval;
                interval = refreshInterval * 2;
            }

            timer = new Timer((obj) =>
            {
                TimeSpan currentInterval = refreshInterval;
                try
                {
                    using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60)))
                    {
                        RefreshAsync(cts.Token).ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                }
                catch (Exception ex)
                {
                    logger?.LogWarning($"自动刷新数据库异常, {ex.Message}");
                    logger?.LogException(ex, $"自动刷新数据库异常, {ex.Message}");

                    currentInterval = TimeSpan.FromMinutes(5);
                }
                finally
                {
                    timer!.Change(currentInterval, currentInterval * 2);
                }
            }, null, dueTime, interval);
        }

        private void ReloadDatabase()
        {
            using (loadLock.Lock(TimeSpan.FromMinutes(1)))
            {
                itemsGame = new VDFDatabase(items_game, (db) =>
                {
                    using (var stream = db.Open())
                    {
                        var keyValue = KeyValue.FromStream(stream!);
                        return keyValue;
                    }
                });

                itemsGameCDN = new VDFDatabase(items_game_cdn, (db) =>
                {
                    using (var stream = db.Open())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string data = reader.ReadToEnd();
                            var lines = data.Split('\n');

                            var keyValue = new KeyValue("items_game_cdn");
                            foreach (var line in lines)
                            {
                                var array = line.Split('=');

                                if (array.Length > 1)
                                {
                                    keyValue.Children.Add(new KeyValue(array[0].Trim('\r'), array[1].Trim('\r')));
                                }
                            }
                            return keyValue;
                        }
                    }
                });

                IDictionary<Language, VDFDatabase> keyValues = new Dictionary<Language, VDFDatabase>();
                foreach (var resource in csgo_resources)
                {
                    keyValues.Add(resource.Language,
                        new VDFDatabase(resource, (db) =>
                        {
                            using (var stream = db.Open())
                            {
                                var keyValue = KeyValue.FromStream(stream!)["Tokens"];
                                return keyValue;
                            }
                        }));
                }

                resources = new Dictionary<Language, VDFDatabase>(keyValues);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal VDFDatabase? ItemsGame => itemsGame;

        /// <summary>
        /// 
        /// </summary>
        internal VDFDatabase? ItemsGameCDN => itemsGameCDN;

        /// <summary>
        /// 
        /// </summary>
        internal IDictionary<Language, VDFDatabase>? Resources => resources;

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            timer.Dispose();

            itemsGame = null;
            itemsGameCDN = null;
            resources?.Clear();
        }

        private static Database InitItemsGame(string dbPath)
        {
            return new FileDatabase(name: "items_game",
                file: Path.Combine(dbPath, "items_game.txt"),
                remoteUrl: "https://raw.githubusercontent.com/SteamDatabase/GameTracking-CS2/master/game/csgo/pak01_dir/scripts/items/items_game.txt",
                language: Language.None);
        }

        private static Database InitItemsGameCDN(string dbPath)
        {
            return new FileDatabase(name: "items_game_cdn",
                file: Path.Combine(dbPath, "items_game_cdn.txt"),
                remoteUrl: "https://raw.githubusercontent.com/SteamDatabase/GameTracking-CS2/master/game/csgo/pak01_dir/scripts/items/items_game_cdn.txt",
                language: Language.None);
        }

        private static List<Database> InitResources(string dbPath)
        {
            var databases = new List<Database>();
            foreach (var item in Enum.GetValues<Language>())
            {
                if (item == Language.None)
                {
                    continue;
                }

                databases.Add(new FileDatabase(name: $"csgo_{item.ToString().ToLower()}",
                    file: Path.Combine(dbPath, "resource", $"csgo_{item.ToString().ToLower()}.txt"),
                    remoteUrl: $"https://raw.githubusercontent.com/SteamDatabase/GameTracking-CS2/master/game/csgo/pak01_dir/resource/csgo_{item.ToString().ToLower()}.txt",
                    language: item));
            }
            return databases;
        }

        private static Database InitItemsGame(Assembly assembly)
        {
            var name = $"SteamKit.Assets.SteamDatabase.items_game.txt";
            var fullResourceName = assembly.GetManifestResourceNames().FirstOrDefault(c => c.Equals(name, StringComparison.OrdinalIgnoreCase)) ?? throw new FileNotFoundException("无法加载资源", name);

            return new ResourceDatabase(assembly,
                name: "items_game",
                resourceName: fullResourceName,
                language: Language.None);
        }

        private static Database InitItemsGameCDN(Assembly assembly)
        {
            var name = $"SteamKit.Assets.SteamDatabase.items_game_cdn.txt";
            var fullResourceName = assembly.GetManifestResourceNames().FirstOrDefault(c => c.Equals(name, StringComparison.OrdinalIgnoreCase)) ?? throw new FileNotFoundException("无法加载资源", name);

            return new ResourceDatabase(assembly,
                name: "items_game_cdn",
                resourceName: fullResourceName,
                language: Language.None);
        }

        private static List<Database> InitResources(Assembly assembly)
        {
            var databases = new List<Database>();
            foreach (var item in Enum.GetValues<Language>())
            {
                if (item == Language.None)
                {
                    continue;
                }

                var name = $"SteamKit.Assets.SteamDatabase.resource.csgo_{item.ToString().ToLower()}.txt";
                var fullResourceName = assembly.GetManifestResourceNames().FirstOrDefault(c => c.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (string.IsNullOrWhiteSpace(fullResourceName))
                {
                    continue;
                }

                databases.Add(new ResourceDatabase(assembly,
                name: $"csgo_{item.ToString().ToLower()}",
                resourceName: fullResourceName,
                language: item));
            }
            return databases;
        }

        internal class ResourceDatabase : Database
        {
            private readonly Assembly assembly;

            public ResourceDatabase(Assembly assembly, string name, string resourceName, Language language) : base(name, fullName: resourceName, remoteUrl: "", language)
            {
                this.assembly = assembly;
            }

            public override ResourceDatabase SetRemoteUrl(string remoteUrl)
            {
                return this;
            }

            public override Stream Open()
            {
                var stream = assembly.GetManifestResourceStream(this.FullName);
                return stream ?? new MemoryStream();
            }

            public override Task RefreshAsync(ILogger? logger, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }
        }

        internal class FileDatabase : Database
        {
            private readonly FileInfo db;

            public FileDatabase(string name, string file, string remoteUrl, Language language) : base(name, fullName: file, remoteUrl: remoteUrl, language)
            {
                db = new FileInfo(file);

                if (!db.Directory!.Exists)
                {
                    db.Directory!.Create();
                }
            }

            public FileInfo File { get { return db; } }

            public override FileDatabase SetRemoteUrl(string remoteUrl)
            {
                this.Remote = remoteUrl;
                return this;
            }

            public override Stream Open()
            {
                if (!db.Exists)
                {
                    return new MemoryStream();
                }

                var stream = db.OpenRead();
                return stream;
            }

            public override async Task RefreshAsync(ILogger? logger, CancellationToken cancellationToken = default)
            {
                if (string.IsNullOrWhiteSpace(this.Remote))
                {
                    return;
                }

                var response = await SteamApi.GetAsync(this.Remote).ConfigureAwait(false);
                logger?.LogDebug($"更新CS2Database, {this.Remote} , HttpStatusCode:{response.HttpStatusCode} , {response.Content.Length} bytes");

                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    return;
                }

                using (var stream = response.Content)
                {
                    using (var fileStream = db.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        fileStream.SetLength(0);
                        await stream.CopyToAsync(fileStream);
                    }
                }
            }
        }

        internal abstract class Database
        {
            /// <summary>
            /// 
            /// </summary>
            public readonly Language Language;

            public Database(string name, string fullName, string remoteUrl, Language language)
            {
                FullName = fullName;
                Name = name;
                Remote = remoteUrl;
                Language = language;
            }

            public string Remote { get; protected set; }

            public string Name { get; }

            public string FullName { get; }

            public abstract Database SetRemoteUrl(string remoteUrl);

            public abstract Stream Open();

            public abstract Task RefreshAsync(ILogger? logger, CancellationToken cancellationToken = default);
        }
    }
}
