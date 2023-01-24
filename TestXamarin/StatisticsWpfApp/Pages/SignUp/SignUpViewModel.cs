using StatisticsWpfApp.Database.Context;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using StatisticsWpfApp.MobileDatabaseServices;
using StatisticsDatabaseLibrary.Entities;

namespace StatisticsWpfApp.Pages.SignUp
{
    class SignUpViewModel : BindableObject
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

        private ICommand _signUpCommand;
        public ICommand SignUpCommand
        {
            get
            {
                if (_signUpCommand == null)
                    _signUpCommand = new Command<object>(
                        async o =>
                        {
                            //DatabaseContext databaseContext = new DatabaseContext();
                            MobileDatabaseService mobileDatabaseService = new MobileDatabaseService();
                            if (mobileDatabaseService.DatabaseService.DatabaseContext.Users.Any(u => u.Login == Login))
                            {
                                ErrorMessage = "Już jest takie konto";
                            }
                            else
                            {
                                mobileDatabaseService.DatabaseService.DatabaseContext.Users.Add(new User()
                                {
                                    Login = Login,
                                    Password = Password
                                });
                                mobileDatabaseService.DatabaseService.DatabaseContext.SaveChanges();
                                await Application.Current.MainPage.Navigation.PushAsync(new Login.Login());
                            }
                        }
                        );
                return _signUpCommand;
            }
        }

    }
}
