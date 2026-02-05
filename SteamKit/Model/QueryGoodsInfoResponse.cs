
namespace SteamKit.Model
{
    /// <summary>
    /// 查询商品信息响应
    /// </summary>
    public class QueryGoodsInfoResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// AppId
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// ContextId
        /// </summary>
        public string? ContextId { get; set; }

        /// <summary>
        /// HashName
        /// </summary>
        public string? HashName { get; set; }

        /// <summary>
        /// 默认ClassId
        /// </summary>
        public ulong? ClassId { get; set; }

        /// <summary>
        /// 默认检视连接
        /// </summary>
        public string? DefaultInspectUrl { get; set; }
    }
}
