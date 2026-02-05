namespace _12306.Models
{
    public class TicketQueryResponse
    {
        public int httpstatus { get; set; }

        public Data? data { get; set; }

        public string? messages { get; set; }

        public bool status { get; set; }
    }

    public class Data
    {
        public List<string> result { get; set; } = new List<string>();

        public string? flag { get; set; }

        public string? level { get; set; }

        public string? sametlc { get; set; }

        public Dictionary<string, string> map { get; set; } = new Dictionary<string, string>();
    }
}
