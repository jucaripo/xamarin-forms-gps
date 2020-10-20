using System.ComponentModel;
using Xamarin.Forms;
using gps.ViewModels;

namespace gps.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}