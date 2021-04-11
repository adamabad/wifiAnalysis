using SQLite;

namespace wifiAnalysis
{
    public class ScanResult
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int Ping { get; set; }
        public int Upload { get; set; }
        public int Download { get; set; }

    }
}
