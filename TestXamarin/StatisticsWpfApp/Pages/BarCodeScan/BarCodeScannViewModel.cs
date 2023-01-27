﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;

namespace StatisticsWpfApp.Pages.BarCodeScan
{
    public class BarCodeScannViewModel : BindableObject
    {
        public bool IsScanCodeOk { get; set; }

        private string _codeResult;
        public string CodeResult
        {
            get { return _codeResult; }
            set
            {
                _codeResult = value;
                OnPropertyChanged(nameof(CodeResult));
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
                                CodeResult = result.Text;
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
                            IsScanCodeOk = true;
                            await Application.Current.MainPage.Navigation.PopAsync();
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
                            IsScanCodeOk = false;
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        );
                return _cancelCommand;
            }
        }
    }
}
