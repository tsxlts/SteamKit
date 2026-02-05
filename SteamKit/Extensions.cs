using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SteamKit.Internal;
using SteamKit.Model;
using static SteamKit.SteamEnum;

namespace SteamKit
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class Extensions
    {
        private static AsyncLock steamTimestampLock = new AsyncLock();
        private static bool aligned = false;
        private static TimeSpan TimeDifference = TimeSpan.Zero;

        /// <summary>
        /// 默认SteamId
        /// </summary>
        public const ulong DefaultSteamId = 76561197960265728;

        /// <summary>
        /// Steam语言Cookie名称
        /// </summary>
        public const string SteamLanguageCookeName = "Steam_Language";

        /// <summary>
        /// Steam SessionId Cookie名称
        /// </summary>
        public const string SteamSessionidCookieName = "sessionid";

        /// <summary>
        /// Steam登录Token Cookie名称
        /// </summary>
        public const string SteamAccessTokenCookeName = "steamLoginSecure";

        /// <summary>
        /// Steam刷新Token Cookie名称
        /// </summary>
        public const string SteamRefreshTokenCookeName = "steamRefresh_steam";

        /// <summary>
        /// 交易资格检查 Cookie名称
        /// </summary>
        public const string WebTradeEligibilityCookeName = "webTradeEligibility";

        /// <summary>
        /// 获取Steam秒级时间戳
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async ValueTask<ulong> GetSteamTimestampAsync(CancellationToken cancellationToken = default)
        {
            if (!aligned)
            {
                using (await steamTimestampLock.LockAsync(timeout: TimeSpan.FromSeconds(3), cancellationToken))
                {
                    if (!aligned)
                    {
                        await AlignSteamTimeAsync(cancellationToken).ConfigureAwait(false);
                    }
                }
            }

            ulong timestamp = GetSystemTimestamp(DateTime.UtcNow.Add(TimeDifference));
            return timestamp;
        }

        /// <summary>
        /// 获取系统秒级时间戳
        /// </summary>
        /// <returns></returns>
        public static ulong GetSystemTimestamp()
        {
            return GetSystemTimestamp(DateTime.UtcNow);
        }

        /// <summary>
        /// 获取系统毫秒级时间戳
        /// </summary>
        /// <returns></returns>
        public static ulong GetSystemMilliTimestamp()
        {
            return GetSystemMilliTimestamp(DateTime.UtcNow);
        }

        /// <summary>
        /// 获取系统秒级时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static ulong GetSystemTimestamp(DateTime time)
        {
            ulong timestamp = (ulong)time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            return timestamp;
        }

        /// <summary>
        /// 获取系统毫秒级时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static ulong GetSystemMilliTimestamp(DateTime time)
        {
            ulong timestamp = (ulong)time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            return timestamp;
        }

        /// <summary>
        /// 获取DeviceId
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <returns></returns>
        public static string GetDeviceId(this string steamId)
        {
            var sha = SHA1.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(steamId));
            string deviceId = Utils.ByteArrayToHexString(bytes).Substring(0, 32);

            deviceId = Regex.Replace(deviceId, "^([0-9a-f]{8})([0-9a-f]{4})([0-9a-f]{4})([0-9a-f]{4})([0-9a-f]{12}).*$", "$1-$2-$3-$4-$5");

            return $"android:{deviceId}";
        }

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="cookies">Cookie字符串</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static CookieCollection GetCookies(this string cookies, Language language = Language.None)
        {
            string languageCode = language.GetApiCode();

            var cookieCollection = new CookieCollection();
            string[] array;
            string key;
            string value;
            foreach (string item in cookies.Split(new string[] { HttpUtils.CookieSplit }, StringSplitOptions.RemoveEmptyEntries).Where(c => !string.IsNullOrWhiteSpace(c)))
            {
                string cookie = item.Split(new string[] { HttpUtils.CookieSplit }, StringSplitOptions.RemoveEmptyEntries)[0];
                array = cookie.Split('=');
                key = array[0];
                value = Uri.UnescapeDataString(array[1]);
                if (key.Equals(SteamLanguageCookeName, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(languageCode))
                    {
                        languageCode = value;
                    }
                    continue;
                }

                cookieCollection.Add(new Cookie(key, value));
            }

            cookieCollection.Add(new Cookie(SteamLanguageCookeName, languageCode));
            return cookieCollection;
        }

        /// <summary>
        /// 设置语言类型
        /// </summary>
        /// <param name="cookies">Cookie</param>
        /// <param name="language">语言类型</param>
        /// <returns></returns>
        public static CookieCollection SetLanguage(this CookieCollection cookies, Language language)
        {
            string languageCode = language.GetApiCode();

            var cookie = cookies[SteamLanguageCookeName];
            if (cookie != null)
            {
                cookies.Remove(cookie);
            }

            if (string.IsNullOrWhiteSpace(languageCode))
            {
                return cookies;
            }

            cookie = new Cookie(SteamLanguageCookeName, languageCode, cookie?.Domain, cookie?.Path);
            cookies.Add(cookie);

            return cookies;
        }

        /// <summary>
        /// 设置交易资格校验结果
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static CookieCollection SetWebTradeEligibility(this CookieCollection cookies)
        {
            var tradeEligibility = GenerateWebTradeEligibility();

            var cookie = cookies[WebTradeEligibilityCookeName];
            if (cookie != null)
            {
                cookies.Remove(cookie);
            }

            cookie = new Cookie(WebTradeEligibilityCookeName, JsonConvert.SerializeObject(tradeEligibility, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }), cookie?.Domain, cookie?.Path);
            cookies.Add(cookie);

            return cookies;
        }

        /// <summary>
        /// 获取SteamToken
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public static SteamToken GetSteamToken(this string token)
        {
            try
            {
                var tokenComponents = token.Split('.');
                var base64 = tokenComponents[1].Replace('-', '+').Replace('_', '/');
                if (base64.Length % 4 != 0)
                {
                    base64 += new string('=', 4 - base64.Length % 4);
                }
                var payloadBytes = Convert.FromBase64String(base64);

                SteamToken steamToken = JsonConvert.DeserializeObject<SteamToken>(Encoding.UTF8.GetString(payloadBytes))!;
                return steamToken;
            }
            catch
            {
                throw new SteamTokenException("无效的Token", nameof(token));
            }
        }

        /// <summary>
        /// 从Cookie获取SessionId
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string? GetSessionId(this CookieCollection cookies)
        {
            string? sessionId = cookies?.FirstOrDefault(c => c.Name.Equals(SteamSessionidCookieName, StringComparison.CurrentCultureIgnoreCase))?.Value;
            return sessionId;
        }

        /// <summary>
        /// Partner转SteamId
        /// </summary>
        /// <param name="partner">Partner</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static string GetSteamId(this string partner)
        {
            if (!ulong.TryParse(partner, out ulong partnerId))
            {
                throw new NotSupportedException("不支持的partner格式");
            }

            ulong steamId = partnerId + DefaultSteamId;
            return steamId.ToString();
        }

        /// <summary>
        /// SteamId转Partner
        /// </summary>
        /// <param name="steamId">SteamId</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static string GetPartner(this string steamId)
        {
            if (!ulong.TryParse(steamId, out ulong steam))
            {
                throw new NotSupportedException("不支持的steamId格式");
            }

            ulong partner = steam - DefaultSteamId;
            return partner.ToString();
        }

        /// <summary>
        /// 获取检视参数
        /// </summary>
        /// <param name="inspectLink">检视链接</param>
        /// <param name="param_s"></param>
        /// <param name="param_m"></param>
        /// <param name="param_a"></param>
        /// <param name="param_d"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void GetInspectParameter(string inspectLink, out ulong param_s, out ulong param_m, out ulong param_a, out ulong param_d)
        {
            Match a = new Regex(@"A(\d+)").Match(inspectLink);
            Match d = new Regex(@"D(\d+)").Match(inspectLink);
            Match s = new Regex(@"S(\d+)").Match(inspectLink);
            Match m = new Regex(@"M(\d+)").Match(inspectLink);
            if (!a.Success || !d.Success || (!s.Success && !m.Success))
            {
                throw new ArgumentException("检视链接格式错误", nameof(inspectLink));
            }

            param_a = Convert.ToUInt64(a.Groups[1].Value);
            param_d = Convert.ToUInt64(d.Groups[1].Value);
            param_s = 0;
            param_m = 0;
            if (s.Success)
            {
                param_s = Convert.ToUInt64(s.Groups[1].Value);
            }
            else
            {
                param_m = Convert.ToUInt64(m.Groups[1].Value);
            }
        }

        /// <summary>
        /// 获取Steam授权登录参数
        /// </summary>
        /// <param name="url">授权返地址</param>
        /// <returns></returns>
        public static IDictionary<string, string> InitOpenIdAuthParameter(this Uri url)
        {
            var returnUrl = $"{url}";
            var match = Regex.Match(returnUrl, @"^(?<realm>https?://[^:/]+)");
            if (!match.Success)
            {
                throw new ArgumentException("授权返回地址错误", nameof(returnUrl));
            }

            var realm = match.Groups["realm"].Value;
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "openid.claimed_id", "http://specs.openid.net/auth/2.0/identifier_select" },
                { "openid.identity", "http://specs.openid.net/auth/2.0/identifier_select" },
                { "openid.mode", "checkid_setup" },
                { "openid.ns", "http://specs.openid.net/auth/2.0" },
                { "openid.realm", $"{realm}" },
                { "openid.return_to", $"{returnUrl}" }
            };
            return parameters;
        }

        /// <summary>
        /// 商品ClassInfo转资产描述
        /// </summary>
        /// <param name="assetClassInfo">商品ClassInfo</param>
        /// <param name="appId">游戏Id</param>
        /// <returns></returns>
        public static BaseDescription ToBaseDescription(this AssetClassInfo assetClassInfo, string appId)
        {
            BaseDescription result = new BaseDescription
            {
                AppId = appId,
                MarketHashName = assetClassInfo.MarketHashName,
                MarketName = assetClassInfo.MarketName,
                Name = assetClassInfo.Name,
                NameColor = assetClassInfo.NameColor,
                Type = assetClassInfo.Type,
                ClassId = assetClassInfo.ClassId,
                InstanceId = assetClassInfo.InstanceId,
                Tradable = assetClassInfo.Tradable,
                Marketable = assetClassInfo.Marketable,
                Commodity = assetClassInfo.Commodity,
                IconUrl = assetClassInfo.IconUrl,
            };
            return result;
        }

        /// <summary>
        /// 商品ClassInfo转资产描述
        /// </summary>
        /// <param name="assetClassInfo">商品ClassInfo</param>
        /// <param name="appId">游戏Id</param>
        /// <param name="includeOwnerDescriptions">返回值是否包含资产私有描述信息</param>
        /// <returns></returns>
        public static TagDescription ToTagDescription(this AssetClassInfo assetClassInfo, string appId, bool includeOwnerDescriptions)
        {
            IEnumerable<AssetDescription> descriptions = assetClassInfo.Descriptions.Values;
            if (!includeOwnerDescriptions)
            {
                descriptions = descriptions.Where(c => !IsOwnerDescription(c));
            }

            TagDescription result = new TagDescription
            {
                AppId = appId,
                MarketHashName = assetClassInfo.MarketHashName,
                MarketName = assetClassInfo.MarketName,
                Name = assetClassInfo.Name,
                NameColor = assetClassInfo.NameColor,
                Type = assetClassInfo.Type,
                ClassId = assetClassInfo.ClassId,
                InstanceId = assetClassInfo.InstanceId,
                Tradable = assetClassInfo.Tradable,
                Marketable = assetClassInfo.Marketable,
                Commodity = assetClassInfo.Commodity,
                IconUrl = assetClassInfo.IconUrl,
                Actions = new List<AssetAction>(),

                Descriptions = descriptions.ToList(),

                OwnerDescriptions = new List<AssetDescription>(),

                Tags = assetClassInfo.Tags.Select(c => new TagDescription.Tag
                {
                    Category = c.Value.Category,
                    InternalName = c.Value.InternalName,
                    LocalizedCategoryName = c.Value.CategoryName,
                    LocalizedTagName = c.Value.Name,
                    Color = c.Value.Color
                }).ToList()
            };
            return result;
        }

        /// <summary>
        /// 移除资产私有描述信息
        /// </summary>
        /// <param name="assetClassInfo"></param>
        /// <returns></returns>
        public static void RemoveOwnerDescriptions(this AssetClassInfo assetClassInfo)
        {
            assetClassInfo.OwnerDescriptions = new Dictionary<int, AssetDescription>();
            assetClassInfo.Descriptions = new Dictionary<int, AssetDescription>(assetClassInfo.Descriptions.Where(c => !IsOwnerDescription(c.Value)).ToList());
        }

        /// <summary>
        /// 报价状态转换
        /// </summary>
        /// <param name="status">报价状态</param>
        /// <returns></returns>
        public static OfferStatus ToOfferStatus(this TradeOfferState status)
        {
            switch (status)
            {
                case TradeOfferState.Canceled:
                case TradeOfferState.CanceledBySecondFactor:
                    return OfferStatus.主动取消;

                case TradeOfferState.Declined:
                    return OfferStatus.对方拒绝;

                case TradeOfferState.Countered:
                    return OfferStatus.对方修改报价;

                case TradeOfferState.Expired:
                    return OfferStatus.交易过期;

                case TradeOfferState.Invalid:
                case TradeOfferState.InvalidItems:
                    return OfferStatus.交易失效;

                case TradeOfferState.Accepted:
                    return OfferStatus.交易成功;

                case TradeOfferState.Active:
                    return OfferStatus.等待接受;

                case TradeOfferState.NeedsConfirmation:
                    return OfferStatus.等待令牌确认;

                case TradeOfferState.InEscrow:
                    return OfferStatus.交易暂挂;

                case TradeOfferState.Unknown:
                default:
                    return OfferStatus.未知;
            }
        }

        /// <summary>
        /// 获取资产可交易时间
        /// </summary>
        /// <param name="description">可交易时间描述信息</param>
        /// <returns></returns>
        public static DateTime? GetAssetTradableExpires(string description)
        {
            //2023 7月 15 (7:00:00) 格林尼治标准时间 后可交易
            var match = Regex.Match(description, @"(\d{4}) (\d{1,2})月 (\d{1,2}) \((\d{1,2}):(\d{1,2}):(\d{1,2})\) 格林尼治标准时间 后可交易");
            if (match.Success)
            {
                var timeValue = match.Value;
                string format = "yyyy M月 d (H:m:s) 格林尼治标准时间 后可交易";
                if (!DateTime.TryParseExact(timeValue, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var time))
                {
                    return null;
                }

                return time.ToLocalTime();
            }

            //2025 7月 23 (7:00:00) 格林尼治标准时间之前不能被消耗、改造或转让
            match = Regex.Match(description, @"(\d{4}) (\d{1,2})月 (\d{1,2}) \((\d{1,2}):(\d{1,2}):(\d{1,2})\) 格林尼治标准时间之前不能被消耗、改造或转让");
            if (match.Success)
            {
                var timeValue = match.Value;
                string format = "yyyy M月 d (H:m:s) 格林尼治标准时间之前不能被消耗、改造或转让";
                if (!DateTime.TryParseExact(timeValue, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var time))
                {
                    return null;
                }

                return time.ToLocalTime();
            }

            //Monday, September 30, 2024 (7:00:00) 格林尼治标准时间 后可交易
            match = Regex.Match(description, @"(.+?), (.+?) (\d{1,2}), (\d{4}) \((\d{1,2}):(\d{1,2}):(\d{1,2})\) 格林尼治标准时间 后可交易");
            if (match.Success)
            {
                var timeValue = match.Value;
                string format = "dddd, MMMM d, yyyy (H:m:s) 格林尼治标准时间 后可交易";
                if (!DateTime.TryParseExact(timeValue, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var time))
                {
                    return null;
                }

                return time.ToLocalTime();
            }

            //开放交易时间：2023年7月24日 (10:00:00)
            match = Regex.Match(description, @"开放交易时间：(\d{4})年(\d{1,2})月(\d{1,2})日 \((\d{1,2}):(\d{1,2}):(\d{1,2})\)");
            if (match.Success)
            {
                var timeValue = match.Value;
                string format = "开放交易时间：yyyy年M月d日 (H:m:s)";
                if (!DateTime.TryParseExact(timeValue, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var time))
                {
                    return null;
                }

                return time;
            }

            //开放交易时间：Mon Sep 30 23:00:00 2024
            match = Regex.Match(description, @"开放交易时间：(.+?) (.+?) (\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2}) (\d{4})");
            if (match.Success)
            {
                var timeValue = match.Value;
                string format = "开放交易时间：ddd MMM d H:m:s yyyy";
                if (!DateTime.TryParseExact(timeValue, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var time))
                {
                    return null;
                }

                return time;
            }

            return null;
        }

        /// <summary>
        /// 生成交易资格检查数据
        /// </summary>
        /// <returns></returns>
        public static object GenerateWebTradeEligibility()
        {
            var timestamp = GetSystemTimestamp();

            var obj = new
            {
                allowed = 1,
                allowed_at_time = 0,
                steamguard_required_days = 15,
                new_device_cooldown_days = 0,
                time_checked = timestamp
            };
            return obj;
        }

        /// <summary>
        /// 对齐Steam时间
        /// </summary>
        /// <param name="cancellationToken"></param>
        internal static async Task AlignSteamTimeAsync(CancellationToken cancellationToken = default)
        {
            ulong now = GetSystemTimestamp();
            var serverTimeResponse = await SteamAuthenticator.QueryServerTimeAsync((ulong)now, cancellationToken).ConfigureAwait(false);
            var serverTime = serverTimeResponse.Body?.server_time;
            if (serverTime > 0)
            {
                TimeDifference = TimeSpan.FromSeconds(serverTime.Value) - TimeSpan.FromSeconds(now);
                aligned = true;
            }
        }

        /// <summary>
        /// 是否是资产私有描述信息
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private static bool IsOwnerDescription(AssetDescription description)
        {
            return "sticker_info".Equals(description.Name, StringComparison.OrdinalIgnoreCase) ||
                "keychain_info".Equals(description.Name, StringComparison.OrdinalIgnoreCase) ||
                "nametag".Equals(description.Name, StringComparison.OrdinalIgnoreCase) ||
                "attr: keychain slot 0 seed".Equals(description.Name, StringComparison.OrdinalIgnoreCase) ||
                "stattrak_type".Equals(description.Name, StringComparison.OrdinalIgnoreCase) ||
                "stattrak_score".Equals(description.Name, StringComparison.OrdinalIgnoreCase) ||
                "stattrak_warn".Equals(description.Name, StringComparison.OrdinalIgnoreCase) ||
                description.Value.Contains("</div>", StringComparison.OrdinalIgnoreCase);
        }
    }
}
