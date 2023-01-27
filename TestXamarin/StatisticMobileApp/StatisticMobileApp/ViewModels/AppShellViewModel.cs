using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StatisticMobileApp.ViewModels
{
    class AppShellViewModel: BaseViewModel
    {
        private ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                    _logoutCommand = new Command<object>(
                        async o =>
                        {
                            Shell.Current.FlyoutIsPresented = false;
                            await Shell.Current.GoToAsync("//LoginPage");
                        }
                        );
                return _logoutCommand;
            }
        }
    }
}
