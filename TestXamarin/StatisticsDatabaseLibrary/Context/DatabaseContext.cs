using Microsoft.EntityFrameworkCore;
using StatisticsDatabaseLibrary.Entities;

namespace StatisticsDatabaseLibrary.Context
{
    public class DatabaseContext : DbContext
    {
        string connectionString;

        /*public DatabaseContext():this("Test.db")
        {

        }*/

        public DatabaseContext() : this("Filename=dummy.db")
        {

        }

        public DatabaseContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public void OwnMigrate()
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CopyBook> CopyBooks { get; set; }
    }
}
