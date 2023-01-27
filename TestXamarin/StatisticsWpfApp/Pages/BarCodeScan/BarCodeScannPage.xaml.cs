using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsWpfApp.Pages.BarCodeScan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarCodeScannPage : ContentPage
    {
        public BarCodeScannPage(BarCodeScannViewModel barCodeScannViewModel)
        {
            BindingContext = barCodeScannViewModel;
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
    }
}