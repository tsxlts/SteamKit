namespace SteamKit.Client.Model.GC.CS2
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountItemPersonalStore
    {
        /// <summary>
        /// 生成时间
        /// 更新时间
        /// </summary>
        public uint GenerationTime { get; set; }

        /// <summary>
        /// 可兑换余额
        /// </summary>
        public uint RedeemableBalance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ulong> Items { get; set; } = new List<ulong>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personalStore"></param>
        public void Update(CSOAccountItemPersonalStore personalStore)
        {
            this.GenerationTime = personalStore.generation_time;
            this.RedeemableBalance = personalStore.redeemable_balance;
            this.Items = personalStore.items;
        }
    }
}
