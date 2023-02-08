using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticMobileDatabaseLibrary.Entities
{
    public class ScannedPhoto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public int CopyBookId { get; set; }
        public CopyBook CopyBook { get; set; }
    }
}
