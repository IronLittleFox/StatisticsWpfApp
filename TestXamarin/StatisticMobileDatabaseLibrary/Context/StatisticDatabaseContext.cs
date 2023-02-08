using Microsoft.EntityFrameworkCore;
using StatisticMobileDatabaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticMobileDatabaseLibrary.Context
{
    //Add-Migration ChangeToNullableColumnsMigration -OutputDir Migrations  -Project StatisticMobileDatabaseLibrary -StartupProject StatisticDummyConsoleApp
    public class StatisticDatabaseContext : DbContext
    {
        private string sqlConnection;

        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<CopyBook> CopyBooks { get; set; }
        public DbSet<ScannedPhoto> ScannedPhotos { get; set; }

        public StatisticDatabaseContext()
        {
            sqlConnection = "Filename=Dummy.db";
        }

        public StatisticDatabaseContext(string sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(sqlConnection);
            base.OnConfiguring(optionsBuilder);
        }

        public void OwnMigrate()
        {
            Database.Migrate();
        }
    }
}
