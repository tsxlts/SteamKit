namespace SteamKit.Internal
{
    internal class CollectionReleaser<TCollection> : IDisposable
    {
        protected readonly TCollection collection;
        private readonly Action<TCollection> release;

        public CollectionReleaser(TCollection collection, Action<TCollection> release)
        {
            this.collection = collection;
            this.release = release;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            release.Invoke(collection);
        }
    }
}
