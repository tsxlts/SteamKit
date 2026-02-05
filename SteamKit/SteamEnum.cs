using System.ComponentModel;
using SteamKit.Attributes;

namespace SteamKit
{
    /// <summary>
    /// Enum
    /// </summary>
    public class SteamEnum
    {
        /// <summary>
        /// 网站
        /// </summary>
        [Flags]
        public enum Website
        {
            /// <summary>
            /// 社区
            /// </summary>
            Community = 1 << 0,

            /// <summary>
            /// 商店
            /// </summary>
            Store = 1 << 1,

            /// <summary>
            /// 客服
            /// </summary>
            Help = 1 << 2,

            /// <summary>
            /// 收银台
            /// </summary>
            Checkout = 1 << 3,
        }

        /// <summary>
        /// Language
        /// </summary>
        public enum Language
        {
            /// <summary>
            /// 未设置
            /// </summary>
            [Language(apiCode: "", webApiCode: "")]
            None = -1,

            /// <summary>
            /// 英语
            /// English
            /// </summary>
            [Language(apiCode: "english", webApiCode: "en", Name = "English")]
            English = 0,

            /// <summary>
            /// 德语
            /// Deutsch
            /// </summary>
            [Language(apiCode: "german", webApiCode: "de", Name = "Deutsch")]
            German = 1,

            /// <summary>
            /// 法语
            /// Français
            /// </summary>
            [Language(apiCode: "french", webApiCode: "fr", Name = "Français")]
            French = 2,

            /// <summary>
            /// 意大利语
            /// Italiano
            /// </summary>
            [Language(apiCode: "italian", webApiCode: "it", Name = "Italiano")]
            Italian = 3,

            /// <summary>
            /// 韩语
            /// 한국어
            /// </summary>
            [Language(apiCode: "koreana", webApiCode: "ko", Name = "한국어")]
            Koreana = 4,

            /// <summary>
            /// 西班牙语-西班牙
            /// Español-España
            /// </summary>
            [Language(apiCode: "spanish", webApiCode: "es", Name = "Español-España")]
            Spanish = 5,

            /// <summary>
            /// 简体中文
            /// </summary>
            [Language(apiCode: "schinese", webApiCode: "zh-CN", Name = "简体中文")]
            Schinese = 6,

            /// <summary>
            /// 繁体中文
            /// 繁體中文
            /// </summary>
            [Language(apiCode: "tchinese", webApiCode: "zh-TW", Name = "繁體中文")]
            Tchinese = 7,

            /// <summary>
            /// 俄语
            /// Русский
            /// </summary>
            [Language(apiCode: "russian", webApiCode: "ru", Name = "Русский")]
            Russian = 8,

            /// <summary>
            /// 泰语
            /// ไทย
            /// </summary>
            [Language(apiCode: "thai", webApiCode: "th", Name = "ไทย")]
            Thai = 9,

            /// <summary>
            /// 日语
            /// 日本語
            /// </summary>
            [Language(apiCode: "japanese", webApiCode: "ja", Name = "日本語")]
            Japanese = 10,

            /// <summary>
            /// 葡萄牙语-葡萄牙
            /// Português
            /// </summary>
            [Language(apiCode: "portuguese", webApiCode: "pt", Name = "Português")]
            Portuguese = 11,

            /// <summary>
            /// 波兰语
            /// Polski
            /// </summary>
            [Language(apiCode: "polish", webApiCode: "pl", Name = "Polski")]
            Polish = 12,

            /// <summary>
            /// 丹麦语
            /// Dansk
            /// </summary>
            [Language(apiCode: "danish", webApiCode: "da", Name = "Dansk")]
            Danish = 13,

            /// <summary>
            /// 荷兰语
            /// Nederlands
            /// </summary>
            [Language(apiCode: "dutch", webApiCode: "nl", Name = "Nederlands")]
            Dutch = 14,

            /// <summary>
            /// 芬兰语
            /// Suomi
            /// </summary>
            [Language(apiCode: "finnish", webApiCode: "fi", Name = "Suomi")]
            Finnish = 15,

            /// <summary>
            /// 挪威语
            /// Norsk
            /// </summary>
            [Language(apiCode: "norwegian", webApiCode: "no", Name = "Norsk")]
            Norwegian = 16,

