using StatisticsDatabaseLibrary.Entities;
using StatisticsWpfApp.Database.Context;
using StatisticsWpfApp.MobileDatabaseServices;
using StatisticsWpfApp.Pages.BarCodeScan;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StatisticsWpfApp.AddBook
{
    class AddBookViewModel : BindableObject
    {
        private readonly int userId;
        //DatabaseContext databaseContext;
        MobileDatabaseService mobileDatabaseService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private int _pageFrom;
        public int PageFrom
        {
            get { return _pageFrom; }
            set
            {
                _pageFrom = value;
                OnPropertyChanged(nameof(PageFrom));
            }
        }

        private int _pageTo;
        public int PageTo
        {
            get { return _pageTo; }
            set
            {
                _pageTo = value;
                OnPropertyChanged(nameof(PageTo));
            }
        }

        private string _barCode;
        public string BarCode
        {
            get { return _barCode; }
            set
            {
                _barCode = value;
                OnPropertyChanged(nameof(BarCode));
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
                            mobileDatabaseService.DatabaseService.DatabaseContext.CopyBooks.Add(new CopyBook()
                            {
                                Title = Title,
                                PageFrom = PageFrom,
                                PageTo = PageTo,
                                UserId = userId,
                                BarCode = BarCode
                            });
                            mobileDatabaseService.DatabaseService.DatabaseContext.SaveChanges();
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
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        );
                return _cancelBookCommand;
            }
        }

        private ICommand _getCodeCommand;
        public ICommand GetCodeGommand
        {
            get
            {
                if (_getCodeCommand == null)
                    _getCodeCommand = new Command<object>(
                        async o =>
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new BarCodeScannPage());
                        }
                        );
                return _getCodeCommand;
            }
        }

        public AddBookViewModel(int userId)
        {
            this.userId = userId;
            //databaseContext = new DatabaseContext();
            mobileDatabaseService = new MobileDatabaseService();
        }
    }
}
