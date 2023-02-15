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
        private ListProductsVM listProductsVM;
        public ListProductsVM ListProductsVM { get { return listProductsVM; } set { listProductsVM = value; OnPropertyChanged(); } }
        private Order table;
        public Order Table { get { return table; } set { table = value; OnPropertyChanged(); } }
        public ListProductsView(Category fatherCategory, Order table)
        {
            Table = table;
            InitializeComponent();
            listProductsVM = new ListProductsVM(fatherCategory);
            this.BindingContext = listProductsVM;

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Product product = (Product)ProdList.SelectedItem;
            if (e.Item == null)
                return;

            await Navigation.PushAsync(new ListIngredient(product, Table));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}