//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StatisticsWpfApp.Database.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace StatisticsWpfApp.Database.Context
{
    class DatabaseContext_TMP //: DbContext
    {
        /*public DatabaseContext_TMP()
        {
            SQLitePCL.Batteries_V2.Init();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database.db");
            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
            //optionsBuilder.UseSqlite("Filename=database.db");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User_TMP> Users { get; set; }
        public DbSet<CopyBook_TMP> CopyBooks { get; set; }*/
    }
}
