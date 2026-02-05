using SteamKit.Client.Model.GC.CS2;

namespace SteamKit.Client
{
    internal class SOMsgExtensions
    {
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributes"></param>
        /// <param name="defIndex">属性定义</param>
        /// <param name="decode">解码属性</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="value">返回属性值</param>
        /// <returns></returns>
        public static bool TryGetAttribute<T>(IEnumerable<CSOEconItemAttribute> attributes, uint defIndex, Func<CSOEconItemAttribute, T> decode, T? defaultValue, out T? value)
        {
            var attribute = attributes.FirstOrDefault(c => c.def_index == defIndex);
            if (attribute == null)
            {
                value = defaultValue;
                return false;
            }

            value = decode(attribute);
            return true;
        }
    }
}
