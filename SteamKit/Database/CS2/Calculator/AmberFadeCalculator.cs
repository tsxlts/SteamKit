using SteamKit.Database.CS2.Calculator.Model;

namespace SteamKit.Database.CS2.Calculator
{
    /// <summary>
    /// 渐变琥珀渐变率计算器
    /// </summary>
    public class AmberFadeCalculator : BaseFadeCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        public AmberFadeCalculator() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override List<WeaponsDefIndex> GetWeapons()
        {
            return new List<WeaponsDefIndex>
            {
                WeaponsDefIndex.AUG,
                WeaponsDefIndex.GalilAR,
                WeaponsDefIndex.MAC10,
                WeaponsDefIndex.P2000,
                WeaponsDefIndex.R8Revolver,
                WeaponsDefIndex.SawedOff,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override List<WeaponsDefIndex> GetReversedWeapons()
        {
            return new List<WeaponsDefIndex>
            {
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override List<WeaponsDefIndex> GetTradeUpWeapons()
        {
            return new List<WeaponsDefIndex>
            {
                WeaponsDefIndex.AUG,
                WeaponsDefIndex.GalilAR,
                WeaponsDefIndex.MAC10,
                WeaponsDefIndex.P2000,
                WeaponsDefIndex.R8Revolver,
                WeaponsDefIndex.SawedOff,
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
                        pattern_offset_x_start = -0.7,
                        pattern_offset_x_end = -0.7,
                        pattern_offset_y_start = -0.7,
                        pattern_offset_y_end = -0.7,
                        pattern_rotate_start = -55,
                        pattern_rotate_end = -65,
                    };
            }
        }
    }
}