            /// <summary>
            /// 瑞典语
            /// Svenska
            /// </summary>
            [Language(apiCode: "swedish", webApiCode: "sv", Name = "Svenska")]
            Swedish = 17,

            /// <summary>
            /// 匈牙利语
            /// Magyar
            /// </summary>
            [Language(apiCode: "hungarian", webApiCode: "hu", Name = "Magyar")]
            Hungarian = 18,

            /// <summary>
            /// 捷克语
            /// čeština
            /// </summary>
            [Language(apiCode: "czech", webApiCode: "cs", Name = "čeština")]
            Czech = 19,

            /// <summary>
            /// 罗马尼亚语
            /// Română
            /// </summary>
            [Language(apiCode: "romanian", webApiCode: "ro", Name = "Română")]
            Romanian = 20,

            /// <summary>
            /// 土耳其语
            /// Türkçe
            /// </summary>
            [Language(apiCode: "turkish", webApiCode: "tr", Name = "Türkçe")]
            Turkish = 21,

            /// <summary>
            /// 葡萄牙语-巴西
            /// Português-Brasil
            /// </summary>
            [Language(apiCode: "brazilian", webApiCode: "pt-BR", Name = "Português-Brasil")]
            Brazilian = 22,

            /// <summary>
            /// 保加利亚语
            /// български език
            /// </summary>
            [Language(apiCode: "bulgarian", webApiCode: "bg", Name = "български език")]
            Bulgarian = 23,

            /// <summary>
            /// 希腊语
            /// Ελληνικά
            /// </summary>
            [Language(apiCode: "greek", webApiCode: "el", Name = "Ελληνικά")]
            Greek = 24,

            /// <summary>
            /// 乌克兰语
            /// Українська
            /// </summary>
            [Language(apiCode: "ukrainian", webApiCode: "uk", Name = "Українська")]
            Ukrainian = 25,

            /// <summary>
            /// 西班牙语-拉丁美洲
            /// Español-Latinoamérica
            /// </summary>
            [Language(apiCode: "latam", webApiCode: "es-419", Name = "Español-Latinoamérica")]
            Latam = 26,

            /// <summary>
            /// 越南语
            /// Tiếng Việt
            /// </summary>
            [Language(apiCode: "vietnamese", webApiCode: "vi", Name = "iếng Việt")]
            Vietnamese = 27,

            /// <summary>
            /// 印度尼西亚语
            /// Bahasa Indonesia
            /// </summary>
            [Language(apiCode: "indonesian", webApiCode: "id", Name = "Bahasa Indonesia")]
            Indonesian = 28,
        }

        /// <summary>
        /// Currency
        /// </summary>
        public enum Currency
        {
            /// <summary>
            /// 
            /// </summary>
            Invalid = 0,

            /// <summary>
            /// 美元
            /// </summary>
            [Description("$")]
            USD = 1,

            /// <summary>
            /// 英镑
            /// </summary>
            [Description("£")]
            GBP = 2,

            /// <summary>
            /// 欧元
            /// </summary>
            [Description("€")]
            EUR = 3,

            /// <summary>
            /// 瑞士法郎
            /// </summary>
            [Description("CHF")]
            CHF = 4,

            /// <summary>
            /// 俄罗斯卢布
            /// </summary>
            [Description("pуб.")]
            RUB = 5,

            /// <summary>
            /// 波兰兹罗提
            /// </summary>
            [Description("zł")]
            PLN = 6,

            /// <summary>
            /// 巴西雷亚尔
            /// </summary>
            [Description("R$")]
            BRL = 7,

            /// <summary>
            /// 日圆
            /// </summary>
            [Description("¥")]
            JPY = 8,

            /// <summary>
            /// 罗威克朗
            /// </summary>
            [Description("kr")]
            NOK = 9,

            /// <summary>
            /// 印尼卢比
            /// </summary>
            [Description("Rp")]
            IDR = 10,

            /// <summary>
            /// 马来西亚林吉特
            /// </summary>
            [Description("RM")]
            MYR = 11,

            /// <summary>
            /// 菲律宾比索
            /// </summary>
            [Description("P")]
            PHP = 12,

