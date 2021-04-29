namespace wifiAnalysis
{
    public class ScanObject
    {
        public int Scan_ID { get; set; }
        public int Ping { get; set; }
        public int Jitter { get; set; }
        public double Up { get; set; }
        public double Down { get; set; }
        public string Server { get; set; }
        public string IP { get; set; }
        public string HostName { get; set; }

    }
}
