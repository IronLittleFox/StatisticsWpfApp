using StatisticsDatabaseLibrary.Context;
using StatisticsDatabaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsDatabaseLibrary.DatabaseServices
{
    public class DatabaseService
    {
        private readonly DatabaseContext databaseContext;

        public DatabaseContext DatabaseContext { get; set; }

        public DatabaseService(string connectionString)
        {
            databaseContext = new DatabaseContext(connectionString);
            DatabaseContext = databaseContext;
        }

        public IEnumerable<CopyBook> GetListOfCopyBooks(int userId)
        {
            return databaseContext.CopyBooks.Where(cb => cb.UserId == userId);
        }

    }
}
