namespace SteamKit.Internal
{
    internal static class HttpUtils
    {
        public const string CookieSplit = "; ";

        public static CookieCollection GetCookies(this HttpResponseMessage response)
        {
            CookieCollection cookieCollection = new CookieCollection();

            IEnumerable<string>? cookies;
            response.Headers.TryGetValues("Set-Cookie", out cookies);
            if (cookies == null)
            {
                return cookieCollection;
            }

            string cookie;
            string key;
            string value;
            string[] array;
            foreach (string item in cookies)
            {
                cookie = item.Split(new string[] { CookieSplit }, StringSplitOptions.RemoveEmptyEntries)[0];
                array = cookie.Split('=');
                key = array[0];
                value = Uri.UnescapeDataString(array[1]);

                cookieCollection.Add(new Cookie(key, value));
            }

            return cookieCollection;
        }
    }
}
