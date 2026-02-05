
using System.Text.RegularExpressions;

namespace SteamKit.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class CookieComparer
    {
        /// <summary>
        /// 判断是否同一个域
        /// </summary>
        /// <param name="domain1"></param>
        /// <param name="domain2"></param>
        /// <returns></returns>
        public static bool EqualDomains(string domain1, string domain2)
        {
            if (string.Equals(domain1, domain2, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var array1 = domain1.Split('.');
            var array2 = domain2.Split(".");
            if (array1.Length != array2.Length)
            {
                return false;
            }

            var s1 = string.Join(".", array1).Replace("*", string.Empty);
            var s2 = string.Join(".", array2).Replace("*", string.Empty);
            return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 判断是否同一个路径
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool EqualPath(string path1, string path2)
        {
            if (string.Equals(path1, path2, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            path1 = $"{path1.Trim('/')}/";
            path2 = $"{path2.Trim('/')}/";
            return string.Equals(path1, path2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 判断mainDomains是否包含subDomains
        /// </summary>
        /// <param name="mainDomains"></param>
        /// <param name="subDomains"></param>
        /// <returns></returns>
        public static bool ContainsDomains(string mainDomains, string subDomains)
        {
            if (string.Equals(mainDomains, subDomains, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            string main = mainDomains.TrimStart('.');
            if (!main.StartsWith("*."))
            {
                main = $"*.{main}";
            }
            main = main.Replace(".", @"\.").Replace("*", @".*");

            string pattern = $"{main}$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            bool contains = regex.IsMatch(subDomains);
            return contains;
        }

        /// <summary>
        /// 判断mainPath是否包含subPath
        /// </summary>
        /// <param name="mainPath"></param>
        /// <param name="subPath"></param>
        /// <returns></returns>
        public static bool ContainsPath(string mainPath, string subPath)
        {
            if (string.IsNullOrWhiteSpace(mainPath) || string.Equals(mainPath, "/", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (string.Equals(mainPath, subPath, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            mainPath = $"{mainPath.Trim('/')}/";
            subPath = $"{subPath.Trim('/')}/";

            return subPath.StartsWith(mainPath, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 判断是否同一个Cookie
        /// </summary>
        /// <param name="cookie1"></param>
        /// <param name="cookie2"></param>
        /// <returns></returns>
        public static bool Equals(Cookie cookie1, Cookie cookie2)
        {
            return string.Equals(cookie1.Name, cookie2.Name, StringComparison.OrdinalIgnoreCase)
                     && EqualDomains(cookie1.Domain ?? "", cookie2.Domain ?? "")
                     && EqualPath(cookie1.Path ?? "", cookie2.Path ?? "");
        }
    }
}
