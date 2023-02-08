using Newtonsoft.Json;
using StatisticMobileApp.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StatisticMobileApp.ViewModels
{
    class PhotoPreviewViewModel : BaseViewModel
    {
        private string serializeJsonImageArrayByte;

        public string SerializeJsonImageArrayByte
        {
            get { return serializeJsonImageArrayByte; }
            set 
            { 
                serializeJsonImageArrayByte = value;
                ImageArrayByte = JsonConvert.DeserializeObject<byte[]>(value);
            }
        }

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
                            await Shell.Current.GoToAsync($"..");
                        }
                        );
                return _closeCommand;
            }
        }

        public PhotoPreviewViewModel()
        {
            ImageArrayByte = (byte[])AppParameters.GetParameter("ImageArrayByte");
        }
    }
}