            /// <summary>
            /// 新加坡元
            /// </summary>
            [Description("S$")]
            SGD = 13,

            /// <summary>
            /// 泰铢
            /// </summary>
            [Description("฿")]
            THB = 14,

            /// <summary>
            /// 越南盾
            /// </summary>
            [Description("₫")]
            VND = 15,

            /// <summary>
            /// 韩元
            /// </summary>
            [Description("₩")]
            KRW = 16,

            /// <summary>
            /// 新土耳其里拉
            /// </summary>
            [Description("TL")]
            TRY = 17,

            /// <summary>
            /// 乌克兰格里夫纳
            /// </summary>
            [Description("₴")]
            UAH = 18,

            /// <summary>
            /// 墨西哥比索
            /// </summary>
            [Description("Mex$")]
            MXN = 19,

            /// <summary>
            /// 加拿大元
            /// </summary>
            [Description("CDN$")]
            CAD = 20,

            /// <summary>
            /// 澳元
            /// </summary>
            [Description("A$")]
            AUD = 21,

            /// <summary>
            /// 新西兰元
            /// </summary>
            [Description("NZ$")]
            NZD = 22,

            /// <summary>
            /// 人民币
            /// </summary>
            [Description("¥")]
            CNY = 23,

            /// <summary>
            /// 印度卢比
            /// </summary>
            [Description("₹")]
            INR = 24,

            /// <summary>
            /// 智利比索
            /// </summary>
            [Description("CLP$")]
            CLP = 25,

            /// <summary>
            /// 秘鲁新索尔
            /// </summary>
            [Description("S\\/.")]
            PEN = 26,

            /// <summary>
            /// 哥伦比亚比索
            /// </summary>
            [Description("COL$")]
            COP = 27,

            /// <summary>
            /// 南非兰特
            /// </summary>
            [Description("R")]
            ZAR = 28,

            /// <summary>
            /// 港币
            /// </summary>
            [Description("HK$")]
            HKD = 29,

            /// <summary>
            /// 台币
            /// </summary>
            [Description("NT$")]
            TWD = 30,

            /// <summary>
            /// 沙特里亚尔
            /// </summary>
            [Description("SR")]
            SAR = 31,

            /// <summary>
            /// 阿联酋迪拉姆
            /// </summary>
            [Description("AED")]
            AED = 32,

            /// <summary>
            /// 阿根廷比索
            /// </summary>
            [Description("ARS$")]
            ARS = 34,

            /// <summary>
            /// 以色列新谢克尔
            /// </summary>
            [Description("₪")]
            ILS = 35,

            /// <summary>
            /// 
            /// </summary>
            BYN = 36,

            /// <summary>
            /// 哈萨克斯坦坚戈
            /// </summary>
            [Description("₸")]
            KZT = 37,

            /// <summary>
            /// 科威特第纳尔
            /// </summary>
            [Description("KD")]
            KWD = 38,

            /// <summary>
            /// 卡塔尔里亚尔
            /// </summary>
            [Description("QR")]
            QAR = 39,

            /// <summary>
            /// 哥斯达黎加科朗
            /// </summary>
            [Description("₡")]
            CRC = 40,

            /// <summary>
            /// 乌拉圭比索
            /// </summary>
            [Description("$U")]
            UYU = 41,

            /// <summary>
            /// 保加利亚列弗
            /// </summary>
            [Description("лв")]
            BGN = 42,

            /// <summary>
            /// 克罗地亚库纳
            /// </summary>
            [Description("kn")]
            HRK = 43,

            /// <summary>
            /// 捷克克朗
            /// </summary>
            [Description("Kč‌")]
            CZK = 44,

            /// <summary>
            /// 丹麦克朗
            /// </summary>
            [Description("kr‌")]
            DKK = 45,

            /// <summary>
            /// 匈牙利福林
            /// </summary>
            [Description("Ft")]
            HUF = 46,

            /// <summary>
            /// 列伊
            /// </summary>
            [Description("RON")]
            RON = 47,
        }

        /// <summary>
        /// 好友关系
        /// </summary>
        public enum FriendRelationship
        {
            /// <summary>
            /// 
            /// </summary>
            None = 0,

            /// <summary>
            /// 
            /// </summary>
            Blocked = 1,

