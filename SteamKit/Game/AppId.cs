
namespace SteamKit.Game
{
    /// <summary>
    /// 游戏Id
    /// 应用Id
    /// </summary>
    public struct AppId : IEquatable<AppId>
    {
        /// <summary>
        /// CS2
        /// </summary>
        public static AppId CS2 = 730;

        /// <summary>
        /// TF2
        /// </summary>
        public static AppId TF2 = 440;

        /// <summary>
        /// Dota2
        /// </summary>
        public static AppId Dota2 = 570;

        /// <summary>
        /// Id
        /// </summary>
        public readonly uint Id;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        public AppId(uint appId)
        {
            Id = appId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Id.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is AppId other))
            {
                return false;
            }

            return Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AppId other)
        {
            return Id == other.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId1"></param>
        /// <param name="appId2"></param>
        /// <returns></returns>
        public static bool operator ==(AppId appId1, AppId appId2)
        {
            return appId1.Id == appId2.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId1"></param>
        /// <param name="appId2"></param>
        /// <returns></returns>
        public static bool operator !=(AppId appId1, AppId appId2)
        {
            return appId1.Id != appId2.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        public static implicit operator AppId(uint appId)
        {
            return new AppId(appId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        public static implicit operator uint(AppId appId)
        {
            return appId.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        public static implicit operator string(AppId appId)
        {
            return appId.Id.ToString();
        }
    }
}
