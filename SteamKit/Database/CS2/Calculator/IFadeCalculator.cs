using SteamKit.Database.CS2.Calculator.Model;

namespace SteamKit.Database.CS2.Calculator
{
    /// <summary>
    /// 渐变率计算器
    /// </summary>
    public interface IFadeCalculator
    {
        /// <summary>
        /// 获取传入武器和图案模板的渐变率
        /// </summary>
        /// <param name="weapon">武器</param>
        /// <param name="seed">图案模板</param>
        /// <returns></returns>
        public FadePercentage GetFadePercentage(WeaponsDefIndex weapon, uint seed);

        /// <summary>
        /// 获取传入武器的渐变率
        /// </summary>
        /// <param name="weapon">武器</param>
        /// <returns></returns>
        public IEnumerable<FadePercentage> GetFadePercentages(WeaponsDefIndex weapon);

        /// <summary>
        /// 获取所有武器
        /// </summary>
        public List<WeaponsDefIndex> GetWeapons();

        /// <summary>
        /// 是否支持传入武器的渐变率
        /// </summary>
        /// <param name="weapon">武器</param>
        /// <returns></returns>
        public bool Supported(WeaponsDefIndex weapon);
    }
}
