using SQLite;
namespace wifiAnalysis
{
    public class ScanObject
    {
        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int Scan_ID { get; set; }
        [NotNull]
        public int Ping { get; set; }
        [NotNull]
        public int Jitter { get; set; }
        [NotNull]
        public double Up { get; set; }
        [NotNull]
        public double Down { get; set; }
        [NotNull]
        public string Server { get; set; }
        [NotNull]
        public string IP { get; set; }
        [NotNull]
        public string HostName { get; set; }

    }
}
