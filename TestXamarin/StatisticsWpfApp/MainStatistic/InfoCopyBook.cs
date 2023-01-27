using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsWpfApp.MainStatistic
{
    public class InfoCopyBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BarCode { get; set; }
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        public int ImageCount { get; set; }
    }
}
