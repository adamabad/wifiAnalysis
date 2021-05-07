using SQLite;
namespace wifiAnalysis
{
    public class ScanObject
    {
        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int Scan_ID { get; set; }
        [NotNull]
        public int Room_ID { get; set; }
        public double download { get; set; }
        public double upload { get; set; }
        public int latency { get; set; }
        public int jitter { get; set; }
        public string testServer { get; set; }
        public string ip_address { get; set; }
        public string hostname { get; set; }
        public string date { get; set; }
    }
}
