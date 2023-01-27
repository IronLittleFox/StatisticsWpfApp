using Microsoft.EntityFrameworkCore;
using StatisticMobileDatabaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticMobileDatabaseLibrary.Context
{
    public class StatisticDatabaseContext : DbContext
    {
        private string sqlConnection;

        public DbSet<RegisteredUser> RegisteredUsers { get; set; }

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
