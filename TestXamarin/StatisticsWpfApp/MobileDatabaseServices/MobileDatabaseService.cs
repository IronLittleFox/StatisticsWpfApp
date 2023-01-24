using StatisticsDatabaseLibrary.DatabaseServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace StatisticsWpfApp.MobileDatabaseServices
{
    public class MobileDatabaseService
    {
        public DatabaseService DatabaseService { get; set; }

        public MobileDatabaseService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database_2.db");
            DatabaseService = new DatabaseService($"Filename={dbPath}");
        }
    }
}
