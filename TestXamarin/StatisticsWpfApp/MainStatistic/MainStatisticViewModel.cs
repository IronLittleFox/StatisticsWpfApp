using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using StatisticsDatabaseLibrary.Entities;
using StatisticsWpfApp.MobileDatabaseServices;
using StatisticsWpfApp.Pages.ScanBookDetail;
using StatisticsWpfApp.Utils;

namespace StatisticsWpfApp.MainStatistic
{
    class MainStatisticViewModel : BindableObject
    {
        //DatabaseContext databaseContext;
        //MobileDatabaseService mobileDatabaseService;
        int userId;

        public ObservableCollection<InfoCopyBook> MyCopyBook { get; set; }

        private ICommand _addCopyBookCommand;
        public ICommand AddCopyBookCommand
        {
            get
            {
                if (_addCopyBookCommand == null)
                    _addCopyBookCommand = new Command<object>(
                        async o =>
                        {
                            
                            await Application.Current.MainPage.Navigation.PushAsync(new ScanBookDetailPage(DetailStatus.Add, userId, null));
                            
                        }
                        );
                return _addCopyBookCommand;
            }
        }

        private ICommand _editCopyBookCommand;
        public ICommand EditCopyBookCommand
        {
            get
            {
                if (_editCopyBookCommand == null)
                    _editCopyBookCommand = new Command<int>(
                        async id =>
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new ScanBookDetailPage(DetailStatus.Edit, userId, id));
                        }
                        );
                return _editCopyBookCommand;
            }
        }

        private ICommand _deleteCopyBookCommand;
        public ICommand DeleteCopyBookCommand
        {
            get
            {
                if (_deleteCopyBookCommand == null)
                    _deleteCopyBookCommand = new Command<int>(
                        id =>
                        {
                            CopyBook copyBook = MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.CopyBooks.FirstOrDefault(cb => cb.Id == id);
                            var d = MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.CopyBooks.ToList();
                            MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.CopyBooks.Remove(copyBook);
                            MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.SaveChanges();
                            InfoCopyBook infoCopyBook = MyCopyBook.FirstOrDefault(cb => cb.Id == id);
                            MyCopyBook.Remove(infoCopyBook);
                        }
                        );
                return _deleteCopyBookCommand;
            }
        }

        public MainStatisticViewModel(int userId)
        {
            this.userId = userId;
            //databaseContext = new DatabaseContext();
            //mobileDatabaseService = new MobileDatabaseService();
            MyCopyBook = new ObservableCollection<InfoCopyBook>();
            MyCopyBook.Clear();
            foreach (var item in MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.CopyBooks.Where(cb => cb.UserId == userId))
            {
                MyCopyBook.Add(new InfoCopyBook() 
                { 
                    Id = item.Id,
                    Title = item.Title,
                    PageFrom = item.PageFrom,
                    PageTo = item.PageTo,
                    BarCode = item.BarCode,
                    ImageCount = MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.ScannedPhotos.Where(sp => sp.CopyBookId == item.Id).Count()
                });
            }
        }

        public void Refresh()
        {
            MyCopyBook.Clear();
            foreach (var item in MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.CopyBooks.Where(cb => cb.UserId == userId))
            {
                MyCopyBook.Add(new InfoCopyBook()
                {
                    Id = item.Id,
                    Title = item.Title,
                    PageFrom = item.PageFrom,
                    PageTo = item.PageTo,
                    BarCode = item.BarCode,
                    ImageCount = MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.ScannedPhotos.Where(sp => sp.CopyBookId == item.Id).Count()
                });
            }
        }
    }
}
