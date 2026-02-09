using System.Text;
using _12306.Models;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using SteamKit;
using SteamKit.Api;
using Vdaima.Utils.Email;

namespace _12306
{
    internal class Program
    {
        private static readonly IConfiguration Configuration;

        static Program()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsetting.json", false, true);
            Configuration = configurationBuilder.Build();
        }

        static async Task Main(string[] args)
        {
            var notifyEmailSender = new EmailSender(new EmailSetting
            {
                SenderName = "微代码",
                Sender = "tsxlts@foxmail.com",
                Password = "zivbupjigjhlecgf",
                Server = "smtp.qq.com",
                Port = 465,
                UseSsl = true,
            });

            var cookies = new CookieCollection();

            int.TryParse(Configuration["train:interval"], out var interval);
            var notify = Configuration["train:notify"];

            DateTime.TryParse(Configuration["train:date"], out var date);
            var from = Configuration["train:from"];
            var to = Configuration["train:to"];
            bool.TryParse(Configuration["train:no_seat"], out var no_seat);
            var train_code = Configuration.GetSection("train:train_code").Get<List<string>>() ?? new List<string>();

            var cts = new CancellationTokenSource();
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    var index = $"https://kyfw.12306.cn/otn/leftTicket/init?" +
                        $"linktypeid=dc" +
                        $"&fs={Uri.EscapeDataString(from)}" +
                        $"&ts={Uri.EscapeDataString(to)}" +
                        $"&date={Uri.EscapeDataString($"{date:yyyy-MM-dd}")}" +
                        $"&flag={Uri.EscapeDataString("N,N,Y")}";
                    var init = await SteamApi.GetAsync<string>(index);

                    var header = new Dictionary<string, string>
                {
                    {"referer",index }
                };
                    cookies.Add(init.Cookies);

                    var query = await SteamApi.GetAsync<TicketQueryResponse>($"https://kyfw.12306.cn/otn/leftTicket/queryG?" +
                        $"leftTicketDTO.train_date={Uri.EscapeDataString($"{date:yyyy-MM-dd}")}" +
                        $"&leftTicketDTO.from_station={from.Split(',').Last()}" +
                        $"&leftTicketDTO.to_station={to.Split(',').Last()}" +
                        $"&purpose_codes=ADULT", headers: header, currentCookies: cookies);

                    var queryBody = query.Body;
                    if (!(queryBody?.status ?? false))
                    {
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{queryBody?.messages}");
                    }

                    var tickets = new Dictionary<TrainInfo, List<SeatAvailability>>();
                    var results = queryBody?.data?.result ?? new List<string>();
                    foreach (var result in results)
                    {
                        var trainInfo = TrainInfo.Parse(result, queryBody!.data!.map);
                        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{trainInfo.TrainCode}\t{trainInfo.Action}");

                        if (string.IsNullOrWhiteSpace(trainInfo.SecretStr))
                        {
                            continue;
                        }

                        if (train_code.Any() && !train_code.Contains(trainInfo.TrainCode))
                        {
                            continue;
                        }

                        var ticketDetails = trainInfo.Seats.Values.Where(c => c.HasTicket).ToList();
                        if (!no_seat)
                        {
                            ticketDetails.RemoveAll(c => c.Type == SeatType.无座);
                        }
                        if (!ticketDetails.Any())
                        {
                            continue;
                        }

                        tickets.Add(trainInfo, ticketDetails);
                    }

                    if (tickets.Any())
                    {
                        var message = new StringBuilder();
                        message.AppendLine($"<div style='width: 100%; text-align: center;'>");
                        message.AppendLine($"<div style='font-weight: bold;padding-bottom:10px;'>" +
                            $"<p>监控到</p>" +
                            $"<p>" +
                            $"<span style='color:red;'>{date:yyyy年MM月dd日}</span>" +
                            $"<span style='color:green;'>&nbsp;{from}&nbsp;</span>到<span style='color:green;'>&nbsp;{to}&nbsp;</span>" +
                            $"</p>" +
                            $"<p>有余票</p>" +
                            $"</div>");
                        foreach (var item in tickets.OrderBy(c => c.Key.DptTime))
                        {
                            var trainInfo = item.Key;
                            var ticketDetails = item.Value;
                            message.AppendLine($"<div style='border-top: 1px solid #eee; width: 80%; margin: 0 auto;padding:5px 0px;'>" +
                                $"<p><span style='color:red;'>&nbsp;{trainInfo.TrainCode}&nbsp;</span>次列车</p>" +
                                $"<p>" +
                                $"<span style='color:green;'>{trainInfo.FromStationName}&nbsp;</span><span style='color:red;'>{trainInfo.DptTime.Hours:00}:{trainInfo.DptTime.Minutes:00}</span>" +
                                $"<span>&nbsp;到&nbsp;</span>" +
                                $"<span style='color:green;'>{trainInfo.ToStationName}&nbsp;</span><span style='color:red;'>{trainInfo.ArrTime.Hours:00}:{trainInfo.ArrTime.Minutes:00}</span>" +
                                $"</p>" +
                                $"{string.Join("", ticketDetails.Select(c => $"<p>{c.DisplayName}：{c.RawValue}</p>"))}" +
                                $"</div>");
                        }
                        message.AppendLine($"</div>");

                        await notifyEmailSender.SendEmailAsync(notify, "火车票余票通知", message.ToString(), TextFormat.Html);
                        cts.Cancel();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await Task.Delay(interval);
            }

            Console.WriteLine("监控完成...");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
