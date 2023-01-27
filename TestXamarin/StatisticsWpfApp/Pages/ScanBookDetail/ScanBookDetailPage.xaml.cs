using StatisticsWpfApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsWpfApp.Pages.ScanBookDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanBookDetailPage : ContentPage
    {
        public ScanBookDetailPage(DetailStatus detailStatus, int userId, int? copyBookId)
        {
            BindingContext = new ScanBookDetailViewModel(detailStatus, userId, copyBookId);
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