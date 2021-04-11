using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace wifiAnalysis
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ScanResult>().Wait();
        }

        public Task<List<ScanResult>> GetPeopleAsync()
        {
            return _database.Table<ScanResult>().ToListAsync();
        }

        public Task<int> SavePersonAsync(ScanResult scanResult)
        {
            return _database.InsertAsync(scanResult);
        }
    }
}
