using SteamKit.Database.CS2.Calculator.Model;

namespace SteamKit.Database.CS2.Calculator
{
    /// <summary>
    /// 渐变强酸渐变率计算器
    /// </summary>
    public class AcidFadeCalculator : BaseFadeCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        public AcidFadeCalculator() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override List<WeaponsDefIndex> GetWeapons()
        {
            return new List<WeaponsDefIndex>
            {
                WeaponsDefIndex.SSG08,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override List<WeaponsDefIndex> GetReversedWeapons()
        {
            return new List<WeaponsDefIndex>
            {
                WeaponsDefIndex.SSG08,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override List<WeaponsDefIndex> GetTradeUpWeapons()
        {
            return new List<WeaponsDefIndex>
            {
                WeaponsDefIndex.SSG08,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override WeaponFadeConfig GetConfig(WeaponsDefIndex weapon)
        {
            switch (weapon)
            {
                default:
                    return new WeaponFadeConfig
                    {
                        pattern_offset_x_start = -2.4,
                        pattern_offset_x_end = -2.1,
                        pattern_offset_y_start = 0.0,
                        pattern_offset_y_end = 0.0,
                        pattern_rotate_start = -55,
                        pattern_rotate_end = -65,
                    };
            }
        }
    }
}
