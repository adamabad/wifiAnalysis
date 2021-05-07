using SQLite;
namespace wifiAnalysis
{
    public class ScanObjectWithName
    {
        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int Scan_ID { get; set; }
        [NotNull]
        public string Room_Name { get; set; }
        public int Room_ID { get; set; }
        public double download { get; set; }
        public string hostname { get; set; }
        public string ip_address { get; set; }
        public int jitter { get; set; }
        public int latency { get; set; }
        public double maxDownload { get; set; }
        public double maxUpload { get; set; }
        public string testDate { get; set; }
        public double upload { get; set; }
        public string testServer { get; set; }
        public string userAgent { get; set; }
        public bool isVisible { get; set; }
    }
}
