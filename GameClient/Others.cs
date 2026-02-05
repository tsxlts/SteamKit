using System.Net.Http.Json;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using SteamKit;

namespace GameClient
{
    internal static class Others
    {
        public static async Task<object> GenerateSql()
        {
            List<string> sql = new List<string>();

            var buffResponse = await SteamApi.GetAsync<Newtonsoft.Json.Linq.JObject>("https://buff.163.com/api/market/csgo_container?container=highlight&is_container=0&container_type=other_container_charm&_=1752197127966");
            var goods = buffResponse.Body.Value<Newtonsoft.Json.Linq.JObject>("data").Value<Newtonsoft.Json.Linq.JArray>("items");
            foreach (var item in goods)
            {
                var hashName = item.Value<Newtonsoft.Json.Linq.JObject>("goods").Value<string>("market_hash_name");
                var iconUrl = item.Value<Newtonsoft.Json.Linq.JObject>("goods").Value<string>("icon_url");

                var icon = await CopyImg(iconUrl);

                sql.Add($"update steamgoods set Icon='{icon}' where GameId='730' and HashName='{hashName}' ;");
            }
            string json = JsonConvert.SerializeObject(sql, Formatting.Indented);

            Console.WriteLine(json);
            return sql;
        }

        public static async Task<object> RefreshSteamGoods()
        {
            var buffResponse = await SteamApi.GetAsync<Newtonsoft.Json.Linq.JObject>("https://buff.163.com/api/market/csgo_container?container=highlight&is_container=0&container_type=other_container_charm&_=1752197127966");
            var goods = buffResponse.Body.Value<Newtonsoft.Json.Linq.JObject>("data").Value<Newtonsoft.Json.Linq.JArray>("items");
            goods.Clear();
            Parallel.ForEach(goods.Reverse(), new ParallelOptions { MaxDegreeOfParallelism = 2 }, item =>
            {
                var hashName = item.Value<Newtonsoft.Json.Linq.JObject>("goods").Value<string>("market_hash_name");
                RefreshSteamGoods(hashName).GetAwaiter().GetResult();
            });

            var list = new List<string>
            {
            };

            Parallel.ForEach(list, new ParallelOptions { MaxDegreeOfParallelism = 1 }, item =>
            {
                RefreshSteamGoods(item).GetAwaiter().GetResult();
            });
            return null;
        }

        public static async Task RentDelisting(string goodsId)
        {
            try
            {
                var cookies = "";

                var response = await SteamApi.GetAsync<string>($"https://api.ecosteam.cn/Api/Manage/Rent/Delisting1?" +
                    $"goodsNum={goodsId}" +
                    $"&Reason={Uri.EscapeDataString("业务调整, 不再支持可租可售")}", currentCookies: Extensions.GetCookies(cookies));

                Console.WriteLine(response.Body);
            }
            catch
            {

            }
        }

        public static async Task SellDelisting(string goodsId)
        {
            try
            {
                var cookies = "";

                var response = await SteamApi.PostAsync<string>($"https://api.ecosteam.cn/Api/GoodsAdmin/GoodsSellingEdit", JsonContent.Create(new
                {
                    ReasonId = "",
                    goodsNum = goodsId,
                    ReasonDescript = "饰品暂不支持交易,系统下架"
                }), currentCookies: Extensions.GetCookies(cookies));

                Console.WriteLine(response.Body);
            }
            catch
            {

            }
        }

        public static async Task RefreshSteamGoods(string hashName)
        {
            try
            {
                var cookies = "";

                var response = await SteamApi.GetAsync<string>($"https://api.ecosteam.cn/Api/Manage/SteamGoods/RefreshSteamGoods?" +
                    $"steamGameId={730}" +
                    $"&hashName={Uri.EscapeDataString(hashName)}", currentCookies: Extensions.GetCookies(cookies));

                Console.WriteLine(response.Body);
            }
            catch
            {

            }
        }

        public static async Task Inspect(string assetId)
        {
            try
            {
                var cookies = "";

                var response = await SteamApi.PostAsync<string>($"https://api.ecosteam.cn/Api/AssetView/Inspect", JsonContent.Create(new
                {
                    GameId = "730",
                    AssetId = assetId,
                    InspectTypes = new object[] { 1, 4 },
                    Retry = true,
                }), currentCookies: Extensions.GetCookies(cookies));

                Console.WriteLine(response.Body);
            }
            catch
            {

            }
        }

