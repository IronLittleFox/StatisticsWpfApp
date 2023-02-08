using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace StatisticMobileApp.ViewModels
{
    [QueryProperty(nameof(BarCode), nameof(BarCode))]
    class BarCodeScanViewModel : BaseViewModel
    {
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

        private string _codeType;
        public string CodeType
        {
            get { return _codeType; }
            set
            {
                _codeType = value;
                OnPropertyChanged(nameof(CodeType));
            }
        }

        private ICommand _scanResultCommand;
        public ICommand ScanResultCommand
        {
            get
            {
                if (_scanResultCommand == null)
                    _scanResultCommand = new Command<Result>(
                        result =>
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                if (result == null)
                                    return;
                                CodeType = result.BarcodeFormat.ToString();
                                BarCode = result.Text;
                            });
                        }
                        );
                return _scanResultCommand;
            }
        }

        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (okCommand == null)
                    okCommand = new Command<object>(
                        async o =>
                        {
                            await Shell.Current.GoToAsync($"..?BarCode={BarCode}");
                        }
                        );
                return okCommand;
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new Command<object>(
                        async o =>
                        {
                            await Shell.Current.GoToAsync($"..");
                        }
                        );
                return _cancelCommand;
            }
        }
    }
}
