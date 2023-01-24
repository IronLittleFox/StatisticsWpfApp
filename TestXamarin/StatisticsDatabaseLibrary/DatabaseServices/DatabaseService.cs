using StatisticsDatabaseLibrary.Context;
using System;
using System.Collections.Generic;
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


    }
}
