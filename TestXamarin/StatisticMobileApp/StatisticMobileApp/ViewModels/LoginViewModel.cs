using StatisticMobileApp.Parameters;
using StatisticMobileApp.Views;
using StatisticMobileDatabaseLibrary.DatabaseServices;
using StatisticMobileDatabaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StatisticMobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
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

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                    _loginCommand = new Command<object>(
                        async o =>
                        {
                            RegisteredUser registeredUser = statisticDatabaseServices.GetRegisteredUser(Login, Password);
                            if (registeredUser != null)
                            {
                                AppParameters.ChangeParameter("RegisteredUserId", registeredUser.Id);
                                await Shell.Current.GoToAsync($"//{nameof(CopyBooksPage)}");
                            }
                            else
                            {

                            }
                        }
                        );
                return _loginCommand;
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
                            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
                        }
                        );
                return _registerCommand;
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
                            Login = "";
                            Password = "";
                        }
                        );
                return _appearingCommand;
            }
        }

        public LoginViewModel(StatisticDatabaseServices statisticDatabaseServices)
        {
            this.statisticDatabaseServices = statisticDatabaseServices;

        }
    }
}
