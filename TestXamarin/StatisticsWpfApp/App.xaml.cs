using DLToolkit.Forms.Controls;
using StatisticsWpfApp.Pages.Login;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsWpfApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            MainPage = new NavigationPage(new Login());
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
    }
}
