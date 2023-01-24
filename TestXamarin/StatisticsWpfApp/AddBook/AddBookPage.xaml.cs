using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StatisticsWpfApp.AddBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBookPage : ContentPage
    {
        public AddBookPage(int userId)
        {
            BindingContext = new AddBookViewModel(userId);
            InitializeComponent();
        }
    }
}