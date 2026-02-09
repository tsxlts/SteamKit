namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MicroTxnAuthRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<int, Item> lineitems { get; set; } = new Dictionary<int, Item>();

        /// <summary>
        /// 
        /// </summary>
        public ulong transid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong OrderID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint appid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Enums.Language language { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ECurrencyCode currency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ECurrencyCode BillingCurrency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint BillingTotal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint Tax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint VAT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sandbox { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string SteamRealm { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string RequiresCachedPmtMethod { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Refundable { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public class Item
        {
            /// <summary>
            /// 
            /// </summary>
            public string description { get; set; } = string.Empty;

            /// <summary>
            /// 
            /// </summary>
            public uint gameitemid { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public uint amount { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public uint quantity { get; set; }
        }
    }
}
