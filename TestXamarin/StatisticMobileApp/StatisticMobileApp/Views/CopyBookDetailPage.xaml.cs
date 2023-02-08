using Newtonsoft.Json;
using StatisticMobileApp.Models;
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
    public partial class CopyBookDetailPage : ContentPage
    {
        public CopyBookDetailPage()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<CopyBookDetailViewModel>();
        }

        protected override bool OnBackButtonPressed()
        {
            var vm = (CopyBookDetailViewModel)BindingContext;
            if (vm.CancelBookCommand.CanExecute(null))  // You can add parameters if any
            {
                vm.CancelBookCommand.Execute(null); // You can add parameters if any
            }
            return base.OnBackButtonPressed();
        }
    }
}