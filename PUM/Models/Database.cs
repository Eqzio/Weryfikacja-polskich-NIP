using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUM.Models
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<VerificationDetails.VerifiedNIP>().Wait();
        }

        public Task<List<VerificationDetails.VerifiedNIP>> GetVerifiedNIPAsync()
        {
            return _database.Table<VerificationDetails.VerifiedNIP>().ToListAsync();
        }

        public Task<int> SaveVerifiedNIPAsync(VerificationDetails.VerifiedNIP verifiedNIP)
        {
            return _database.InsertAsync(verifiedNIP);
        }
    }
}
