
using System.Text.RegularExpressions;
using SteamKit.Model;

namespace SteamKit
{
    /// <summary>
    /// 发送报价错误信息
    /// </summary>
    public class SendOfferErrorMsg
    {
        /*
         *  对方无法交易
         *  @"[\w\W]*?无法进行交易"
         *  @"[\w\W]*? is not available to trade"
         *  
         *  自己无法交易
         *  @"在您可以进行交易之前[\w\W]*?您必须[\w\W]*?"
         *  @"You must [\w\W]*? before you can participate in a trade"
         *  
         *  @"您只有在启用[\w\W]*?至少[\w\W]*?才能参与交易"
         *  @"You must have had [\w\W]*? before you can participate in a trade"
         *  
         *  @"您刚刚重置了您的密码[\w\W]*?为了保护您库存中的物品[\w\W]*?您将无法交易物品"
         *  @"You recently forgot and then reset your Steam account's password. In order to protect the items in your inventory, you will be unable to trade for 4 more days"
         *  
         *  @"[\w\W]*?您无法与[\w\W]*?交易"
         *  @"You cannot trade with [\w\W]"
         */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SendOfferErrorMsg(string message)
        {
            Message = message;
            SelfCanTrade = true;
            PartnerCanTrade = true;

            var regexs = new List<(string Regex, bool Our)>
            {
                (@"(您只有在启用[\w\W]*?至少[\w\W]*?才能参与交易|You must have had [\w\W]*? before you can participate in a trade)",true),
                (@"(在您可以进行交易之前[\w\W]*?您必须[\w\W]*?|You must [\w\W]*? before you can participate in a trade)",true),
                (@"(您将无法交易物品|you will be unable to trade)",true),
                (@"([\w\W]*?您无法与[\w\W]*?交易|You cannot trade with [\w\W])",true),

                (@"([\w\W]*?无法进行交易|[\w\W]*? is not available to trade"")",false)
            };
            Match match;
            foreach (var item in regexs)
            {
                match = Regex.Match(message ?? "", item.Regex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    continue;
                }

                if (item.Our)
                {
                    SelfCanTrade = false;
                }
                else
                {
                    PartnerCanTrade = false;
                }
                break;
            }

            var codeMatch = Regex.Match(message ?? "", @"\((?<code>\d+)\)");
            if (codeMatch.Success)
            {
                string code = codeMatch.Groups["code"].Value;
                if (Enum.TryParse<ErrorCodes>(code, out var error))
                {
                    ErrorCode = error;
                }
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 自己是否可交易
        /// </summary>
        public bool SelfCanTrade { get; set; }

        /// <summary>
        /// 对方是否可交易
        /// </summary>
        public bool PartnerCanTrade { get; set; }

        /// <summary>
        /// 错误发
        /// </summary>
        public ErrorCodes? ErrorCode { get; set; }
    }
}
