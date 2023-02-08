using StatisticMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarCodeScanPage : ContentPage
    {
        public BarCodeScanPage()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<BarCodeScanViewModel>();
        }
    }
}