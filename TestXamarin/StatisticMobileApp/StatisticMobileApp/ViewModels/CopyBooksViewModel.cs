using Newtonsoft.Json;
using StatisticMobileApp.Models;
using StatisticMobileApp.Parameters;
using StatisticMobileApp.Views;
using StatisticMobileDatabaseLibrary.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace StatisticMobileApp.ViewModels
{
    class CopyBooksViewModel : BaseViewModel
    {
        private StatisticDatabaseServices statisticDatabaseServices;
        private int registeredUserId;

        private ObservableCollection<InfoCopyBook> _myCopyBook;
        public ObservableCollection<InfoCopyBook> MyCopyBook 
        {
            get => _myCopyBook;
            set
            {
                _myCopyBook = value;
                OnPropertyChanged();
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
                            RefreshMyCopyBook();
                        }
                        );
                return _appearingCommand;
            }
        }

        private ICommand _addCopyBookCommand;
        public ICommand AddCopyBookCommand
        {
            get
            {
                if (_addCopyBookCommand == null)
                    _addCopyBookCommand = new Command<object>(
                        async o =>
                        {
                            CopyBookDetailParameter copyBookDetailParameter = new CopyBookDetailParameter()
                            {
                                DetailStatus = Utils.DetailStatus.Add,
                                RegisteredUserId = registeredUserId
                            };
                            AppParameters.ChangeParameter("CopyBookDetailParameter", copyBookDetailParameter);
                            await Shell.Current.GoToAsync($"{nameof(CopyBookDetailPage)}");
                            if (true)
                            {

                            }
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
                            CopyBookDetailParameter copyBookDetailParameter = new CopyBookDetailParameter()
                            {
                                DetailStatus = Utils.DetailStatus.Edit,
                                RegisteredUserId = registeredUserId,
                                CopyBookId = id
                            };
                            AppParameters.ChangeParameter("CopyBookDetailParameter", copyBookDetailParameter);
                            await Shell.Current.GoToAsync($"{nameof(CopyBookDetailPage)}");
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
                            statisticDatabaseServices.DeleteCopyBook(id);
                            RefreshMyCopyBook();
                        }
                        );
                return _deleteCopyBookCommand;
            }
        }

        public CopyBooksViewModel(StatisticDatabaseServices statisticDatabaseServices)
        {
            this.statisticDatabaseServices = statisticDatabaseServices;

            this.registeredUserId = (int)AppParameters.GetParameter("RegisteredUserId");

            MyCopyBook = new ObservableCollection<InfoCopyBook>();
            
        }

        private void RefreshMyCopyBook()
        {
            MyCopyBook.Clear();
            foreach (var item in statisticDatabaseServices.GetListOfCopyBooks(registeredUserId))
            {
                MyCopyBook.Add(new InfoCopyBook()
                {
                    Id = item.Id,
                    Title = item.Title,
                    PageFrom = item.PageFrom,
                    PageTo = item.PageTo,
                    BarCode = item.BarCode,
                    ImageCount = statisticDatabaseServices.GetScannedPhotoCount(item.Id)
                });
            }
        }
    }
}
