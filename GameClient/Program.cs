using System.Security.Cryptography;
using System.Text;
using GameClient.Factory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QRCoder;
using SteamKit;
using SteamKit.Api;
using SteamKit.Client;
using SteamKit.Client.Hanlders;
using SteamKit.Client.Internal.Msg;
using SteamKit.Client.Model;
using SteamKit.Client.Model.Cloud;
using SteamKit.Client.Model.GC;
using SteamKit.Client.Model.GC.CS2;
using SteamKit.Client.Model.Proto;
using SteamKit.Client.Server;
using SteamKit.Database.CS2;
using SteamKit.Game;
using SteamKit.Game.CS2;
using SteamKit.Game.Dota2;
using SteamKit.Game.TF2;
using SteamKit.Types;
using SteamKit.WebClient;
using static SteamKit.Builder.ProxyBulider;

namespace GameClient
{
    internal class Program
    {
        private static readonly IConfiguration Configuration;

        static Program()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsetting.json", false, true);
            Configuration = configurationBuilder.Build();

            WithProxy((s, m) =>
            {
                //return Proxy.Instance;

                var steamCommunity = Configuration["Proxy:SteamCommunity"]!;
                var steamApi = Configuration["Proxy:SteamApi"]!;
                var steamLogin = Configuration["Proxy:SteamLogin"]!;
                var steamStore = Configuration["Proxy:SteamStore"]!;

                var proxy = Proxy.Instance
                .WithSteamCommunity(steamCommunity)
                .WithSteamApi(steamApi)
                .WithSteamStore(steamStore)
                .WithSteamLogin(steamLogin);
                return proxy;
            });

            var js = JsonConvert.SerializeObject(new
            {
                Collectibles = CS2Database.Single.Collectibles(Enums.Language.Schinese),
                WeaponCases = CS2Database.Single.WeaponCases(Enums.Language.Schinese),
                WeaponCaseKeys = CS2Database.Single.WeaponCaseKeys(Enums.Language.Schinese),
                Paints = CS2Database.Single.Paints(Enums.Language.Schinese),
                Stickers = CS2Database.Single.Stickers(Enums.Language.Schinese),
                Keychains = CS2Database.Single.Keychains(Enums.Language.Schinese),
            }, Formatting.Indented);
        }

