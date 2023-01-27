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

            //Do skasowania
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            Routing.RegisterRoute(nameof(RegisterPage),typeof(RegisterPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
