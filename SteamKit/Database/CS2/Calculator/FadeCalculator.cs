using SteamKit.Database.CS2.Calculator.Model;

namespace SteamKit.Database.CS2.Calculator
{
    /// <summary>
    /// 渐变之色渐变率计算器
    /// </summary>
    public class FadeCalculator : BaseFadeCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        public FadeCalculator() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override List<WeaponsDefIndex> GetWeapons()
        {
            return new List<WeaponsDefIndex>
            {
                WeaponsDefIndex.AWP,
                WeaponsDefIndex.Bayonet,
                WeaponsDefIndex.BowieKnife,
                WeaponsDefIndex.ButterflyKnife,
                WeaponsDefIndex.ClassicKnife,
                WeaponsDefIndex.FalchionKnife,
                WeaponsDefIndex.FlipKnife,
                WeaponsDefIndex.Glock18,
                WeaponsDefIndex.GutKnife,
                WeaponsDefIndex.HuntsmanKnife,
                WeaponsDefIndex.Karambit,
                WeaponsDefIndex.KukriKnife,
                WeaponsDefIndex.M4A1S,
                WeaponsDefIndex.M9Bayonet,
                WeaponsDefIndex.MAC10,
                WeaponsDefIndex.MP7,
                WeaponsDefIndex.NavajaKnife,
                WeaponsDefIndex.NomadKnife,
                WeaponsDefIndex.ParacordKnife,
                WeaponsDefIndex.R8Revolver,
                WeaponsDefIndex.ShadowDaggers,
                WeaponsDefIndex.SkeletonKnife,
                WeaponsDefIndex.StilettoKnife,
                WeaponsDefIndex.SurvivalKnife,
                WeaponsDefIndex.TalonKnife,
                WeaponsDefIndex.UMP45,
                WeaponsDefIndex.UrsusKnife,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override List<WeaponsDefIndex> GetReversedWeapons()
        {
            return new List<WeaponsDefIndex>
            {
                WeaponsDefIndex.AWP,
                WeaponsDefIndex.Karambit,
                WeaponsDefIndex.MP7,
                WeaponsDefIndex.TalonKnife
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override List<WeaponsDefIndex> GetTradeUpWeapons()
        {
            return new List<WeaponsDefIndex>
            {
                WeaponsDefIndex.AWP,
                WeaponsDefIndex.Glock18,
                WeaponsDefIndex.M4A1S,
                WeaponsDefIndex.MAC10,
                WeaponsDefIndex.MP7,
                WeaponsDefIndex.R8Revolver,
                WeaponsDefIndex.UMP45,
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
                case WeaponsDefIndex.M4A1S:
                    return new WeaponFadeConfig
                    {
                        pattern_offset_x_start = -0.14,
                        pattern_offset_x_end = 0.05,
                        pattern_offset_y_start = 0,
                        pattern_offset_y_end = 0,
                        pattern_rotate_start = -45,
                        pattern_rotate_end = -45,
                    };

                case WeaponsDefIndex.MP7:
                    return new WeaponFadeConfig
                    {
                        pattern_offset_x_start = -0.9,
                        pattern_offset_x_end = -0.3,
                        pattern_offset_y_start = -0.7,
                        pattern_offset_y_end = -0.5,
                        pattern_rotate_start = -55,
                        pattern_rotate_end = -65,
                    };

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
