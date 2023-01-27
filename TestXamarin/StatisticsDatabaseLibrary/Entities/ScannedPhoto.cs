using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StatisticsDatabaseLibrary.Entities
{
    public class ScannedPhoto
    {
        [Key]
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public int CopyBookId { get; set; }
        public CopyBook CopyBook { get; set; }
    }
}
