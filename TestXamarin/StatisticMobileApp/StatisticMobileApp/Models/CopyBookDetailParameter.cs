using StatisticMobileApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticMobileApp.Models
{
    public class CopyBookDetailParameter
    {
        public DetailStatus DetailStatus { get; set; }
        public int RegisteredUserId { get; set; }
        public int CopyBookId { get; set; }
    }
}