            /// <summary>
            /// 
            /// </summary>
            RequestRecipient = 2,

            /// <summary>
            /// 
            /// </summary>
            Friend = 3,

            /// <summary>
            /// 
            /// </summary>
            RequestInitiator = 4,

            /// <summary>
            /// 
            /// </summary>
            Ignored = 5,

            /// <summary>
            /// 
            /// </summary>
            IgnoredFriend = 6,
        }

        /// <summary>
        /// 用户状态
        /// </summary>
        public enum PersonaState
        {
            /// <summary>
            /// 离线
            /// </summary>
            Offline = 0,

            /// <summary>
            /// 在线
            /// </summary>
            Online = 1,

            /// <summary>
            /// 忙碌
            /// </summary>
            Busy = 2,

            /// <summary>
            /// 离开
            /// </summary>
            Away = 3,

            /// <summary>
            /// 暂停
            /// </summary>
            Snooze = 4,

            /// <summary>
            /// 想要交易
            /// </summary>
            LookingToTrade = 5,

            /// <summary>
            /// 想要玩
            /// </summary>
            LookingToPlay = 6
        }

        /// <summary>
        /// 用户个人资料状态
        /// </summary>
        public enum CommunityVisibilityState
        {
            /// <summary>
            /// 
            /// </summary>
            私密 = 1,

            /// <summary>
            /// 
            /// </summary>
            仅好友 = 2,

            /// <summary>
            /// 
            /// </summary>
            公开 = 3
        }

        /// <summary>
        /// 留言权限
        /// </summary>
        public enum CommentPermission
        {
            /// <summary>
            /// 
            /// </summary>
            仅好友 = 0,

            /// <summary>
            /// 
            /// </summary>
            公开 = 1,

            /// <summary>
            /// 
            /// </summary>
            私密 = 2,
        }

        /// <summary>
        /// 待确认信息类型
        /// </summary>
        public enum ConfirmationType
        {
            /// <summary>
            /// Invalid
            /// </summary>
            Invalid = 0,

            /// <summary>
            /// Generic
            /// <para>通用的确认信息</para>
            /// </summary>
            Generic = 1,

            /// <summary>
            /// Trade
            /// <para>交易</para>
            /// </summary>
            Trade = 2,

            /// <summary>
            /// Market Listing
            /// <para>市场上架</para>
            /// </summary>
            MarketListing = 3,

            /// <summary>
            /// FeatureOptOut
            /// </summary>
            FeatureOptOut = 4,

            /// <summary>
            /// Change Phone Number
            /// <para>更改手机号码</para>
            /// </summary>
            ChangePhoneNumber = 5,

            /// <summary>
            /// Account Recovery
            /// <para>帐户恢复</para>
            /// 修改密码
            /// 修改邮箱等
            /// </summary>
            AccountRecovery = 6,

            /// <summary>
            /// Register API Key
            /// <para>注册 API 密钥</para>
            /// </summary>
            RegisterApiKey = 9,

            /// <summary>
            /// Join Family
            /// <para>加入家庭</para>
            /// </summary>
            JoinFamily = 11,

            /// <summary>
            /// Account Security
            /// <para>帐户安全</para>
            /// </summary>
            AccountSecurity = 12
        }

        /// <summary>
        /// 登录方式
        /// </summary>
        public enum LoginType
        {
            /// <summary>
            /// 匿名登录
            /// </summary>
            Anonymous = 0,

            /// <summary>
            /// 用户帐号密码登录
            /// </summary>
            UserName = 101,

            /// <summary>
            /// 扫码授权登录
            /// </summary>
            QrCode = 102,

            /// <summary>
            /// RefreshToken登录
            /// </summary>
            RefreshToken = 202,

            /// <summary>
            /// 网页登录Token登录
            /// </summary>
            WebToken = 301,

            /// <summary>
            /// 游戏服务器登录
            /// </summary>
            GameServer = 901
        }

        /// <summary>
        /// 状态
        /// </summary>
        public enum AddAuthenticatorStatus
        {
            /// <summary>
            /// 等待手机验证码确认
            /// </summary>
            AwaitingFinalization = 1,

            /// <summary>
            /// 需要设置手机号
            /// </summary>
            MustProvidePhoneNumber = 2,

