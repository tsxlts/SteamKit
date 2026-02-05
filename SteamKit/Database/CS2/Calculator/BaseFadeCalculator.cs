using SteamKit.Database.CS2.Calculator.Model;
using SteamKit.Internal;

namespace SteamKit.Database.CS2.Calculator
{
    /// <summary>
    /// 渐变率计算器
    /// </summary>
    public abstract class BaseFadeCalculator : IFadeCalculator
    {
        private readonly double minPercentage;

        /// <summary>
        /// 
        /// </summary>
        public BaseFadeCalculator()
        {
            minPercentage = 80;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public FadePercentage GetFadePercentage(WeaponsDefIndex weapon, uint seed)
        {
            var percentages = GetFadePercentages(weapon);
            return percentages.FirstOrDefault(c => c.Weapon == weapon && c.Seed == seed) ?? new FadePercentage
            {
                Weapon = weapon,
                Seed = seed,
                Percentage = 0,
                Ranking = 0
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public IEnumerable<FadePercentage> GetFadePercentages(WeaponsDefIndex weapon)
        {
            if (!Supported(weapon))
            {
                throw new NotSupportedException($"The weapon {weapon} is currently not supported.");
            }

            var config = GetConfig(weapon);
            var tradeUpWeapons = GetTradeUpWeapons();
            var reversedWeapons = GetReversedWeapons();

            double maxSeed = tradeUpWeapons.Contains(weapon) ? 1000 : 999;

            List<double> rawResults = new List<double>();
            CS2RandomNumberGenerator randomNumberGenerator;

            for (var i = 0u; i <= maxSeed; i += 1)
            {
                randomNumberGenerator = new CS2RandomNumberGenerator();
                randomNumberGenerator.SetSeed(i);

                double rawResult;

                double xOffset = randomNumberGenerator.Random(config.pattern_offset_x_start, config.pattern_offset_x_end);
                double yOffset = randomNumberGenerator.Random(config.pattern_offset_y_start, config.pattern_offset_y_end);
                double rotation = randomNumberGenerator.Random(config.pattern_rotate_start, config.pattern_rotate_end);

                bool usesRotation = config.pattern_rotate_start != config.pattern_rotate_end;
                bool usesXOffset = config.pattern_offset_x_start != config.pattern_offset_x_end;

                if (usesRotation && usesXOffset)
                {
                    rawResult = rotation * xOffset;
                }
                else if (usesRotation)
                {
                    rawResult = rotation;
                }
                else
                {
                    rawResult = xOffset;
                }

                rawResults.Add(rawResult);
            }

            bool isReversed = reversedWeapons.Contains(weapon);

            double bestResult;
            double worstResult;
            if (isReversed)
            {
                bestResult = rawResults.Min();
                worstResult = rawResults.Max();
            }
            else
            {
                bestResult = rawResults.Max();
                worstResult = rawResults.Min();
            }

            double resultRange = worstResult - bestResult;

            var percentageResults = rawResults.Select(c => (worstResult - c) / resultRange);
            var sortedPercentageResults = percentageResults.OrderBy(c => c).ToList();

            return percentageResults.Select((value, index) => new FadePercentage
            {
                Weapon = weapon,
                Seed = (uint)index,
                Percentage = minPercentage + value * (100 - minPercentage),
                Ranking = Math.Min(sortedPercentageResults.IndexOf(value) + 1, sortedPercentageResults.Count - sortedPercentageResults.IndexOf(value))
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public bool Supported(WeaponsDefIndex weapon)
        {
            var weapons = GetWeapons();
            if (!weapons.Contains(weapon))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract List<WeaponsDefIndex> GetWeapons();

        /// <summary>
        /// 
        /// </summary>
        protected abstract List<WeaponsDefIndex> GetReversedWeapons();

        /// <summary>
        /// 
        /// </summary>
        protected abstract List<WeaponsDefIndex> GetTradeUpWeapons();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        protected abstract WeaponFadeConfig GetConfig(WeaponsDefIndex weapon);
    }
}
