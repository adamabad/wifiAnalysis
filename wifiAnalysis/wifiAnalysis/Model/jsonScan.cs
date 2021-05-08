namespace wifiAnalysis
{
    public class jsonScan
    {
        public double download { get; set; }
        public string hostname { get; set; }
        public string ip_address { get; set; }
        public int jitter { get; set; }
        public int latency { get; set; }
        public double maxDownload { get; set; }
        public double maxUpload { get; set; }
        public string testDate { get; set; }
        public string testServer { get; set; }
        public double upload { get; set; }
        public string userAgent { get; set; }
    }
}
