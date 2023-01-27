using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StatisticsWpfApp.Pages.ScanBookPreview
{
    public class ScanBookPreviewViewModel : BindableObject
    {
        public byte[] arrayOfByte { get; }


        private byte[] _imageArrayByte;
        public byte[] ImageArrayByte
        {
            get { return _imageArrayByte; }
            set
            {
                _imageArrayByte = value;
                OnPropertyChanged(nameof(ImageArrayByte));
            }
        }

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new Command<object>(
                        async o =>
                        {
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        );
                return _closeCommand;
            }
        }

        public ScanBookPreviewViewModel(byte[] imageArrayByte)
        {
            _imageArrayByte = imageArrayByte;
        }
    }
}