        static async Task Main(string[] args)
        {
            var stickers = CS2Database.Single.Stickers(Enums.Language.Schinese).ToArray();
            var jstickers = CS2Database.Single.Stickers(Enums.Language.Japanese).ToArray();

            //
            var hex = "3809A2CF73780E0401000000";
            var buffer = HexStringToByteArray(hex);
            var ms = new MemoryStream(buffer);

            var reader = new BinaryReader(ms);
            var orderid = reader.ReadUInt64();
            var c = reader.ReadInt32();

            hex = "01004D6573736167654F626A65637400006C696E656974656D7300003000016465736372697074696F6E00E89B87E599ACE6ADA6E599A8E7AEB1E992A5E58C99000767616D656974656D6964006D050000000000000A616D6F756E74008A07000000000000027175616E7469747900010000000808077472616E7369640056CFA1CF73780E04074F72646572494400E87052910000000002617070696400DA0200000263757272656E6379001D0000000256415400000000000A746F74616C008A070000000000000A546178000000000000000000026C616E677561676500060000000273616E64626F7800000000000242696C6C696E6743757272656E6379001D0000000A42696C6C696E67546F74616C008A0700000000000002537465616D5265616C6D0001000000025265717569726573436163686564506D744D6574686F64000000000002526566756E6461626C6500010000000808004D6573736167654F626A656374000248617357616C6C657400010000000A6E416D6F756E740097A1000000000000026543757272656E6379436F6465001D0000000A6E416D6F756E7444656C61796564000000000000000000026543757272656E6379436F646544656C61796564001D00000002537465616D5265616C6D00010000000808";
            buffer = HexStringToByteArray(hex);
            ms = new MemoryStream(buffer);
            var messageObject = new MessageObject();
            var success = messageObject.ReadFromStream(ms);

            SteamCommunityClient webClient = new SteamCommunityClient();
            webClient.SetLogger(new ConsoleLogger());
            webClient.SetLanguage(Enums.Language.Schinese);

            var token = Configuration["Steam:LoginToken"]!;
            token = Uri.UnescapeDataString(token);

            await webClient.LoginAsync(token);

            var QueryInventoryHistoryAsync = await SteamApi.QueryInventoryHistoryAsync(webClient.SteamId, webClient.SessionId, ["730"], null, Enums.Language.Schinese, webClient.WebCookie);
            var QuetyListingsAsync = await SteamApi.QuetyListingsAsync(0, 10, webClient.WebCookie);
            var QueryMarketListingsAsync = await SteamApi.QueryMarketListingsAsync("730", "Dual Berettas | Flora Carnivora (Well-Worn)", 0, 10);
            var QuerySelfInventoryAsync = await SteamApi.QueryInventoryAsync(webClient.SteamId, "730", "2", false, Enums.Language.Schinese, webClient.WebCookie);
            var QueryTradeStatusAsync = await SteamApi.QueryTradeStatusAsync(webClient.WebApiToken, "812466051293101185");
            var QueryInventoryAsync = await SteamApi.QueryInventoryAsync(webClient.SteamId, "730", "2", userCookies: webClient.WebCookie);
            var QueryOffersAsync = await SteamApi.QueryOffersAsync(webClient.WebApiToken);
            var QueryOfferAsync = await SteamApi.QueryOfferAsync(webClient.WebApiToken, "8839119086");

            Console.WriteLine("请选择操作：\n" +
                $"1、【检视】\t 2、【CS2Client】\t 3、【Dota2Client】\t 4、【TF2Client】\n" +
                $"5、【UnifiedGameClient】\n" +
                $"9、【SteamKit】t 10、【SlimClient】");
            var action = Console.ReadLine();
            switch (action)
            {
                case "10":
                    {
                        var client = new SlimClient()
                            .WithLogger(new ConsoleLogger())
                            .WithProtocol(ProtocolTypes.Tcp)
                            .WithSocketProvider(new ProxySocketProvider())
                            .ConfigureOptions(options =>
                            {
                                options
                                .WithOSType(EOSType.Win11)
                                .WithGamingDeviceType(EGamingDeviceType.PS3)
                                .WithUIMode(EUIMode.Unknown)
                                .WithChatMode(ChatMode.Default)
                                ;
                            })
                            ;

                        await client.ConnectAsync();

                        var authResult = await client.AuthTokenViaCredentialsAsync("ryvgt41502", "hgsj80861F", null,
                            EAuthTokenPlatformType.k_EAuthTokenPlatformType_SteamClient,
                            EAuthTokenAppType.k_EAuthTokenAppType_Unknown, 2);
                        Console.WriteLine($"{JsonConvert.SerializeObject(authResult, Formatting.Indented)}");

                        if (authResult.AllowedConfirmations.Any(c => c.confirmation_type == EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceCode))
                        {
                            Console.WriteLine("请输入令牌码");
                            var code = Console.ReadLine();
                            var confirmLogin = await authResult.ConfirmLoginAsync(EAuthSessionGuardType.k_EAuthSessionGuardType_DeviceCode, code);
                            await Task.Delay(1000);
                        }

                        var tokenResult = await authResult.PollingAuthTokenAsync();
                        Console.WriteLine($"{JsonConvert.SerializeObject(tokenResult, Formatting.Indented)}");

                        var logonResponse = await client.LogonAsync(authResult.SteamId, tokenResult.RefreshToken!);
                        Console.WriteLine($"{JsonConvert.SerializeObject(logonResponse, Formatting.Indented)}");

                        Console.WriteLine($"End ...");
                        await Task.Delay(5000);
                    }
                    break;

                case "9":
                default:
                    {
                        var jsToken = await SteamApi.GetClientWebTokenAsync(webClient.WebCookie);

                        var steamClient = new SteamClient();
                        steamClient
                            .WithLogger(new ConsoleLogger())
                            .WithProtocol(ProtocolTypes.Tcp)
                            .WithWebToken(jsToken.Body!.AccountName!, ulong.Parse(jsToken.Body.SteamId!), jsToken.Body.Token!)
                            //.WithQrCode(DrawQRCode)
                            //.WithUser("ddpc_2", "HNKJDX666@.", new AuthGuardFactory(new SteamKit.Model.SteamGuard { SharedSecret = "" }))
                            //.WithRefreshToken(76561198830495124, "eyAidHlwIjogIkpXVCIsICJhbGciOiAiRWREU0EiIH0.eyAiaXNzIjogInN0ZWFtIiwgInN1YiI6ICI3NjU2MTE5ODgzMDQ5NTEyNCIsICJhdWQiOiBbICJjbGllbnQiLCAid2ViIiwgInJlbmV3IiwgImRlcml2ZSIgXSwgImV4cCI6IDE3ODEzNTc2NTIsICJuYmYiOiAxNzU0Mjg4NDU2LCAiaWF0IjogMTc2MjkyODQ1NiwgImp0aSI6ICIwMDBDXzI3M0Q2OTAxXzg4NzFEIiwgIm9hdCI6IDE3NjI5Mjg0NTYsICJwZXIiOiAxLCAiaXBfc3ViamVjdCI6ICIxLjE5My41OS4xMzkiLCAiaXBfY29uZmlybWVyIjogIjEwMy4xOTcuNzEuOTkiIH0.UPgD2huZNahj1eCdLBCQrnIMcDNKfgcb-9frWl570TNqAotiDOChiRp4YVATDA1VtrJ_YNP36BUNwvzVTS5vDw")
                            ;

                        var userHandler = steamClient.GetHandler<SteamUserHandler>();
                        userHandler.RegistClientSessionTokenCallback((sender, arg) =>
                        {
                            return Task.CompletedTask;
                        });
                        userHandler.RegistPlayingSessionStateCallback((s, e) =>
                        {
                            Console.WriteLine($"PlayingSessionState：\t{e.playing_app}[{e.playing_blocked}]");
                            return Task.CompletedTask;
                        });
                        userHandler.RegistClientUserNotificationsCallback((sender, arg) =>
                        {
                            Console.WriteLine($"用户通知：\n{JsonConvert.SerializeObject(arg.notifications, Formatting.Indented)}");
                            return Task.CompletedTask;
                        });
                        userHandler.RegistClientWalletInfoUpdateCallback((sender, arg) =>
                        {
                            return Task.CompletedTask;
                        });
                        userHandler.RegistClientAccountInfoCallback((sender, arg) =>
                        {
                            return Task.CompletedTask;
                        });
                        userHandler.RegistClientEmailAddrInfoCallback((sender, arg) =>
                        {
                            return Task.CompletedTask;
                        });
                        userHandler.RegistClientVanityURLChangedCallback((sender, arg) =>
                        {
                            return Task.CompletedTask;
                        });

                        var messageHandler = steamClient.GetHandler<ServiceMessageHandler>();
                        messageHandler.RegistServiceNotification((sender, arg) =>
                        {
                            Console.WriteLine($"服务端通知：{arg.RpcName}");
                            if (arg.MethodName == "NotificationsReceived")
                            {
                                var x = arg.GetBody<CSteamNotification_NotificationsReceived_Notification>();
                                Console.WriteLine(JsonConvert.SerializeObject(x, Formatting.Indented));
                            }
                            return Task.CompletedTask;
                        });
                        messageHandler.RegistServiceResponse((sender, arg) =>
                        {
                            Console.WriteLine($"服务端响应：\n{JsonConvert.SerializeObject(arg, Formatting.Indented)}");
                            return Task.CompletedTask;
                        });

                        await steamClient.ConnectAsync();

                        var authenticatorHandler = steamClient.GetHandler<AuthenticatorHandler>();

                        var protoBufMsg = new GCClientProtoBufMsg<CMsgStoreGetUserData>((uint)EGCItemMsg.k_EMsgGCStoreGetUserData, 64);
                        protoBufMsg.Body.currency = 27;
                        steamClient.SendAsync(730, protoBufMsg);

                        var inventories = await steamClient.QueryInventoryAsync(730, 2);
                        var tradeOfferAccessToken = await steamClient.QueryTradeOfferAccessTokenAsync();

                        var xsc = await steamClient.ServiceMethodCallAsync((ICloud server) => server.SignalAppLaunchIntent(new CCloud_AppLaunchIntent_Request
                        {
                            appid = 730,
                            client_id = 0,
                            machine_name = $"{Environment.MachineName}@SteamKit",
                            ignore_pending_operations = false,
                            os_type = (int)EOSType.Win11,
                            device_type = 1
                        }));

                        await Task.Delay(3000);

                        Console.ReadKey();
                    }
                    break;

                case "5":
                    {
                        var webLogin = await webClient.LoginAsync(token);
                        if (!webLogin)
                        {
                            Console.WriteLine("登录Steam失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        var jsToken = await SteamApi.GetClientWebTokenAsync(webClient.WebCookie);

                        Console.WriteLine("输入游戏Id");
                        uint.TryParse(Console.ReadLine(), out var appId);
                        uint version = 19762064;

                        var client = new UnifiedGameClient(appId, version, 0);
                        client
                            .Configure(options => options.KickPlayingSession = false)
                            .WithWebToken(jsToken.Body!.AccountName!, ulong.Parse(jsToken.Body.SteamId!), jsToken.Body.Token!)
                            .WithUser("rnmaz37005", "jgbj13554F", new AuthGuardFactory(new SteamKit.Model.SteamGuard { SharedSecret = "" }))
                            .WithLogger(new ConsoleLogger())
                            .WithProtocol(ProtocolTypes.Tcp);

                        client.OnLoginGameExecuting(async (client) =>
                        {
                            var ownedGames = await client.QueryOwnedGamesAsync();
                            var game = ownedGames.Result?.games?.FirstOrDefault(c => c.appid == client.AppId);
                            if (game == null)
                            {
                                return new LoginGameResponse { Success = false, Error = "游戏库未拥有游戏" };
                            }

                            return new LoginGameResponse { Success = true, Error = null };
                        });

                        var userHandler = client.GetHandler<SteamUserHandler>();
                        userHandler.RegistPlayingSessionStateCallback((s, e) =>
                        {
                            Console.WriteLine($"PlayingSessionState：\t{e.playing_app}[{e.playing_blocked}]");
                            return Task.CompletedTask;
                        });

                        var connect = await client.ConnectAsync();
                        if (!connect.Result)
                        {
                            Console.WriteLine("连接服务器失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        var li = await client.RequestFreeLicenseAsync();

                        var login = await client.LoginGameAsync();
                        if (!login.Success)
                        {
                            Console.WriteLine("登录游戏失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        await client.WaitInitAsync();
                    }
                    break;

                case "4":
                    {
                        var webLogin = await webClient.LoginAsync(token);
                        if (!webLogin)
                        {
                            Console.WriteLine("登录Steam失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        var jsToken = await SteamApi.GetClientWebTokenAsync(webClient.WebCookie);
                        var client = new TF2Client(0, 0);
                        client
                            .Configure(options => options.KickPlayingSession = false)
                            .WithWebToken(jsToken.Body!.AccountName!, ulong.Parse(jsToken.Body.SteamId!), jsToken.Body.Token!)
                            .WithLogger(new ConsoleLogger())
                            .WithProtocol(ProtocolTypes.Tcp);

                        var userHandler = client.GetHandler<SteamUserHandler>();
                        userHandler.RegistPlayingSessionStateCallback((s, e) =>
                        {
                            Console.WriteLine($"PlayingSessionState：\t{e.playing_app}[{e.playing_blocked}]");
                            return Task.CompletedTask;
                        });

                        var connect = await client.ConnectAsync();
                        if (!connect.Result)
                        {
                            Console.WriteLine("连接服务器失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                        var login = await client.LoginGameAsync();
                        if (!login.Success)
                        {
                            Console.WriteLine("登录游戏失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        await client.WaitInitAsync();
                        await Task.Delay(1000);

                        var inventories = await client.GetInventoryAsync();
                        Console.WriteLine($"库存：{inventories.Count}");

                        Console.WriteLine($"按下任意键退出游戏...");
                        Console.ReadKey();
                        await client.LogoutGameAsync();

                        Console.ReadLine();
                        Console.ReadLine();
                        Console.ReadLine();
                    }
                    break;

                case "3":
                    {
                        var webLogin = await webClient.LoginAsync(token);
                        if (!webLogin)
                        {
                            Console.WriteLine("登录Steam失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        var jsToken = await SteamApi.GetClientWebTokenAsync(webClient.WebCookie);
                        var client = new Dota2Client(6525, 19762009);
                        client
                            .Configure(options => options.KickPlayingSession = false)
                            .WithWebToken(jsToken.Body!.AccountName!, ulong.Parse(jsToken.Body.SteamId!), jsToken.Body.Token!)
                            .WithLogger(new ConsoleLogger())
                            .WithProtocol(ProtocolTypes.Tcp);

                        var userHandler = client.GetHandler<SteamUserHandler>();
                        userHandler.RegistPlayingSessionStateCallback((s, e) =>
                        {
                            Console.WriteLine($"PlayingSessionState：\t{e.playing_app}[{e.playing_blocked}]");
                            return Task.CompletedTask;
                        });

                        var connect = await client.ConnectAsync();
                        if (!connect.Result)
                        {
                            Console.WriteLine("连接服务器失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                        var login = await client.LoginGameAsync();
                        if (!login.Success)
                        {
                            Console.WriteLine("登录游戏失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        await client.WaitInitAsync();
                        await Task.Delay(1000);

                        var inventories = await client.GetInventoryAsync();
                        Console.WriteLine($"库存：{inventories.Count}");

                        Console.WriteLine($"按下任意键退出游戏...");
                        Console.ReadKey();
                        await client.LogoutGameAsync();

                        Console.ReadLine();
                        Console.ReadLine();
                        Console.ReadLine();
                    }
                    break;

                case "2":
                    {
                        var webLogin = await webClient.LoginAsync(token);
                        if (!webLogin)
                        {
                            Console.WriteLine("登录Steam失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        var jsToken = await SteamApi.GetClientWebTokenAsync(webClient.WebCookie);
                        var client = new CS2Client(2000600, 19762064);
                        client
                            .Configure(options => options.KickPlayingSession = false)
                            .WithWebToken(jsToken.Body!.AccountName!, ulong.Parse(jsToken.Body.SteamId!), jsToken.Body.Token!)
                            //.WithUser("ddpc_2", "HNKJDX666@.", new AuthGuardFactory(null))
                            .WithLogger(new ConsoleLogger())
                            .WithProtocol(ProtocolTypes.Tcp);

                        client.RegistCallback(EMsg.ClientUpdateGuestPassesList, (s, e) =>
                        {
                            var x = new ServerMsg<MsgClientUpdateGuestPassesList>(e.PacketResult);
                            return Task.CompletedTask;
                        });

                        var userHandler = client.GetHandler<SteamUserHandler>();
                        userHandler.RegistPlayingSessionStateCallback((s, e) =>
                        {
                            Console.WriteLine($"PlayingSessionState：\t{e.playing_app}[{e.playing_blocked}]");
                            return Task.CompletedTask;
                        });

                        var gcHandler = client.GetHandler<SteamGameCoordinator>();

                        var connect = await client.ConnectAsync();
                        if (!connect.Result)
                        {
                            Console.WriteLine("连接服务器失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        var jobResult = await client.RequestFreeLicenseAsync();

                        var login = await client.LoginGameAsync();
                        if (!login.Success)
                        {
                            Console.WriteLine("登录游戏失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        await client.WaitInitAsync();

                        var xxxxx = await client.GetCasketContentsAsync(48076572765);

                        var clientWelcome = await client.RefreshAsync();
                        var g = ProtoBuf.Serializer.Deserialize<CMsgCStrike15Welcome>(new MemoryStream(clientWelcome.game_data));
                        var g2 = ProtoBuf.Serializer.Deserialize<CMsgGCCStrike15_v2_MatchmakingGC2ClientHello>(new MemoryStream(clientWelcome.game_data2));

                        var storeDataResponse = await client.QueryStorePricesAsync(client.Currency, Enums.Language.Schinese)!;

                        try
                        {
                            var wallet = await client.ServiceMethodCallAsync((IUserAccount server) => server.GetClientWalletDetails(new CUserAccount_GetClientWalletDetails_Request
                            {
                                include_balance_in_usd = true,
                                include_formatted_balance = true,
                            }));
                            var walletResponse = wallet.Result;

                            var microTxnHandler = client.GetHandler<MicroTxnHandler>();
                            while (true)
                            {
                                Console.WriteLine("输入购买物品名称：");
                                var name = Console.ReadLine();
                                var item = storeDataResponse!.PriceSheetItems.Value.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || c.HashName.Equals(name, StringComparison.OrdinalIgnoreCase));
                                item = item ?? storeDataResponse!.PriceSheetItems.Value.FirstOrDefault(c => c.DefIndex.ToString() == name);
                                if (item == null)
                                {
                                    Console.WriteLine($"未找到物品：{name}");
                                    continue;
                                }

                                Console.WriteLine("输入购买数量：");
                                if (!uint.TryParse(Console.ReadLine(), out uint count))
                                {
                                    count = 1;
                                }

                                var currency = (ECurrencyCode)walletResponse.currency_code;
                                var storeItem = storeDataResponse!.PriceSheetItems.Value.First(c => c.DefIndex == item.DefIndex);
                                var sotreItemPrice = storeItem.Prices[currency].SalePrice;
                                Console.WriteLine($"{storeItem.Name}：{sotreItemPrice}");

                                var purchaseInitResponse = await client.InitStorePurchaseAsync(storeItem.DefIndex, count, sotreItemPrice * count, client.Currency, language: Enums.Language.Schinese);
                                Console.WriteLine($"InitStorePurchaseAsync:\n{JsonConvert.SerializeObject(purchaseInitResponse, Formatting.Indented)}");

                                var microTxnAuthResponse = await microTxnHandler.QueryMicroTxnAuthRequestAsync(purchaseInitResponse!.txn_id, default);
                                Console.WriteLine($"QueryMicroTxnAuthResponseAsync:\n{JsonConvert.SerializeObject(microTxnAuthResponse, Formatting.Indented)}");

                                Console.WriteLine("请确认购买...");
                                //Console.WriteLine($"https://checkout.steampowered.com/checkout/approvetxn/{microTxnAuthResponse.transid}/?returnurl=https%3A%2F%2Fstore.steampowered.com%2Fbuyitem%2F{client.AppId}%2Ffinalize%2F{microTxnAuthResponse.OrderID}%3Fcanceledurl%3Dhttps%253A%252F%252Fstore.steampowered.com%252F%26returnhost%3Dstore.steampowered.com&canceledurl=https%3A%2F%2Fstore.steampowered.com%2F");
                                Console.WriteLine($"https://checkout.steampowered.com/checkout/approvetxn/{microTxnAuthResponse.transid}/?returnurl=steam");
                                Console.ReadLine();

                                Console.WriteLine("正在确认授权结果...");
                                var authorizeResponse = await microTxnHandler.ClientMicroTxnAuthorizeAsync(microTxnAuthResponse!.transid);
                                Console.WriteLine($"ClientMicroTxnAuthorizeAsync:\n{JsonConvert.SerializeObject(authorizeResponse, Formatting.Indented)}");

                                ms = new MemoryStream(authorizeResponse.Data);
                                messageObject = new MessageObject();
                                success = messageObject.ReadFromStream(ms);

                                Console.WriteLine("正在确认购买...");
                                var purchaseFinalizeResponse = await client.FinalizeStorePurchaseAsync(purchaseInitResponse!.txn_id);
                                Console.WriteLine($"FinalizeStorePurchaseAsync:\n{JsonConvert.SerializeObject(purchaseFinalizeResponse, Formatting.Indented)}");

                                Console.ReadLine();
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"InitStorePurchaseAsync: {ex.Message}");
                        }

                        try
                        {
                            var inventories = await client.GetInventoryAsync();
                            Console.WriteLine($"库存：{inventories.Count}");

                            var x1c = client.GetCasketContentsAsync(48076572765);

                            try
                            {
                                var inspect = await client.InspectAsync("steam://rungame/730/76561202255233023/+csgo_econ_action_preview%20S76561198109788905A47758679545D12151459491154429600",
                                    new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token);
                                Console.WriteLine($"InspectAsync：\n{JsonConvert.SerializeObject(inspect, Formatting.Indented)}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"GetCasketContents: {ex.Message}");
                        }

                        Console.WriteLine($"按下任意键退出游戏...");
                        Console.ReadKey();
                        await client.LogoutGameAsync();
                    }
                    break;

                case "1":
                    {
                        var webLogin = await webClient.LoginAsync(token);
                        if (!webLogin)
                        {
                            Console.WriteLine("登录Steam失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        var csgoClientManagement = new CS2ClientManagement(2000600, 19762064);

                        csgoClientManagement.WithConfiguration(c =>
                            {
                                c.WithInspectInterval(TimeSpan.FromMilliseconds(1000))
                                .WithKickPlayingSession(false);
                            })
                            .OnClientRemoved(async (client) =>
                            {
                                var path = Path.Combine(AppContext.BaseDirectory, "logs");
                                var file = Path.Combine(path, "ClientRemoved.log");
                                Directory.CreateDirectory(path);
                                File.AppendAllLines(file, [$"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\tClientRemoved"]);

                                while (true)
                                {
                                    try
                                    {
                                        using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20)))
                                        {
                                            var jsToken = await SteamApi.GetClientWebTokenAsync(webClient.WebCookie, cts.Token);
                                            var addClient = await csgoClientManagement.AddClientAsync(jsToken.Body!.AccountName!,
                                                ulong.Parse(webClient.SteamId!),
                                                jsToken.Body.Token!,
                                                cts.Token);

                                            if (addClient == null)
                                            {
                                                await Task.Delay(TimeSpan.FromSeconds(60));
                                                continue;
                                            }

                                            Console.WriteLine($"{await webClient.GetAccountNameAsync()} 已加入游戏");

                                            File.AppendAllLines(file, [$"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{addClient.SteamId}"]);
                                        }

                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                }
                            })
                            .WithLogger(new ConsoleLogger())
                            .WithProtocol(ProtocolTypes.Tcp);

                        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));

                        var jsToken = await SteamApi.GetClientWebTokenAsync(webClient.WebCookie);
                        var addClient = await csgoClientManagement.AddClientAsync(jsToken.Body!.AccountName!,
                            ulong.Parse(webClient.SteamId!),
                            jsToken.Body.Token!,
                            cts.Token);
                        if (addClient == null)
                        {
                            Console.WriteLine("登录游戏失败,按任意键退出");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        Console.WriteLine($"{await webClient.GetAccountNameAsync()} 已加入游戏");

                        Task task = Task.Run(async () =>
                        {
                            List<string> links = Configuration.GetSection("CS2InspectLinks").Get<List<string>>()!;

                            while (true)
                            {
                                try
                                {
                                    string link = links[RandomNumberGenerator.GetInt32(0, links.Count)];
                                    Console.WriteLine(link);

                                    var inspect = await csgoClientManagement.InspectAsync(link, new CancellationTokenSource(TimeSpan.FromSeconds(3)).Token);
                                    Console.WriteLine(JsonConvert.SerializeObject(inspect, Formatting.Indented));
                                    Console.WriteLine($"磨损：{BitConverter.UInt32BitsToSingle(inspect?.paintwear ?? 0)}");
                                }
                                catch (OperationCanceledException)
                                {
                                    Console.WriteLine("检视超时");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                finally
                                {
                                    int wait = RandomNumberGenerator.GetInt32(3, 20);
                                    while (wait > 0)
                                    {
                                        Console.WriteLine($"正在等待 {wait--} ...");
                                        await Task.Delay(1000);
                                    }
                                }
                            }
                        });
                        Task.WaitAll(task);

                    }
                    break;
            }
            Console.WriteLine("运行完成,按任意键退出");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void DrawQRCode(BeginQrAuthResult authSession)
        {
            Console.WriteLine($"Challenge URL: {authSession.ChallengeURL}");
            Console.WriteLine();

            using var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(authSession.ChallengeURL, QRCodeGenerator.ECCLevel.L);
            using var qrCode = new AsciiQRCode(qrCodeData);
            var qrCodeAsAsciiArt = qrCode.GetGraphic(1, drawQuietZones: false);

            Console.WriteLine(qrCodeAsAsciiArt);
        }

        /// <summary>
        /// 16进制字符串转字节
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        static byte[] HexStringToByteArray(string hex)
        {
            int hexLen = hex.Length;
            byte[] ret = new byte[hexLen / 2];
            for (int i = 0; i < hexLen; i += 2)
            {
                ret[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return ret;
        }

        /// <summary>
        /// 字节转16进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        static T DeserializeHexString<T>(string hex)
        {
            return ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(HexStringToByteArray(hex)));
        }

        static T DeserializeBase64<T>(string base64)
        {
            base64 = Uri.UnescapeDataString(base64);
            return ProtoBuf.Serializer.Deserialize<T>(new MemoryStream(Convert.FromBase64String(base64)));
        }

        static GCServerProtoBufMsg<T> DeserializeBase64GCServerProtoBufMsg<T>(string base64) where T : ProtoBuf.IExtensible, new()
        {
            base64 = Uri.UnescapeDataString(base64);
            var gCServerProtoBufMsg = new GCServerProtoBufMsg(0, 0, Convert.FromBase64String(base64));
            var msg = new GCServerProtoBufMsg<T>(gCServerProtoBufMsg!);
            return msg;
        }
    }

    public class MsgClientUpdateGuestPassesList : ISteamSerializable
    {
        // Static size: 4
        public EResult Result { get; set; }
        // Static size: 4
        public int CountGuestPassesToGive { get; set; }
        // Static size: 4
        public int CountGuestPassesToRedeem { get; set; }

        public void Serialize(Stream stream)
        {
            using BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);

            bw.Write((int)Result);
            bw.Write(CountGuestPassesToGive);
            bw.Write(CountGuestPassesToRedeem);

        }

        public void Deserialize(Stream stream)
        {
            using BinaryReader br = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);

            Result = (EResult)br.ReadInt32();
            CountGuestPassesToGive = br.ReadInt32();
            CountGuestPassesToRedeem = br.ReadInt32();
        }
    }
}
