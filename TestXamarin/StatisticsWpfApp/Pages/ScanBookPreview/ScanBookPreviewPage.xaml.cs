using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsWpfApp.Pages.ScanBookPreview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanBookPreviewPage : ContentPage
    {
        public ScanBookPreviewPage(byte[] imageArrayByte)
        {
            BindingContext = new ScanBookPreviewViewModel(imageArrayByte);
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