            /// <summary>
            /// 已经绑定验证器
            /// </summary>
            AuthenticatorPresent = 29
        }

        /// <summary>
        /// 验证方式
        /// </summary>
        public enum AddAuthenticatorConfirmType
        {
            /// <summary>
            /// 短信验证码
            /// </summary>
            SmsCode = 1,

            /// <summary>
            /// 邮箱验证码
            /// </summary>
            EmailCode = 3
        }

        /// <summary>
        /// Steam令牌方案
        /// </summary>
        public enum SteamGuardScheme
        {
            /// <summary>
            /// 无
            /// </summary>
            None = 0,

            /// <summary>
            /// 邮箱令牌验证器
            /// </summary>
            Email = 1,

            /// <summary>
            /// 手机令牌验证器
            /// </summary>
            Device = 2
        }

        /// <summary>
        /// Steam通知目标
        /// </summary>
        public enum SteamNotificationTargets
        {
            /// <summary>
            /// 不通知
            /// </summary>
            None = 0,

            /// <summary>
            /// 全部通知
            /// </summary>
            All = 1,

            /// <summary>
            /// 通知SteamApp
            /// </summary>
            SteamApp = 3,

            /// <summary>
            /// 通知Steam PC客户端
            /// </summary>
            SteamClient = 9,

            /// <summary>
            /// 通知SteamApp和Steam PC客户端
            /// </summary>
            SteamAppAndSteamClient = 11
        }

        /// <summary>
        /// Steam通知类型
        /// </summary>
        public enum SteamNotificationType
        {
            /// <summary>
            /// 
            /// </summary>
            Test = 1,

            /// <summary>
            /// 收到礼物
            /// </summary>
            Gift = 2,

            /// <summary>
            /// 收到讨论区回复
            /// </summary>
            Comment = 3,

            /// <summary>
            /// 库存收到物品
            /// </summary>
            Item = 4,

            /// <summary>
            /// 收到好友邀请
            /// </summary>
            FriendInvite = 5,

            /// <summary>
            /// 大型特卖活动
            /// </summary>
            MajorSale = 6,

            /// <summary>
            /// 
            /// </summary>
            PreloadAvailable = 7,

            /// <summary>
            /// 愿望单物品有折扣
            /// </summary>
            Wishlist = 8,

            /// <summary>
            /// 收到交易报价
            /// </summary>
            TradeOffer = 9,

            /// <summary>
            /// 
            /// </summary>
            General = 10,

            /// <summary>
            /// 收到客服回复
            /// </summary>
            HelpRequest = 11,

            /// <summary>
            /// 收到Steam回合通知
            /// </summary>
            AsyncGame = 12,

            /// <summary>
            /// 聊天消息
            /// </summary>
            ChatMsg = 13,

            /// <summary>
            /// 
            /// </summary>
            ModeratorMsg = 14,

            /// <summary>
            /// 家庭儿童请求访问Steam
            /// </summary>
            ParentalFeatureAccessRequest = 15,

            /// <summary>
            /// 邀请我加入Steam家庭
            /// </summary>
            FamilyInvite = 16,

            /// <summary>
            /// 家庭儿童请求购买物品
            /// </summary>
            FamilyPurchaseRequest = 17,

            /// <summary>
            /// 家庭儿童申请额外的游戏时间
            /// </summary>
            ParentalPlaytimeRequest = 18,

            /// <summary>
            /// 家长同意我的购买请求
            /// </summary>
            FamilyPurchaseRequestResponse = 19,

            /// <summary>
            /// 家长同意我的访问Steam
            /// </summary>
            ParentalFeatureAccessResponse = 20,

            /// <summary>
            /// 家长回复我申请的额外游戏时间
            /// </summary>
            ParentalPlaytimeResponse = 21,

            /// <summary>
            /// 收到了请求购买的游戏
            /// </summary>
            RequestedGameAdded = 22,

            /// <summary>
            /// 
            /// </summary>
            SendToPhone = 23,

            /// <summary>
            /// 
            /// </summary>
            ClipDownloaded = 24,

            /// <summary>
            /// 
            /// </summary>
            _2FAPrompt = 25,

