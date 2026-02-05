using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询好友响应
    /// </summary>
    public class QueryFriendsResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("friendslist")]
        public FriendsResult Result { get; set; } = new FriendsResult();

        /// <summary>
        /// 
        /// </summary>
        public class FriendsResult
        {
            /// <summary>
            /// 好友列表
            /// </summary>
            [JsonProperty("friends")]
            public List<Friend> Friends { get; set; } = new List<Friend>();
        }

        /// <summary>
        /// 
        /// </summary>
        public class Friend
        {
            /// <summary>
            /// SteamId
            /// </summary>
            [JsonProperty("steamid")]
            public string SteamId { get; set; } = string.Empty;

            /// <summary>
            /// 好友关系
            /// </summary>
            [JsonProperty("relationship")]
            public string Relationship { get; set; } = string.Empty;

            /// <summary>
            /// 成为好友时间
            /// 秒级时间戳
            /// </summary>
            [JsonProperty("friend_since")]
            public long FriendSince { get; set; }
        }
    }
}
