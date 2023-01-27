using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsWpfApp.MainStatistic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainStatisticPage : ContentPage
    {
        public MainStatisticPage(int userId)
        {
            BindingContext = new MainStatisticViewModel(userId);
            
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnAppearing()
        {
            (BindingContext as MainStatisticViewModel).Refresh();
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
    }
}