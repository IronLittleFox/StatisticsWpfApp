using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWpfApp.Database.Entities
{
    class CopyBook_TMP
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        public User_TMP User { get; set; }
        public int UserId { get; set; }
    }
}
