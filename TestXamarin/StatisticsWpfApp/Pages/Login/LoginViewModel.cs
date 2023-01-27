using StatisticsWpfApp.MainStatistic;
using StatisticsWpfApp.MobileDatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StatisticsWpfApp.Pages.Login
{
    class LoginViewModel : BindableObject
    {
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
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
                            //DatabaseContext databaseContext = new DatabaseContext();
                            //MobileDatabaseService mobileDatabaseService = new MobileDatabaseService();
                            var dd = MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.Users.ToList();
                            if (MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.Users.Any(u => u.Login == Login && u.Password == Password))
                            {
                                int userId = MobileDatabaseService.DatabaseServiceInstance.DatabaseContext.Users.FirstOrDefault(u => u.Login == Login).Id;
                                await Application.Current.MainPage.Navigation.PushAsync(new MainStatisticPage(userId));
                                //ErrorMessage = "Ok";
                            }
                            else
                            {
                                ErrorMessage = "Nie ma takiego konta";
                            }

                        }
                        );
                return _loginCommand;
            }
        }

        private ICommand _signUpCommand;
        public ICommand SignUpCommand
        {
            get
            {
                if (_signUpCommand == null)
                    _signUpCommand = new Command<object>(
                        async o =>
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new SignUp.SignUp());
                        }
                        );
                return _signUpCommand;
            }
        }

    }
}
