using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticMobileDatabaseLibrary.Entities
{
    public class RegisteredUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
