using Newtonsoft.Json;
using StatisticMobileApp.Models;
using StatisticMobileApp.Parameters;
using StatisticMobileApp.Utils;
using StatisticMobileApp.ViewModels;
using StatisticMobileApp.Views;
using StatisticMobileDatabaseLibrary.DatabaseServices;
using StatisticMobileDatabaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StatisticMobileApp.ViewModels
{
    [QueryProperty(nameof(BarCode), nameof(BarCode))]
    class CopyBookDetailViewModel : BaseViewModel
    {
        private CopyBook copyBookModel;
        private StatisticDatabaseServices statisticDatabaseServices;

        private CopyBookDetailParameter _copyBookDetailParameter;

        private string _saveCopyBookText;
        public string SaveCopyBookText
        {
            get { return _saveCopyBookText; }
            set
            {
                _saveCopyBookText = value;
                OnPropertyChanged(nameof(SaveCopyBookText));
            }
        }

        public string BookTitle
        {
            get { return copyBookModel.Title; }
            set
            {
                copyBookModel.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string BarCode
        {
            get { return copyBookModel.BarCode; }
            set
            {
                copyBookModel.BarCode = value;
                OnPropertyChanged(nameof(BarCode));
            }
        }

        public int? PageFrom
        {
            get { return copyBookModel.PageFrom; }
            set
            {
                copyBookModel.PageFrom = value;
                OnPropertyChanged(nameof(PageFrom));
            }
        }

        public int? PageTo
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

        private bool _saveCopyBookVisible;
        public bool SaveCopyBookVisible
        {
            get { return _saveCopyBookVisible; }
            set
            {
                _saveCopyBookVisible = value;
                OnPropertyChanged(nameof(SaveCopyBookVisible));
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

                        }
                        );
                return _appearingCommand;
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
                            await Shell.Current.GoToAsync($"{nameof(BarCodeScanPage)}?BarCode={BarCode}");
                        }
                        );
                return _getCodeCommand;
            }
        }

        private ICommand _scannedPhotoPreviewCommand;
        public ICommand ScannedPhotoPreviewCommand
        {
            get
            {
                if (_scannedPhotoPreviewCommand == null)
                    _scannedPhotoPreviewCommand = new Command<byte[]>(
                        async imageArrayByte =>
                        {
                            AppParameters.ChangeParameter("ImageArrayByte", imageArrayByte);
                            await Shell.Current.GoToAsync($"{nameof(PhotoPreviewPage)}");
                        }
                        );
                return _scannedPhotoPreviewCommand;
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

        private ICommand _saveCopyBookCommand;
        public ICommand SaveCopyBookCommand
        {
            get
            {
                if (_saveCopyBookCommand == null)
                    _saveCopyBookCommand = new Command<object>(
                        async o =>
                        {
                            if (_copyBookDetailParameter.DetailStatus == DetailStatus.Add)
                            {
                                statisticDatabaseServices.AddCopyBook(copyBookModel);
                                await Shell.Current.GoToAsync($"..");
                            }
                            else if (_copyBookDetailParameter.DetailStatus == DetailStatus.Edit)
                            {
                                statisticDatabaseServices.SaveChanges();
                                await Shell.Current.GoToAsync($"..");
                            }
                            else if (_copyBookDetailParameter.DetailStatus == DetailStatus.Show)
                            {

                            }
                        }
                        );
                return _saveCopyBookCommand;
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
                            if (_copyBookDetailParameter.DetailStatus == DetailStatus.Add)
                            {
                                await Shell.Current.GoToAsync($"..");
                            }
                            else if (_copyBookDetailParameter.DetailStatus == DetailStatus.Edit)
                            {
                                statisticDatabaseServices.CancelChange(copyBookModel);
                                await Shell.Current.GoToAsync($"..");
                            }
                            else if (_copyBookDetailParameter.DetailStatus == DetailStatus.Show)
                            {
                                await Shell.Current.GoToAsync($"..");
                            }
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
                            await Shell.Current.GoToAsync($"..");
                        }
                        );
                return _closeBookCommand;
            }
        }

        public CopyBookDetailViewModel(StatisticDatabaseServices statisticDatabaseServices)
        {
            this.statisticDatabaseServices = statisticDatabaseServices;
            _copyBookDetailParameter = (CopyBookDetailParameter)AppParameters.GetParameter("CopyBookDetailParameter");

            if (_copyBookDetailParameter.DetailStatus == DetailStatus.Add)
            {
                copyBookModel = new CopyBook()
                {
                    RegisteredUserId = _copyBookDetailParameter.RegisteredUserId,
                    ScannedPhotos = new ObservableCollection<ScannedPhoto>()
                };
                SaveCopyBookText = "Zapisz";
                SaveCopyBookVisible = true;
                CancelBookVisible = true;
                CloseBookVisible = false;
            }
            else if (_copyBookDetailParameter.DetailStatus == DetailStatus.Edit)
            {
                copyBookModel = statisticDatabaseServices.GetCopyBookWidthScannedPhoto(_copyBookDetailParameter.CopyBookId);
                SaveCopyBookText = "Zapisz";
                SaveCopyBookVisible = true;
                CancelBookVisible = true;
                CloseBookVisible = false;
            }
            else if (_copyBookDetailParameter.DetailStatus == DetailStatus.Show)
            {
                copyBookModel = statisticDatabaseServices.GetCopyBookWidthScannedPhoto(_copyBookDetailParameter.CopyBookId);
                //SaveCopyBookText = "Zapisz";
                SaveCopyBookVisible = false;
                CancelBookVisible = false;
                CloseBookVisible = true;
            }
        }
    }
}
