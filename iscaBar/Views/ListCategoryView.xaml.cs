using iscaBar.Model;
using iscaBar.Models;
using iscaBar.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iscaBar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListCategoryView : ContentPage
    {
        //public ObservableCollection<String> Items = new ObservableCollection<string>();
        public ListCategoriesVM vm;

        private Order table;
        public Order Table { get { return table; } set { table = value; OnPropertyChanged(); } }
        public ListCategoryView(Order table)
        {
            table = Table;
            vm = new ListCategoriesVM();
            InitializeComponent();
            CategoryList.ItemsSource = vm.Items;
        }

        public ListCategoryView(Category cat, Order table)
        {
            Table = table;
            vm = new ListCategoriesVM(cat);
            InitializeComponent();
            CategoryList.ItemsSource = vm.Items;
        }

        async void ProductList(object sender, EventArgs e)
        {
            Category cat = (Category)CategoryList.SelectedItem;
            if (cat == null)
                return;

            if (cat.Child_ids.Count > 0)
            {
                await Navigation.PushAsync(new ListCategoryView(cat,Table));
            }
            else
            {
                await Navigation.PushAsync(new ListProductsView(cat,Table));
            }
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}