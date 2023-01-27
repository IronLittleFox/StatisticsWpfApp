using StatisticsDatabaseLibrary.Entities;
using StatisticsWpfApp.MobileDatabaseServices;
using StatisticsWpfApp.Pages.BarCodeScan;
using StatisticsWpfApp.Utils;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StatisticsWpfApp.Pages.ScanBookPreview;

namespace StatisticsWpfApp.Pages.ScanBookDetail
{
    class ScanBookDetailViewModel : BindableObject
    {
        private DetailStatus detailStatus;
        private readonly int userId;
        private readonly int? copyBookId;
        private BarCodeScannViewModel barCodeScannViewModel;
        //private MobileDatabaseService mobileDatabaseService;

        private CopyBook copyBookModel;

        private string _addBookText;
        public string AddBookText
        {
            get { return _addBookText; }
            set
            {
                _addBookText = value;
                OnPropertyChanged(nameof(AddBookText));
            }
        }
        //private string _title;
        public string Title
        {
            get { return copyBookModel.Title; }
            set
            {
                copyBookModel.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        //private string _barCode;
        public string BarCode
        {
            get { return copyBookModel.BarCode; }
            set
            {
                copyBookModel.BarCode = value;
                OnPropertyChanged(nameof(BarCode));
            }
        }

        //private int _pageFrom;
        public int PageFrom
        {
            get { return copyBookModel.PageFrom; }
            set
            {
                copyBookModel.PageFrom = value;
                OnPropertyChanged(nameof(PageFrom));
            }
        }

        //private int _pageTo;
        public int PageTo
        {
            get { return copyBookModel.PageTo; }
            set
            {
                copyBookModel.PageTo = value;
                OnPropertyChanged(nameof(PageTo));
            }
        }

        public ObservableCollection<ScannedPhoto> ImageGallery
        { 
            get
            {
                return copyBookModel.ScannedPhotos;
            }
            set
            {
                copyBookModel.ScannedPhotos = value;
                OnPropertyChanged(nameof(ImageGallery));
            }
        }

        private bool _addBookVisible;
        public bool AddBookVisible
        {
            get { return _addBookVisible; }
            set
            {
                _addBookVisible = value;
                OnPropertyChanged(nameof(AddBookVisible));
            }
        }

        private bool _cancelBookVisible;
        public bool CancelBookVisible
        {
            get { return _cancelBookVisible; }
            set
            {
                _cancelBookVisible = value;
                OnPropertyChanged(nameof(CancelBookVisible));
            }
        }

        private bool _closeBookVisible;
        public bool CloseBookVisible
        {
            get { return _closeBookVisible; }
            set
            {
                _closeBookVisible = value;
                OnPropertyChanged(nameof(CloseBookVisible));
            }
        }

        private ICommand _appearingCommand;
        public ICommand AppearingCommand
        {
            get
            {
                if (_appearingCommand == null)
                    _appearingCommand = new Command<object>(
                        o =>
                        {
                            if (barCodeScannViewModel != null && barCodeScannViewModel.IsScanCodeOk)
                            {
                                BarCode = barCodeScannViewModel.CodeResult;
                            }
                        }
                        );
                return _appearingCommand;
            }
        }

        private ICommand _scanBookPreviewCommand;
        public ICommand ScanBookPreviewCommand
        {
            get
            {
                if (_scanBookPreviewCommand == null)
                    _scanBookPreviewCommand = new Command<byte[]>(
                        async imageArrayByte =>
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new ScanBookPreviewPage(imageArrayByte));
                        }
                        );
                return _scanBookPreviewCommand;
            }
        }

        private ICommand _getCodeCommand;
        public ICommand GetCodeCommand
        {
            get
            {
                if (_getCodeCommand == null)
                    _getCodeCommand = new Command<object>(
                        async o =>
                        {
                            barCodeScannViewModel = new BarCodeScannViewModel();
                            await Application.Current.MainPage.Navigation.PushAsync(new BarCodeScannPage(barCodeScannViewModel));
                        }
                        );
                return _getCodeCommand;
            }
        }

