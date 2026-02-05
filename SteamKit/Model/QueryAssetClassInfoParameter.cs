
namespace SteamKit.Model
{
    /// <summary>
    /// 资产Class请求数据
    /// </summary>
    public class QueryAssetClassInfoParameter
    {
        /// <summary>
        /// ClassId
        /// </summary>
        public ulong ClassId { get; set; }

        /// <summary>
        /// InstanceId
        /// </summary>
        public ulong? InstanceId { get; set; }
    }
}
