using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using SteamKit.Client.Model;
using SteamKit.Client.Model.GC.CS2;
using SteamKit.Game.CS2;
using SteamKit.Model;
using SteamKit.Api;
using SteamKit.WebClient;
using 解绑邮箱.Factory;
using static SteamKit.Builder.ProxyBulider;
using static SteamKit.Enums;
using SteamKit.Builder;

namespace 解绑邮箱
{
    internal class Program
    {
        static string communityDomain = "https://steamproxy.net";
        static string loginDomain = "https://steamproxy.net/steamlogin";
        static string apiDomain = "https://steamproxy.net/steamapi";
        static string storeDomain = "https://steamproxy.net/steamstore";

        static async Task Main(string[] args)
        {
            ProxyBulider.WithProxy((s, m) =>
            {
                Proxy proxy = Proxy.Instance
                .WithSteamCommunity(communityDomain)
                .WithSteamLogin(loginDomain)
                .WithSteamApi(apiDomain)
                .WithSteamStore(storeDomain);

                return proxy;
            });

            Console.WriteLine("开始解绑Steam帐号邮箱，请按提示进行操作...");

            SteamCommunityClient steamWebClient;

            Console.WriteLine("请输入帐号存储文件路径");
            FileInfo fileInfo = new FileInfo(Console.ReadLine());

            FileInfo saveFile = new FileInfo(Path.Combine(fileInfo.DirectoryName, "邮箱已解绑.txt"));

            FileInfo errorFile = new FileInfo(Path.Combine(fileInfo.DirectoryName, "邮箱解绑失败.txt"));
            while (true)
            {
                using (var input = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite))
                {
                    using (var output = saveFile.Open(FileMode.Append, FileAccess.Write))
                    {
                        using (var erroroutput = errorFile.Open(FileMode.Append, FileAccess.Write))
                        {
                            using (var reader = new StreamReader(input))
                            {
                                using (var writer = new StreamWriter(output))
                                {
                                    using (var errorWriter = new StreamWriter(erroroutput))
                                    {
                                        string userName = null;
                                        string password = null;
                                        try
                                        {
                                            userName = null;
                                            password = null;

                                            string line = reader.ReadLine();
                                            if (string.IsNullOrWhiteSpace(line))
                                            {
                                                break;
                                            }

                                            var array = line.Replace("\t", "|").Split("|").Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();

                                            steamWebClient = new SteamCommunityClient();

                                            Console.WriteLine();
                                            Console.WriteLine("**************************************************************");

                                            userName = array[0].Trim();
                                            password = array[1].Trim();

                                            Console.WriteLine($"开始处理{userName}[{password}]...");

                                            #region 登录帐号
                                            Console.WriteLine("正在登录Steam帐号...");
                                            var login = await steamWebClient.BeginLoginAsync(userName, password, null, EAuthTokenPlatformType.k_EAuthTokenPlatformType_MobileApp, default);
                                            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
                                            while (!cancellationTokenSource.IsCancellationRequested)
                                            {
                                                if (login.steamid == 0 || login.client_id == 0)
                                                {
                                                    Console.WriteLine($"登录失败 {login.extended_error_message}");
                                                    break;
                                                }

                                                bool loginSuccess = await steamWebClient.PollLoginStatusAsync(login.steamid, login.client_id, login.request_id, default);
                                                if (loginSuccess)
                                                {
                                                    break;
                                                }

                                                if (login.allowed_confirmations!.Any(c => c.confirmation_type == EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceCode))
                                                {
                                                    Console.WriteLine("帐号已绑定手机令牌");
                                                    break;
                                                }

                                                if (login.allowed_confirmations!.Any(c => c.confirmation_type == EAuthSessionGuardType.k_EAuthSessionGuardType_EmailCode))
                                                {
                                                    Console.WriteLine("请输入邮箱确认码");
                                                    string code = Console.ReadLine()!;
                                                    await steamWebClient.ConfirmLoginWithGuardCodeAsync(login.steamid, login.client_id, EAuthSessionGuardType.k_EAuthSessionGuardType_EmailCode, code, default);
                                                    continue;
                                                }

                                                Thread.Sleep(1 * 1000);
                                            }
                                            if (!steamWebClient.LoggedIn)
                                            {
                                                Console.WriteLine("登录Steam帐号失败");
                                                continue;
                                            }
                                            Console.WriteLine("Steam帐号登录成功");
                                            #endregion

                                            #region 购买游戏
                                            Console.WriteLine("正在查询用户游戏库信息...");
                                            var games = SteamApi.QueryOwnedGamesAsync(null, steamWebClient.WebApiToken, steamWebClient.SteamId).GetAwaiter().GetResult().Body;
                                            if (!(games?.Games?.Any(c => c.AppId == "730") ?? false))
                                            {
                                                Console.WriteLine("开始添加游戏[730]");
                                                var addGameResult = await BuyGame(steamWebClient);
                                                if (addGameResult.HttpStatusCode != HttpStatusCode.OK || addGameResult.Body == null)
                                                {
                                                    Console.WriteLine("添加游戏[730]失败");
                                                    continue;
                                                }
                                            }
                                            Console.WriteLine("已添加游戏[730]");
                                            #endregion

                                            #region 验证是否可正常登录游戏
                                            int index = 0;
                                            bool success = false;
                                            string error = null;
                                            while (index < 3)
                                            {
                                                index++;
                                                try
                                                {
                                                    Console.WriteLine("正在验证是否可正常进入游戏...");

                                                    var cts = new CancellationTokenSource(10 * 1000);

                                                    CS2Client csgoClient = new CS2Client(2000600, 19762064);
                                                    csgoClient.WithProtocol(ProtocolTypes.Tcp)
                                                        .WithUser(userName, password, new AuthGuardFactory(null, cts))
                                                        .WithLogger(new ConsoleLogger());

                                                    bool loginGame = (await csgoClient.ConnectAsync(cts.Token)).Result;
                                                    if (!loginGame)
                                                    {
                                                        error = "登录游戏失败";

                                                        Console.WriteLine("登录游戏[730]失败,正在重试");
                                                        Task.Delay(1000).Wait();
                                                        continue;
                                                    }
                                                    Console.WriteLine("登录游戏[730]成功,正在验证是否可以检视");

                                                    CEconItemPreviewDataBlock inspect = null;
                                                    try
                                                    {
                                                        inspect = await csgoClient.InspectAsync("steam://rungame/730/76561202255233023/+csgo_econ_action_preview%20S76561199497982850A40098630523D2353348708500697644", new CancellationTokenSource(TimeSpan.FromSeconds(2)).Token);
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    if (inspect == null)
                                                    {
                                                        error = "检视失败";

                                                        Console.WriteLine("正在验证是否可以检视,正在重试");
                                                        continue;
                                                    }

                                                    await csgoClient.DisconnectAsync();
                                                    Console.WriteLine("检视成功,帐号可正常使用");

                                                    success = true;
                                                    break;
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine($"验证游戏异常, {ex.Message}");
                                                }
                                            }
                                            if (!success)
                                            {
                                                throw new Exception(error ?? "验证游戏失败");
                                            }
                                        #endregion

                                        #region 解绑邮箱
                                        CheckAuthenticatorStatus:
                                            var authenticatorStatus = await SteamAuthenticator.QueryAuthenticatorStatusAsync(steamWebClient.WebApiToken, ulong.Parse(steamWebClient.SteamId));
                                            switch ((SteamGuardScheme)authenticatorStatus.Body.steamguard_scheme)
                                            {
                                                case SteamGuardScheme.Device:
                                                    Console.WriteLine("帐号已绑定手机令牌");
                                                    continue;

                                                case SteamGuardScheme.None:
                                                    writer.WriteLine($"{userName}\t\t{password}");
                                                    Console.WriteLine("已成功移除邮箱令牌");
                                                    break;

                                                case SteamGuardScheme.Email:
                                                default:
                                                    var removeAuthenticator = await SteamAuthenticator.RemoveAuthenticatorAsync(steamWebClient.WebApiToken, SteamGuardScheme.None, "");
                                                    if (removeAuthenticator.HttpStatusCode != HttpStatusCode.OK)
                                                    {
                                                        Console.WriteLine("移除邮箱失败,正在重试");
                                                        await Task.Delay(1000);
                                                        goto CheckAuthenticatorStatus;
                                                    }

                                                    if (removeAuthenticator.ResultCode == ErrorCodes.Pending)
                                                    {
                                                        Console.WriteLine("请在绑定邮箱中确认移除邮箱令牌...");
                                                        Console.WriteLine($"如果无法打开确认页面,您可将确认地址中的【https://store.steampowered.com/】替换为【{storeDomain}】");
                                                        //Console.WriteLine("已进行邮箱确认后按任意键继续");
                                                        //Console.ReadLine();
                                                        while (true)
                                                        {
                                                            Console.WriteLine("等待邮箱确认...");
                                                            await Task.Delay(3000);

                                                            authenticatorStatus = await SteamAuthenticator.QueryAuthenticatorStatusAsync(steamWebClient.WebApiToken, ulong.Parse(steamWebClient.SteamId));
                                                            if (authenticatorStatus.Body.steamguard_scheme == (uint)SteamGuardScheme.None)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    goto CheckAuthenticatorStatus;

                                            }
                                            #endregion
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"处理失败,{ex.Message}...");

                                            errorWriter.WriteLine($"{userName}\t\t{password}");
                                        }
                                        finally
                                        {
                                            string text = reader.ReadToEnd();
                                            input.SetLength(0);
                                            input.Write(System.Text.Encoding.UTF8.GetBytes(text));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("执行完成，按任意键退出...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static async Task<IWebResponse<List<object>>> BuyGame(SteamWebClient steamWebClient)
        {
            string Id = "211096";

            var detail = (await SteamApi.GetAsync<string>($"{storeDomain}/app/730/CounterStrike_2/", currentCookies: steamWebClient.WebCookie)).Response;
            detail = HttpUtility.HtmlDecode(detail);
            Regex regex = new Regex(@"AddFreeLicense\(.*?(?<Id>\d+).*?,.*?""Counter-Strike 2"".*?\)");
            var match = regex.Match(detail);
            if (match.Success)
            {
                Id = match.Groups["Id"].Value;
            }

            var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"ajax","true" },
                {"sessionid",steamWebClient.SessionId }
            });

            var addGameResult = SteamApi.PostAsync<List<object>>($"{storeDomain}/freelicense/addfreelicense/{Id}",
                httpContent,
                new Dictionary<string, string> { { "Referer", "https://store.steampowered.com/app/730/CounterStrike_2/" } },
                steamWebClient.WebCookie);

            return await addGameResult;
        }
    }
}