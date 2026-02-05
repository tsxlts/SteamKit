
using System.Text.RegularExpressions;

namespace SteamKit.Internal
{
    internal partial class Regexs
    {
        [GeneratedRegex(@"^(<!DOCTYPE html>)|(<html>[\s\S]*</html>)$", RegexOptions.IgnoreCase, 2000)]
        public static partial Regex HtmlRegex();

        [GeneratedRegex(@"data-loyalty_webapi_token=""(.*?)""", RegexOptions.IgnoreCase, 2000)]
        public static partial Regex WebApiToken();
    }
}
