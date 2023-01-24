using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsDatabaseLibrary.Entities
{
    public class CopyBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BarCode { get; set; }
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
