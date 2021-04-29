using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace wifiAnalysis
{
    public class Database
    {
        public static List<ScanObject> GetScanResults()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ScanObject>("SELECT * FROM ScanTable", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<RoomObject> GetRooms()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<RoomObject>("SELECT * FROM RoomTable", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void InsertScan(ScanObject scan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT into ScanTable (Ping, Jitter, Up, Down, Server, IP, HostName) VALUES (@Ping, @Jitter, @Up, @Down, @Server, @IP, @HostName)", scan);
            }
        }
        public static void InsertRoom(string Name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT into RoomTable (name) VALUES (\"" + Name + "\")");
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
