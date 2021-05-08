using SQLite;
namespace wifiAnalysis
{
    public class RoomObject
    {
        [PrimaryKey, AutoIncrement, NotNull, Unique]
        public int Room_ID { get; set; }
        [NotNull, Unique]
        public string Room_Name { get; set; }

    }
}
