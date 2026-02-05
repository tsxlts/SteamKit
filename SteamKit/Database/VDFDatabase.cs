using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using SteamKit.Database.CS2;
using SteamKit.Internal;

namespace SteamKit.Database
{
    internal class VDFDatabase : IDisposable
    {
        private readonly AsyncLock reloadDatabaseLock;
        private readonly CS2Database.Database database;
        private readonly Func<CS2Database.Database, KeyValue> loadDbAction;
        private readonly FileSystemWatcher? fileWatcher;

        private Lazy<KeyValue> content;
        private Lazy<JToken> data;

        public VDFDatabase(CS2Database.Database database, Func<CS2Database.Database, KeyValue> loadAction)
        {
            this.reloadDatabaseLock = new AsyncLock();
            this.database = database;
            this.loadDbAction = loadAction;

            this.fileWatcher = null;

            ReloadDatabase();

            if (database is CS2Database.FileDatabase fileDatabase)
            {
                this.fileWatcher = new FileSystemWatcher(path: fileDatabase.File.DirectoryName!, filter: fileDatabase.File.Name)
                {
                    NotifyFilter = NotifyFilters.LastWrite,
                };
                this.fileWatcher.Changed += (sender, e) =>
                {
                    ReloadDatabase();
                };
                this.fileWatcher.EnableRaisingEvents = true;
            }
        }

        public CS2Database.Database Database { get { return database; } }

        public Lazy<KeyValue> Content { get { return content; } }

        public Lazy<JToken> Data { get { return data; } }

        public void Dispose()
        {
            fileWatcher?.Dispose();
        }

        [MemberNotNull(nameof(content), nameof(data))]
        private void ReloadDatabase()
        {
            using (reloadDatabaseLock.Lock(TimeSpan.FromMinutes(1)))
            {
                content = new Lazy<KeyValue>(() =>
                {
                    return loadDbAction.Invoke(database);
                }, true);

                data = new Lazy<JToken>(() =>
                {
                    var keyValue = content.Value;
                    return keyValue.ToObject().Value<JToken>(keyValue.Name!)!;
                }, true);
            }
        }
    }
}
