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

    }
}