        public static async Task PaintSeed()
        {
            var ws = new[] {
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_gut", Key="gut-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_kukri", Key="kukri-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_tactical", Key="huntsman-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_m9_bayonet", Key="m9-bayonet-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_push", Key="shadow-daggers-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_bayonet", Key="bayonet-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_css", Key="classic-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_canis", Key="survival-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_ursus", Key="ursus-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_falchion", Key="falchion-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_butterfly", Key="butterfly-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_stiletto", Key="stiletto-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_karambit", Key="karambit-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_widowmaker", Key="talon-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_cord", Key="paracord-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_skeleton", Key="skeleton-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_gypsy_jackknife", Key="navaja-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_outdoor", Key="nomad-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_survival_bowie", Key="bowie-knife-case-hardened" },
                new { Type= "CSGO_Type_Knife", SubType="weapon_knife_flip", Key="flip-knife-case-hardened" },

                new { Type= "CSGO_Type_Pistol", SubType="weapon_fiveseven", Key="five-seven-case-hardened" },
                new { Type= "CSGO_Type_Rifle", SubType="weapon_ak47", Key="ak-47-case-hardened" },
            };

            var index = 999999999;
            List<string> sqls = new List<string>();
            foreach (var item in ws)
            {
                var htmlResponse = await SteamApi.GetAsync<string>($"https://csgoskins.gg/blog/{item.Key}-blue-gem-seed-patterns");
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlResponse.Response!);

                List<HtmlNode> tables = doc.DocumentNode.SelectNodes("//table").ToList();
                if (tables.Count != 1)
                {
                    Console.WriteLine("error...");
                    break;
                }

                var tbody = tables[0].SelectSingleNode("./tbody");
                var rows = tbody.SelectNodes("./tr");
                foreach (var row in rows)
                {
                    var cells = row.SelectNodes("./td").ToList();

                    var lable = cells[0].InnerText.Replace(" ", "");
                    var paintSeeds = cells[1].InnerText.Split(",").ToList();

                    foreach (var itemPaintSeed in paintSeeds)
                    {
                        var paintSeed = int.Parse(itemPaintSeed.Trim());
                        var sql = $"INSERT INTO `csgopaintseedstier` " +
                            $"(`Id`,`TierName`,`Type`,`SubType`,`PaintIndex`,`PaintSeed`,`PaintSeedLabel`,`FastQuery`,`Sort`,`Status`,`Source`,`AudLink`,`CreateTime`) " +
                            $"VALUES " +
                            $"('{++index}','淬火等级','{item.Type}','{item.SubType}',{44},{paintSeed},'{lable}',b'0',9999,'审核成功','None','','{DateTime.Now:yyyy-MM-dd HH:mm:ss}');";

                        sqls.Add(sql);
                    }
                }

                await Task.Delay(1000);
            }

            File.AppendAllLines($"1.txtx", sqls);
            Console.WriteLine("success");
        }

        public static async Task<string> CopyFile(string filePath)
        {
            using (var file = File.OpenRead(filePath))
            {
                var ext = filePath.Split(".").Last();
                string path = UploadFileUtils.UploadFile($"eco/customize/{DateTime.Now:yyyy-MM-dd}/{Guid.NewGuid()}.{ext}", file, out var error);
                return $"//img.ecosteam.cn/{path.TrimStart('/')}";
            }
        }

        public static async Task<string> CopyImg(string url)
        {
            using (var img = (await SteamApi.GetAsync(url)).Content)
            {
                string path = UploadFileUtils.UploadFile($"goodsicon/customize/{DateTime.Now:yyyy-MM-dd}/{Guid.NewGuid()}.png", img, out var error);
                return $"//img.ecosteam.cn/{path.TrimStart('/')}";
            }
        }

        public static void Dota2GoodsType()
        {
            var sql = new List<string>();

            var power = new List<string>();
            using (var reader = new StreamReader(@"C:\Users\Administrator\Desktop\hero\力量-power.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var match = Regex.Match(line, @"title=""(?<name>.+?)"" value=""(?<key>.+?)"">");

                    power.Add(match.Groups["key"].Value);

                    sql.Add($"update steamgoods set Type='Customize-Type_hero_power', TypeName='力量', TypeEnName='Strength' where GameId='570' and SubTypeName='{match.Groups["name"].Value}'; ");
                }
            }

            var agile = new List<string>();
            using (var reader = new StreamReader(@"C:\Users\Administrator\Desktop\hero\敏捷-agile.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var match = Regex.Match(line, @"title=""(?<name>.+?)"" value=""(?<key>.+?)"">");

                    agile.Add(match.Groups["key"].Value);

                    sql.Add($"update steamgoods set Type='Customize-Type_hero_agile', TypeName='敏捷', TypeEnName='Agility' where GameId='570' and SubTypeName='{match.Groups["name"].Value}'; ");
                }
            }

            var generalist = new List<string>();
            using (var reader = new StreamReader(@"C:\Users\Administrator\Desktop\hero\全才-generalist.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var match = Regex.Match(line, @"title=""(?<name>.+?)"" value=""(?<key>.+?)"">");

                    generalist.Add(match.Groups["key"].Value);

                    sql.Add($"update steamgoods set Type='Customize-Type_hero_generalist', TypeName='全才', TypeEnName='Universal' where GameId='570' and SubTypeName='{match.Groups["name"].Value}'; ");
                }
            }

            var intelligence = new List<string>();
            using (var reader = new StreamReader(@"C:\Users\Administrator\Desktop\hero\智力-intelligence.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var match = Regex.Match(line, @"title=""(?<name>.+?)"" value=""(?<key>.+?)"">");

                    intelligence.Add(match.Groups["key"].Value);

                    sql.Add($"update steamgoods set Type='Customize-Type_hero_intelligence', TypeName='智力', TypeEnName='Intelligent' where GameId='570' and SubTypeName='{match.Groups["name"].Value}'; ");
                }
            }

            foreach (var item in sql)
            {
                Console.WriteLine(item);
            }


            //Console.WriteLine("power" + "\n" + JsonConvert.SerializeObject(power));
            //Console.WriteLine("agile" + "\n" + JsonConvert.SerializeObject(agile));
            //Console.WriteLine("generalist" + "\n" + JsonConvert.SerializeObject(generalist));
            //Console.WriteLine("intelligence" + "\n" + JsonConvert.SerializeObject(intelligence));
        }
    }
}
