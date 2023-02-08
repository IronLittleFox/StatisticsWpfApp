using StatisticMobileApp.ViewModels;
using StatisticMobileApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace StatisticMobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<AppShellViewModel>();

            Routing.RegisterRoute(nameof(RegisterPage),typeof(RegisterPage));
            Routing.RegisterRoute(nameof(CopyBookDetailPage),typeof(CopyBookDetailPage));
            Routing.RegisterRoute(nameof(BarCodeScanPage),typeof(BarCodeScanPage));
            Routing.RegisterRoute(nameof(PhotoPreviewPage),typeof(PhotoPreviewPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
