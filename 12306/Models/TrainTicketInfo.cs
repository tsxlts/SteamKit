namespace _12306.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainInfo
    {
        public static class ResultIndex
        {
            public const int SecretStr = 0;
            public const int CanBook = 1;
            public const int TrainNo = 2;
            public const int TrainCode = 3;

            public const int FromStationCode = 6;
            public const int ToStationCode = 7;

            public const int StartTime = 8;
            public const int ArriveTime = 9;
            public const int Duration = 10;
            public const int ArriveSameDay = 11;

            public const int TrainDate = 13;
            public const int TicketExtra = 34;
            public const int SeatTypes = 35;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="stationMap"></param>
        /// <returns></returns>
        public static TrainInfo Parse(string result, Dictionary<string, string> stationMap)
        {
            var arr = result.Split('|');

            var trainCode = arr[ResultIndex.TrainCode];
            var action = arr[ResultIndex.CanBook];

            var info = new TrainInfo
            {
                TrainCode = trainCode,
                TrainNo = arr[ResultIndex.TrainNo],
                SecretStr = Uri.UnescapeDataString(arr[ResultIndex.SecretStr]),

                Action = action,

                TrainDate = DateTime.ParseExact(arr[ResultIndex.TrainDate], "yyyyMMdd", null),

                FromStationCode = arr[ResultIndex.FromStationCode],
                ToStationCode = arr[ResultIndex.ToStationCode],

                FromStationName = stationMap.GetValueOrDefault(arr[ResultIndex.FromStationCode], ""),
                ToStationName = stationMap.GetValueOrDefault(arr[ResultIndex.ToStationCode], ""),

                DptTime = TimeSpan.TryParse(arr[ResultIndex.StartTime], out var st) ? st : TimeSpan.Zero,
                ArrTime = TimeSpan.TryParse(arr[ResultIndex.ArriveTime], out var at) ? at : TimeSpan.Zero,
                DurationMinutes = ParseDuration(arr[ResultIndex.Duration]),

                CanBook = action == "预订",
                CanWait = arr.Length > ResultIndex.TicketExtra && arr[ResultIndex.TicketExtra] == "1",

                ArriveSameDay = arr[ResultIndex.ArriveSameDay] == "Y",
                TrainType = ParseTrainType(trainCode),

                SeatTypes = arr[ResultIndex.SeatTypes],
                Seats = ParseSeats(arr)
            };

            return info;
        }

        private static Dictionary<SeatType, SeatAvailability> ParseSeats(string[] arr)
        {
            var seats = new Dictionary<SeatType, SeatAvailability>();
            if (arr == null || arr.Length == 0)
            {
                return seats;
            }

            string getItem(int index)
            {
                if (index < 0 || index >= arr.Length)
                {
                    return string.Empty;
                }

                return arr[index] ?? string.Empty;
            }

            foreach (var type in Enum.GetValues<SeatType>())
            {
                seats[type] = new SeatAvailability
                {
                    Type = type,
                    DisplayName = $"{type}",
                    RawValue = getItem((int)type)
                };
            }

            return seats;
        }

        private static TrainType ParseTrainType(string trainCode)
        {
            return trainCode[0] switch
            {
                'K' => TrainType.K,
                'T' => TrainType.T,
                'Z' => TrainType.Z,
                'G' => TrainType.G,
                'D' => TrainType.D,
                'C' => TrainType.C,
                _ => TrainType.Other
            };
        }

        private static int ParseDuration(string duration)
        {
            var parts = duration.Split(':');
            return int.Parse(parts[0]) * 60 + int.Parse(parts[1]);
        }

        /// <summary>
        /// 车次号（如：K1 / G1）
        /// </summary>
        public string TrainCode { get; set; } = string.Empty;

        /// <summary>
        /// 内部车次编号（train_no）
        /// </summary>
        public string TrainNo { get; set; } = string.Empty;

        /// <summary>
        /// 出发日期
        /// </summary>
        public DateTime TrainDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Action { get; set; } = string.Empty;

        /// <summary>
        /// 出发站代码
        /// </summary>
        public string FromStationCode { get; set; } = string.Empty;

        /// <summary>
        /// 出发站名称
        /// </summary>
        public string FromStationName { get; set; } = string.Empty;

        /// <summary>
        /// 到达站代码
        /// </summary>
        public string ToStationCode { get; set; } = string.Empty;

        /// <summary>
        /// 到达站名称
        /// </summary>
        public string ToStationName { get; set; } = string.Empty;

        /// <summary>
        /// 出发时间（HH:mm）
        /// </summary>
        public TimeSpan DptTime { get; set; }

        /// <summary>
        /// 到达时间（HH:mm）
        /// </summary>
        public TimeSpan ArrTime { get; set; }

        /// <summary>
        /// 历时（分钟）
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// 是否可预订
        /// </summary>
        public bool CanBook { get; set; }

        /// <summary>
        /// 是否支持候补
        /// </summary>
        public bool CanWait { get; set; }

        /// <summary>
        /// 是否当日到达
        /// </summary>
        public bool ArriveSameDay { get; set; }

        /// <summary>
        /// 列车类型
        /// </summary>
        public TrainType TrainType { get; set; }

        /// <summary>
        /// 提交订单用的加密串
        /// </summary>
        public string SecretStr { get; set; } = string.Empty;

        /// <summary>
        /// 坐席类型
        /// </summary>
        public string SeatTypes { get; set; } = string.Empty;

        /// <summary>
        /// 所有席别余票
        /// </summary>
        public IDictionary<SeatType, SeatAvailability> Seats { get; set; } = new Dictionary<SeatType, SeatAvailability>();
    }

    public class SeatAvailability
    {
        /// <summary>
        /// 席别类型
        /// </summary>
        public SeatType Type { get; set; }

        /// <summary>
        /// 显示名称（硬座 / 二等座等）
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 原始返回值（有 / 无 / 数字）
        /// </summary>
        public string RawValue { get; set; } = string.Empty;

        /// <summary>
        /// 是否有票
        /// </summary>
        public bool HasTicket => RawValue == "有" || int.TryParse(RawValue, out var n) && n > 0;

        /// <summary>
        /// 剩余数量（未知则为 99）
        /// </summary>
        public int Count => int.TryParse(RawValue, out var n) ? n : RawValue == "有" ? 99 : 0;

        public override string ToString()
        {
            return $"{Type}={RawValue}";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TrainType
    {
        K, T, Z,
        G, D, C,
        Other
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SeatType
    {
        /// <summary>
        /// 优选一等座
        /// </summary>
        优选一等座 = 20,

        /// <summary>
        /// 高级软卧
        /// </summary>
        高级软卧 = 21,

        /// <summary>
        /// 其他
        /// </summary>
        其他 = 22,

        /// <summary>
        /// 软卧
        /// </summary>
        软卧 = 23,

        /// <summary>
        /// 软座
        /// </summary>
        软座 = 24,

        /// <summary>
        /// 特等座
        /// </summary>
        特等座 = 25,

        /// <summary>
        /// 无座
        /// </summary>
        无座 = 26,

        /// <summary>
        /// YB（动卧 / 包厢等，12306 内部席别）
        /// </summary>
        YB = 27,

        /// <summary>
        /// 硬卧
        /// </summary>
        硬卧 = 28,

        /// <summary>
        /// 硬座
        /// </summary>
        硬座 = 29,

        /// <summary>
        /// 二等座
        /// </summary>
        二等座 = 30,

        /// <summary>
        /// 一等座
        /// </summary>
        一等座 = 31,

        /// <summary>
        /// 商务座
        /// </summary>
        商务座 = 32,

        /// <summary>
        /// SRRB（市域列车 / 特殊席别）
        /// </summary>
        SRRB = 33,

        /// <summary>
        /// 余票扩展字段
        /// </summary>
        余票扩展 = 34
    }
}