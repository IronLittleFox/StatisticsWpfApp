using StatisticsDatabaseLibrary.DatabaseServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace StatisticsWpfApp.MobileDatabaseServices
{
    public static class MobileDatabaseService
    {
        //public DatabaseService DatabaseService { get; set; }

        private static DatabaseService databaseServiceInstance;
        public static DatabaseService DatabaseServiceInstance
        {
            get
            {
                if (databaseServiceInstance == null)
                {
                    string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database_2.db");
                    databaseServiceInstance = new DatabaseService($"Filename={dbPath}");
                }
                return databaseServiceInstance;
            }
        }
    }
}
