using Microsoft.EntityFrameworkCore;
using StatisticMobileDatabaseLibrary.Context;
using StatisticMobileDatabaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticMobileDatabaseLibrary.DatabaseServices
{
    public class StatisticDatabaseServices
    {
        StatisticDatabaseContext statisticDatabaseContext;

        public StatisticDatabaseServices(StatisticDatabaseContext statisticDatabaseContext)
        {
            this.statisticDatabaseContext = statisticDatabaseContext;
        }

        public bool IsRegisteredUser(string userName)
        {
            return statisticDatabaseContext.RegisteredUsers.Any(ru => ru.UserName == userName);
        }

        public RegisteredUser RegisterNewUser(string userName, string password)
        {
            RegisteredUser registeredUser = new RegisteredUser()
            {
                UserName = userName,
                Password = password
            };
            statisticDatabaseContext.RegisteredUsers.Add(registeredUser);
            statisticDatabaseContext.SaveChanges();
            return registeredUser;
        }

        public RegisteredUser GetRegisteredUser(string userName, string password)
        {
            var x = statisticDatabaseContext.RegisteredUsers.ToList();
            return statisticDatabaseContext.RegisteredUsers
                .FirstOrDefault(ru => ru.UserName == userName && ru.Password == password);
        }

        public IEnumerable<CopyBook> GetListOfCopyBooks(int registeredUserId)
        {
            return statisticDatabaseContext.CopyBooks.Where(cb => cb.RegisteredUserId == registeredUserId);
        }

        public CopyBook GetCopyBook(int copyBookId)
        {
            return statisticDatabaseContext.CopyBooks.FirstOrDefault(cb => cb.Id == copyBookId);
        }

        public CopyBook GetCopyBookWidthScannedPhoto(int copyBookId)
        {
            return statisticDatabaseContext.CopyBooks.Include(cb=> cb.ScannedPhotos).FirstOrDefault(cb => cb.Id == copyBookId);
        }

        public int GetScannedPhotoCount(int copyBookId)
        {
            return statisticDatabaseContext.ScannedPhotos.Where(sp => sp.CopyBookId == copyBookId).Count();
        }

        public CopyBook AddCopyBook(CopyBook copyBook)
        {
            statisticDatabaseContext.CopyBooks.Add(copyBook);
            statisticDatabaseContext.SaveChanges();
            return copyBook;
        }

        public void DeleteCopyBook(int copyBookId)
        {
            CopyBook copyBook = statisticDatabaseContext.CopyBooks.FirstOrDefault(cb => cb.Id == copyBookId);
            if (copyBook != null)
            {
                statisticDatabaseContext.CopyBooks.Remove(copyBook);
                statisticDatabaseContext.SaveChanges();
            }
        }

        public void CancelChange(object value)
        {
            var entry = statisticDatabaseContext.Entry(value);
            switch (entry.State)
            {
                case EntityState.Modified:
                    //entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }

        public void SaveChanges()
        {
            statisticDatabaseContext.SaveChanges();
        }
    }
}
