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
        public ListCategoryView()
        {
            vm = new ListCategoriesVM();
            InitializeComponent();
            CategoryList.ItemsSource = vm.Items;
        }

        public ListCategoryView(Category cat)
        {
            /*Items.Add("Starters");
            Items.Add("Firsts");
            Items.Add("Seconds");*/
            vm = new ListCategoriesVM(cat);
            InitializeComponent();
            CategoryList.ItemsSource = vm.Items;
        }

        async void ProductList(object sender, EventArgs e)
        {
            Category cat = (Category)CategoryList.SelectedItem;
            if (cat == null)
                return;

            if (cat.Subcategories.Count > 0)
            {
                await Navigation.PushAsync(new ListCategoryView(cat));
            }
            else
            {
                await Navigation.PushAsync(new ListProductsView(cat));
            }
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}