        private ICommand _takePhotoCommand;
        public ICommand TakePhotoCommand
        {
            get
            {
                if (_takePhotoCommand == null)
                    _takePhotoCommand = new Command<object>(
                        o =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var photo = await MediaPicker.CapturePhotoAsync();
                                if (photo != null)
                                {
                                    var d = await photo.OpenReadAsync();
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        d.CopyTo(ms);
                                        ImageGallery.Add(new ScannedPhoto() { Photo = ms.ToArray() });
                                    }
                                }
                            });
                        }
                        );
                return _takePhotoCommand;
            }
        }

        private ICommand _addBookCommand;
        public ICommand AddBookCommand
        {
            get
            {
                if (_addBookCommand == null)
                    _addBookCommand = new Command<object>(
                        async o =>
                        {
                            /*CopyBook copyBook = new CopyBook()
                            {
                                Title = Title,
                                PageFrom = PageFrom,
                                PageTo = PageTo,
                                UserId = userId,
                                BarCode = BarCode
                            };
                            mobileDatabaseService.DatabaseService.DatabaseContext.CopyBooks.Add(copyBook);
                            mobileDatabaseService.DatabaseService.DatabaseContext.SaveChanges();
                            mobileDatabaseService.DatabaseService.DatabaseContext.ScannedPhotos.
                                AddRange(ImageGallery.Select(imageByte => new ScannedPhoto() { Photo = imageByte, CopyBookId = copyBook.Id }));*/
                            MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.SaveChanges();
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        );
                return _addBookCommand;
            }
        }

        private ICommand _cancelBookCommand;
        public ICommand CancelBookCommand
        {
            get
            {
                if (_cancelBookCommand == null)
                    _cancelBookCommand = new Command<object>(
                        async o =>
                        {
                            List<EntityEntry> ListToDetach = new List<EntityEntry>();
                            foreach (var entry in MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.ChangeTracker.Entries())
                            {
                                switch (entry.State)
                                {
                                    case EntityState.Modified:
                                        entry.State = EntityState.Unchanged;
                                        break;
                                    case EntityState.Added:
                                        ListToDetach.Add(entry);
                                        //entry.State = EntityState.Detached;
                                        break;
                                    case EntityState.Deleted:
                                        entry.Reload();
                                        break;
                                    default: break;
                                }
                            }
                            ListToDetach.ForEach(ee => ee.State = EntityState.Detached);

                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        );
                return _cancelBookCommand;
            }
        }

        private ICommand _closeBookCommand;
        public ICommand CloseBookCommand
        {
            get
            {
                if (_closeBookCommand == null)
                    _closeBookCommand = new Command<object>(
                        async o =>
                        {
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        );
                return _closeBookCommand;
            }
        }

        public ScanBookDetailViewModel(DetailStatus detailStatus, int userId, int? copyBookId)
        {
            this.detailStatus = detailStatus;
            this.userId = userId;
            this.copyBookId = copyBookId;

            switch (detailStatus)
            {
                case DetailStatus.Add:
                    AddBookText = "Dodaj";
                    AddBookVisible = true;
                    CancelBookVisible = true;
                    CloseBookVisible = false;
                    copyBookModel = new CopyBook();
                    copyBookModel.UserId = userId;
                    copyBookModel.ScannedPhotos = new ObservableCollection<ScannedPhoto>();
                    MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.CopyBooks.Add(copyBookModel);
                    break;
                case DetailStatus.Edit:
                    AddBookText = "Edytuj";
                    AddBookVisible = true;
                    CancelBookVisible = true;
                    CloseBookVisible = false;
                    copyBookModel = MobileDatabaseService.DatabaseServiceInstance.DatabaseContext
                        .CopyBooks.Include(cp => cp.ScannedPhotos).FirstOrDefault(cp => cp.Id == copyBookId);
                    break;
                case DetailStatus.Show:
                    AddBookVisible = false;
                    CancelBookVisible = false;
                    CloseBookVisible = true;
                    copyBookModel = MobileDatabaseService.DatabaseServiceInstance.DatabaseContext
                        .CopyBooks.Include(cp => cp.ScannedPhotos).FirstOrDefault(cp => cp.Id == copyBookId);
                    break;
            }

            //ImageGallery = new ObservableCollection<byte[]>();
        }
    }
}
