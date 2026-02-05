using Newtonsoft.Json;

namespace SteamKit.Model
{
    /// <summary>
    /// 查询钱包余额响应
    /// </summary>
    public class QueryWalletDetailsResponse
    {
        /// <summary>
        /// 是否有钱包
        /// </summary>
        [JsonProperty("has_wallet")]
        public bool HasWallet { get; set; }

        /// <summary>
        /// 用户所在国家编码
        /// </summary>
        [JsonProperty("user_country_code")]
        public string UserCountryCode { get; set; } = string.Empty;

        /// <summary>
        /// 用户钱包所在国家编码
        /// </summary>
        [JsonProperty("wallet_country_code")]
        public string WalletCountryCode { get; set; } = string.Empty;

        /// <summary>
        /// 钱包状态
        /// </summary>
        [JsonProperty("wallet_state")]
        public string WalletState { get; set; } = string.Empty;

        /// <summary>
        /// 全部余额
        /// 单位：分
        /// </summary>
        [JsonProperty("balance")]
        public int Balance { get; set; }

        /// <summary>
        /// 锁定的余额
        /// 延时到账余额
        /// 单位：分
        /// </summary>
        [JsonProperty("delayed_balance")]
        public int DelayedBalance { get; set; }

        /// <summary>
        /// 钱包货币类型
        /// </summary>
        [JsonProperty("currency_code")]
        public SteamEnum.Currency CurrencyCode { get; set; }

        /// <summary>
        /// 最新更新时间
        /// </summary>
        [JsonProperty("time_most_recent_txn")]
        public long TimeMostRecentTxn { get; set; }

        /// <summary>
        /// 最新更新对应的交易Id
        /// </summary>
        [JsonProperty("most_recent_txnid")]
        public string? MostRecentTxnId { get; set; }

        /// <summary>
        /// 美元钱包余额
        /// 单位：美分
        /// </summary>
        [JsonProperty("balance_in_usd")]
        public int USDBalance { get; set; }

        /// <summary>
        /// 美元锁定的余额
        /// 延时到账余额
        /// 单位：美分
        /// </summary>
        [JsonProperty("delayed_balance_in_usd")]
        public int USDDelayedBalance { get; set; }

        /// <summary>
        /// 是否有其他地区钱包
        /// </summary>
        [JsonProperty("has_wallet_in_other_regions")]
        public bool HasOtherRegionsWallet { get; set; }

        /// <summary>
        /// 格式化余额
        /// </summary>
        [JsonProperty("formatted_balance")]
        public string FormattedBalance { get; set; } = string.Empty;

        /// <summary>
        /// 格式化锁定余额
        /// </summary>
        [JsonProperty("formatted_delayed_balance")]
        public string FormattedDelayedBalance { get; set; } = string.Empty;
    }
}
