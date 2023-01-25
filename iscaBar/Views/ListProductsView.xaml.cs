using iscaBar.Models;
using iscaBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iscaBar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListProductsView : ContentPage
    {
        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items { get { return items; } set { items = value; OnPropertyChanged(); } }
        private ListProductsVM listProductsVM;
        public ListProductsVM ListProductsVM { get { return listProductsVM; } set { listProductsVM = value; OnPropertyChanged(); } }

        public ListProductsView(Category fatherCategory)
        {
            InitializeComponent();
            listProductsVM = new ListProductsVM(fatherCategory);
            this.BindingContext = listProductsVM;

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}