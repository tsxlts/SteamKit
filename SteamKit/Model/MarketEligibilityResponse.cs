using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 市场资格响应
    /// </summary>
    public class MarketEligibilityResponse
    {
        /// <summary>
        /// 不被允许的原因原因
        /// </summary>
        [JsonProperty("reason")]
        public EligibilityReason Reason { get; set; }

        /// <summary>
        /// 是否允许使用
        /// </summary>
        [JsonProperty("allowed")]
        public int Allowed { get; set; }

        /// <summary>
        /// 被允许使用的时间
        /// </summary>
        [JsonProperty("allowed_at_time")]
        public long AllowedTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("steamguard_required_days")]
        public int SteamguardRequiredDays { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("new_device_cooldown_days")]
        public int NewDeviceCooldownDays { get; set; }

        /// <summary>
        /// 检测时间
        /// </summary>
        [JsonProperty("time_checked")]
        public long TimeChecked { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
        public long? Expiration { get; set; }

        /// <summary>
        /// 未登录时此值为0
        /// </summary>
        [JsonProperty("forms_requested", NullValueHandling = NullValueHandling.Ignore)]
        public int? FormsRequested { get; set; }

        /// <summary>
        /// 是否允许使用
        /// </summary>
        /// <returns></returns>
        public bool IsAllowed()
        {
            if (Allowed == 1 && FormsRequested != 0)
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 市场资格校验原因
    /// </summary>
    [Flags]
    public enum EligibilityReason
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 临时故障
        /// </summary>
        TemporaryFailure = 1 << 0,

        /// <summary>
        /// 帐户已关闭，可能无法登录
        /// </summary>
        AccountDisabled = 1 << 1,

        /// <summary>
        /// 您的帐户当前已锁定
        /// </summary>
        AccountLockedDown = 1 << 2,

        /// <summary>
        /// 您的帐户受限
        /// </summary>
        AccountLimited = 1 << 3,

        /// <summary>
        /// 您的帐户目前被禁止交易
        /// </summary>
        TradeBanned = 1 << 4,

        /// <summary>
        /// 对账户的信任度很低；交易取消的例子
        /// </summary>
        AccountNotTrusted = 1 << 5,

        /// <summary>
        /// 您最近重置或启用了Steam Guard，启用Steam Guard后必须等待15天
        /// </summary>
        SteamGuardNotEnabled = 1 << 6,

        /// <summary>
        /// 您的帐户必须受到Steam Guard的保护至少15天
        /// </summary>
        SteamGuardOnlyRecentlyEnabled = 1 << 7,

        /// <summary>
        /// 您的帐户密码最近已重置
        /// </summary>
        RecentPasswordReset = 1 << 8,

        /// <summary>
        /// 增加了新的付款方式；例如，Visa卡连接到您的帐户
        /// </summary>
        NewPaymentMethod = 1 << 9,

        /// <summary>
        /// Steam在使用浏览器cookie时遇到问题
        /// </summary>
        InvalidCookie = 1 << 10,

        /// <summary>
        /// 您正在从7天内未受Steam Guard保护的设备登录Steam
        /// </summary>
        UsingNewDevice = 1 << 11,

        /// <summary>
        /// 最近有什么东西被拒绝了（游戏？主题？）
        /// </summary>
        RecentSelfRefund = 1 << 12,

        /// <summary>
        /// 付款方式绑定后发生错误；例如，银行拒绝了链接（？）
        /// </summary>
        NewPaymentMethodCannotBeVerified = 1 << 13,

        /// <summary>
        /// 您必须拥有有效的Steam购买，购买时间在7天至一年之间，最近没有退款或付款纠纷
        /// </summary>
        NoRecentPurchases = 1 << 14,

        /// <summary>
        /// 
        /// </summary>
        AcceptedWalletGift = 1 << 15,

        /// <summary>
        /// 撤销交易的行为
        /// </summary>
        RevokeTrade = 1 << 16
    }
}
