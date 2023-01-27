using Microsoft.Extensions.DependencyInjection;
using StatisticMobileApp.Services;
using StatisticMobileApp.ViewModels;
using StatisticMobileApp.Views;
using StatisticMobileDatabaseLibrary.Context;
using StatisticMobileDatabaseLibrary.DatabaseServices;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

namespace StatisticMobileApp
{
    public partial class App : Application
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            DevExpress.XamarinForms.Editors.Initializer.Init();
            DevExpress.XamarinForms.Popup.Initializer.Init();
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            SetupServices();
            try
            {
                ServiceProvider.GetService<StatisticDatabaseContext>().OwnMigrate();
            }
            catch (Exception ex)
            {

                throw;
            }

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        void SetupServices()
        {
            var services = new ServiceCollection();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "StatisticMobileDatabase.db");
            services.AddSingleton<StatisticDatabaseContext>(sdc => new StatisticDatabaseContext($"Filename={dbPath}"));
            services.AddSingleton<StatisticDatabaseServices>();

            // TODO: Add core services here
            services.AddScoped<AppShellViewModel>();
            services.AddScoped<LoginViewModel>();
            services.AddScoped<RegisterViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public static BaseViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel
        {
            return ServiceProvider.GetService<TViewModel>();
        }
    }
}
