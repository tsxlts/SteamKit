using SteamKit.Game;

namespace SteamKit.Client
{
    /// <summary>
    /// EMsgExtensions
    /// </summary>
    public static class EMsgExtensions
    {
        /// <summary>
        /// 获取GC消息名称
        /// </summary>
        /// <param name="gcMsgType">gcMsg</param>
        /// <param name="appId">AppId</param>
        /// <param name="gcMsg">GCMsg</param>
        /// <returns></returns>
        public static string GetGCMessageName(uint gcMsgType, AppId appId, out object? gcMsg)
        {
            var eMsgEnums = GetGCEMsgEnums(appId);
            foreach (var enumType in eMsgEnums)
            {
                if (!Enum.IsDefined(enumType, (int)gcMsgType))
                {
                    continue;
                }

                Enum.TryParse(enumType, $"{gcMsgType}", true, out gcMsg);
                return Enum.GetName(enumType, (int)gcMsgType)!;
            }

            gcMsg = gcMsgType;
            return gcMsgType.ToString();
        }

        /// <summary>
        /// 获取GC信息类型
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetGCEMsgEnums(AppId appId)
        {
            if (appId == AppId.TF2)
            {
                yield return typeof(Model.GC.TF2.ETFGCMsg);
                yield return typeof(Model.GC.TF2.EGCBaseMsg);
                yield return typeof(Model.GC.TF2.ESOMsg);
                yield return typeof(Model.GC.TF2.EGCSystemMsg);
                yield return typeof(Model.GC.TF2.EGCItemMsg);
                yield return typeof(Model.GC.TF2.EGCBaseClientMsg);
                yield break;
            }

            if (appId == AppId.Dota2)
            {
                yield return typeof(Model.GC.Dota2.EDOTAGCMsg);
                yield return typeof(Model.GC.Dota2.EGCBaseMsg);
                yield return typeof(Model.GC.Dota2.ESOMsg);
                yield return typeof(Model.GC.Dota2.EGCItemMsg);
                yield return typeof(Model.GC.Dota2.EGCBaseClientMsg);
                yield break;
            }

            if (appId == AppId.CS2)
            {
                yield return typeof(Model.GC.CS2.ECsgoGCMsg);
                yield return typeof(Model.GC.CS2.EGCBaseMsg);
                yield return typeof(Model.GC.CS2.ESOMsg);
                yield return typeof(Model.GC.CS2.EGCSystemMsg);
                yield return typeof(Model.GC.CS2.EGCItemMsg);
                yield return typeof(Model.GC.CS2.EGCBaseClientMsg);
                yield break;
            }
        }
    }
}
