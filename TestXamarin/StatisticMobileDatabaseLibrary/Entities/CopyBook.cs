using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StatisticMobileDatabaseLibrary.Entities
{
    public class CopyBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BarCode { get; set; }
        public int? PageFrom { get; set; }
        public int? PageTo { get; set; }
        public RegisteredUser RegisteredUser { get; set; }
        public int RegisteredUserId { get; set; }
        public ObservableCollection<ScannedPhoto> ScannedPhotos { get; set; }
    }
}
