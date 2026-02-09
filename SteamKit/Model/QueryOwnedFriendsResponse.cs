
using Newtonsoft.Json;
using static SteamKit.Enums;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询自己的好友响应
    /// </summary>
    public class QueryOwnedFriendsResponse
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
            /// 
            /// </summary>
            [JsonProperty("bincremental")]
            public bool BIncremental { get; set; }

            /// <summary>
            /// 
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
            /// 好友SteamId
            /// </summary>
            [JsonProperty("ulfriendid")]
            public string SteamId { get; set; } = string.Empty;

            /// <summary>
            /// 好友关系
            /// </summary>
            [JsonProperty("efriendrelationship")]
            public FriendRelationship Relationship { get; set; }
        }
    }
}
