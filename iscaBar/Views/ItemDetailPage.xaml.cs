using iscaBar.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace iscaBar.Views
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