            /// <summary>
            /// 
            /// </summary>
            MobileConfirmation = 26,

            /// <summary>
            /// 
            /// </summary>
            PartnerEvent = 27,

            /// <summary>
            /// 收到了游戏测试或限定推出的邀请
            /// </summary>
            PlaytestInvite = 28,

            /// <summary>
            /// 
            /// </summary>
            TradeReversal = 29,
        }

        /// <summary>
        /// OSType
        /// </summary>
        public enum OSType
        {
            Unknown = -1,
            Web = -700,
            IOSUnknown = -600,
            IOS1 = -599,
            IOS2 = -598,
            IOS3 = -597,
            IOS4 = -596,
            IOS5 = -595,
            IOS6 = -594,
            IOS6_1 = -593,
            IOS7 = -592,
            IOS7_1 = -591,
            IOS8 = -590,
            IOS8_1 = -589,
            IOS8_2 = -588,
            IOS8_3 = -587,
            IOS8_4 = -586,
            IOS9 = -585,
            IOS9_1 = -584,
            IOS9_2 = -583,
            IOS9_3 = -582,
            IOS10 = -581,
            IOS10_1 = -580,
            IOS10_2 = -579,
            IOS10_3 = -578,
            IOS11 = -577,
            IOS11_1 = -576,
            IOS11_2 = -575,
            IOS11_3 = -574,
            IOS11_4 = -573,
            IOS12 = -572,
            IOS12_1 = -571,
            AndroidUnknown = -500,
            Android6 = -499,
            Android7 = -498,
            Android8 = -497,
            Android9 = -496,
            UMQ = -400,
            PS3 = -300,
            MacOSUnknown = -102,
            MacOS104 = -101,
            MacOS105 = -100,
            MacOS1058 = -99,
            MacOS106 = -95,
            MacOS1063 = -94,
            MacOS1064_slgu = -93,
            MacOS1067 = -92,
            MacOS107 = -90,
            MacOS108 = -89,
            MacOS109 = -88,
            MacOS1010 = -87,
            MacOS1011 = -86,
            MacOS1012 = -85,
            Macos1013 = -84,
            Macos1014 = -83,
            Macos1015 = -82,
            MacOS1016 = -81,
            MacOS11 = -80,
            MacOS111 = -79,
            MacOS1017 = -78,
            MacOS12 = -77,
            MacOS1018 = -76,
            MacOS13 = -75,
            MacOS1019 = -74,
            MacOS14 = -73,
            MacOS1020 = -72,
            MacOS15 = -71,
            MacOSMax = -1,
            LinuxUnknown = -203,
            Linux22 = -202,
            Linux24 = -201,
            Linux26 = -200,
            Linux32 = -199,
            Linux35 = -198,
            Linux36 = -197,
            Linux310 = -196,
            Linux316 = -195,
            Linux318 = -194,
            Linux3x = -193,
            Linux4x = -192,
            Linux41 = -191,
            Linux44 = -190,
            Linux49 = -189,
            Linux414 = -188,
            Linux419 = -187,
            Linux5x = -186,
            Linux54 = -185,
            Linux6x = -184,
            Linux7x = -183,
            Linux510 = -182,
            LinuxMax = -101,
            WinUnknown = 0,
            Win311 = 1,
            Win95 = 2,
            Win98 = 3,
            WinME = 4,
            WinNT = 5,
            Win2000 = 6,
            WinXP = 7,
            Win2003 = 8,
            WinVista = 9,
            Windows7 = 10,
            Win2008 = 11,
            Win2012 = 12,
            Windows8 = 13,
            Windows81 = 14,
            Win2012R2 = 15,
            Windows10 = 16,
            Win2016 = 17,
            Win2019 = 18,
            Win2022 = 19,
            Win11 = 20,
            WinMAX = 21,
        }

        /// <summary>
        /// 资产属性值类型
        /// </summary>
        public enum AssetPropertyType
        {
            /// <summary>
            /// 
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// 
            /// </summary>
            Float = 1,

            /// <summary>
            /// 
            /// </summary>
            Int = 2,

            /// <summary>
            /// 
            /// </summary>
            String = 3,

            /// <summary>
            /// 
            /// </summary>
            MAX = 4,
        }
    }
}
