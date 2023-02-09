using Autofac;
//using Microsoft.Extensions.DependencyInjection;
using StatisticMobileApp.ViewModels;
using StatisticMobileApp.Views;
using StatisticMobileDatabaseLibrary.Context;
using StatisticMobileDatabaseLibrary.DatabaseServices;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

namespace StatisticMobileApp
{
    public partial class App : Application
    {
        private static IContainer Container { get; set; }

        public App()
        {
            DevExpress.XamarinForms.Editors.Initializer.Init();
            DevExpress.XamarinForms.Popup.Initializer.Init();
            DevExpress.XamarinForms.CollectionView.Initializer.Init();
            InitializeComponent();

            SetupServices();
            try
            {
                //Container.Resolve<StatisticDatabaseContext>().OwnMigrate();
            }
            catch (Exception)
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
            var builder = new ContainerBuilder();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "StatisticMobileDatabase.db");
            //File.Delete(dbPath);
            builder.RegisterType<StatisticDatabaseContext>().AsSelf().SingleInstance().WithParameter("sqlConnection", $"Filename={dbPath}");

            builder.RegisterType<StatisticDatabaseServices>().AsSelf();

            builder.RegisterType<AppShellViewModel>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<RegisterViewModel>().AsSelf();
            builder.RegisterType<CopyBooksViewModel>().AsSelf();
            builder.RegisterType<CopyBookDetailViewModel>().AsSelf();
            builder.RegisterType<BarCodeScanViewModel>().AsSelf();
            builder.RegisterType<PhotoPreviewViewModel>().AsSelf();

            Container = builder.Build();
        }

        public static TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel
        {
            return Container.Resolve<TViewModel>();
        }

        //public static TViewModel GetViewModel<TViewModel>(int registeredUserId) where TViewModel : BaseViewModel
        //{
        //    return Container.Resolve<TViewModel>(new NamedParameter("registeredUserId", registeredUserId));
        //}
    }
}
