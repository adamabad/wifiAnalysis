using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Reflection;

namespace wifiAnalysis
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database()
        {
            string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ScanDatabase.db3");
            //Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            //Stream embeddedDatabaseStream = assembly.GetManifestResourceStream("wifiAnalysis.ScanDatabse.db");
            if (!File.Exists(DatabasePath))
            {
                RoomObject room;
                database = new SQLiteAsyncConnection(DatabasePath);
                database.CreateTableAsync<RoomObject>().Wait();
                database.CreateTableAsync<ScanObject>().Wait();
                room = new RoomObject
                {
                    Room_Name = "Living Room"
                };
                SaveRoomAsync(room);
                room = new RoomObject
                {
                    Room_Name = "Kitchen"
                };
                SaveRoomAsync(room);

                ScanObject scanTest;
                scanTest = new ScanObject
                {
                    Room_ID = 1,
                    download = 50,
                    upload = 30.3,
                    latency = 34,
                    jitter = 20,
                    testServer = "Boston1",
                    ip_address = "192.168.1.1",
                    hostname = "abb1",
                    testDate = "5/7/2021"
                };
                SaveScanAsync(scanTest);
            }
            else
            {
                database = new SQLiteAsyncConnection(DatabasePath);
                database.CreateTableAsync<RoomObject>().Wait();
                database.CreateTableAsync<ScanObject>().Wait();

            }
        }
        public Task<List<ScanObject>> GetScanResults()
        { 
            return database.Table<ScanObject>().ToListAsync();
        }

        public Task<ScanObject> GetScanResult(int id)
        {
            return database.Table<ScanObject>().Where(i => i.Scan_ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveScanAsync(ScanObject scanObject)
        {
            if (scanObject.Scan_ID != 0)
            {
                return database.UpdateAsync(scanObject);
            }
            else
            {
                return database.InsertAsync(scanObject);
            }
        }

        public Task<int> DeleteScanAsync(ScanObject scanObject)
        {
            return database.DeleteAsync(scanObject);
        }

        public Task<List<RoomObject>> GetRooms()
        {
            return database.Table<RoomObject>().ToListAsync();
        }

        public Task<RoomObject> GetRoom(int id)
        {
            return database.Table<RoomObject>().Where(i => i.Room_ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveRoomAsync(RoomObject roomObject)
        {
            if (roomObject.Room_ID != 0)
            {
                return database.UpdateAsync(roomObject);
            }
            else
            {
                return database.InsertAsync(roomObject);
            }
        }

        public Task<int> DeleteRoomAsync(RoomObject roomObject)
        {
            return database.DeleteAsync(roomObject);
        }
    }
}
