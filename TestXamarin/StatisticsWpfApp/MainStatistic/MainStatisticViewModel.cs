using StatisticsWpfApp.Database.Context;
using StatisticsWpfApp.Database.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using StatisticsWpfApp.AddBook;
using StatisticsDatabaseLibrary.Entities;
using StatisticsWpfApp.MobileDatabaseServices;

namespace StatisticsWpfApp.MainStatistic
{
    class MainStatisticViewModel : BindableObject
    {
        //DatabaseContext databaseContext;
        MobileDatabaseService mobileDatabaseService;
        int userId;

        public ObservableCollection<CopyBook> MyCopyBook { get; set; }

        private ICommand _addCopyBookCommand;
        public ICommand AddCopyBookCommand
        {
            get
            {
                if (_addCopyBookCommand == null)
                    _addCopyBookCommand = new Command<object>(
                        async o =>
                        {
                            
                            await Application.Current.MainPage.Navigation.PushAsync(new AddBookPage(userId));
                            
                        }
                        );
                return _addCopyBookCommand;
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
                            CopyBook copyBook = mobileDatabaseService.DatabaseService.DatabaseContext.CopyBooks.FirstOrDefault(cb => cb.Id == id);
                            var d = mobileDatabaseService.DatabaseService.DatabaseContext.CopyBooks.ToList();
                            mobileDatabaseService.DatabaseService.DatabaseContext.CopyBooks.Remove(copyBook);
                            mobileDatabaseService.DatabaseService.DatabaseContext.SaveChanges();
                            copyBook = MyCopyBook.FirstOrDefault(cb => cb.Id == id);
                            MyCopyBook.Remove(copyBook);
                        }
                        );
                return _deleteCopyBookCommand;
            }
        }

        public MainStatisticViewModel(int userId)
        {
            this.userId = userId;
            //databaseContext = new DatabaseContext();
            mobileDatabaseService = new MobileDatabaseService();
            MyCopyBook = new ObservableCollection<CopyBook>();
            MyCopyBook.Clear();
            foreach (var item in mobileDatabaseService.DatabaseService.DatabaseContext.CopyBooks.Where(cb => cb.UserId == userId))
            {
                MyCopyBook.Add(new CopyBook() 
                { 
                    Title = item.Title,
                    PageFrom = item.PageFrom,
                    PageTo = item.PageTo,
                    BarCode = item.BarCode
                });
            }
        }


        public void Refresh()
        {
            MyCopyBook.Clear();
            foreach (var item in mobileDatabaseService.DatabaseService.DatabaseContext.CopyBooks.Where(cb => cb.UserId == userId))
            {
                MyCopyBook.Add(new CopyBook()
                {
                    Id = item.Id,
                    Title = item.Title,
                    PageFrom = item.PageFrom,
                    PageTo = item.PageTo,
                    BarCode = item.BarCode
                });
            }
        }
    }
}
