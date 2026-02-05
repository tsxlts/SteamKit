using Newtonsoft.Json;
using static SteamKit.SteamEnum;

namespace SteamKit.Model
{
    /// <summary>
    /// 购买商品响应
    /// </summary>
    public class BuyAssetResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public ErrorCodes? Success { get; set; }

        /// <summary>
        /// 是否需要确认
        /// </summary>
        [JsonProperty("need_confirmation")]
        public bool NeedConfirmation { get; set; }

        /// <summary>
        /// 确认信息
        /// <para>NeedConfirmation为true时有值</para>
        /// <para>确认后传入此对象返回的确认Id继续完成购买</para>
        /// </summary>
        [JsonProperty("confirmation")]
        public BuyAssetConfirmation? Confirmation { get; set; }

        /// <summary>
        /// 钱包信息
        /// <para>购买成功后有值</para>
        /// </summary>
        [JsonProperty("wallet_info")]
        public BuyAssetWalletResponse? Result { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }
    }

    /// <summary>
    /// 购买商品返回信息
    /// </summary>
    public class BuyAssetWalletResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_currency")]
        public Currency WalletCurrency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_country")]
        public string WalletCountry { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_state")]
        public string WalletState { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_fee")]
        public int WalletFee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_fee_minimum")]
        public int WalletFeeMinimum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_fee_percent")]
        public decimal WalletFeePercent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_publisher_fee_percent_default")]
        public decimal WalletPublisherFeePercentDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_fee_base")]
        public int WalletFeeBase { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_balance")]
        public int WalletBalance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_delayed_balance")]
        public int WalletDelayedBalance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_max_balance")]
        public int WalletMaxBalance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("wallet_trade_max_balance")]
        public int WalletTradeMaxBalance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("rwgrsn")]
        public int rwgrsn { get; set; }
    }

    /// <summary>
    /// 购买商品确认信息
    /// </summary>
    public class BuyAssetConfirmation
    {
        /// <summary>
        /// 确认Id
        /// Creator_Id
        /// </summary>
        [JsonProperty("confirmation_id")]
        public string ConfirmationId { get; set; } = string.Empty;
    }
}
