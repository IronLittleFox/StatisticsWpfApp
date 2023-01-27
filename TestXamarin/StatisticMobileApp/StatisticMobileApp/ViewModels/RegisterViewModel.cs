using StatisticMobileApp.Views;
using StatisticMobileDatabaseLibrary.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StatisticMobileApp.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        private StatisticDatabaseServices statisticDatabaseServices;

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private ICommand _registerCommand;
        public ICommand RegisterCommand
        {
            get
            {
                if (_registerCommand == null)
                    _registerCommand = new Command<object>(
                        async o =>
                        {
                            if (statisticDatabaseServices.IsRegisteredUser(Login))
                            {

                            }
                            else
                            {
                                statisticDatabaseServices.RegisterNewUser(Login, Password);
                                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                            }
                        }
                        );
                return _registerCommand;
            }
        }

        public RegisterViewModel(StatisticDatabaseServices statisticDatabaseServices)
        {
            this.statisticDatabaseServices = statisticDatabaseServices;
        }

    }
}
