using iscaBar.Models;
using iscaBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using iscaBar.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iscaBar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOrdersView : ContentPage
    {
        //private ListOrdersVM listOrdersVM;
        private ListOrdersVM ListOrdersVM;// { get { return listOrdersVM; } set { listOrdersVM = value; OnPropertyChanged(); } }

        public ListOrdersView()
        {
            InitializeComponent();

            ListOrdersVM = new ListOrdersVM();
            BindingContext = ListOrdersVM;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Order order = (Order)e.Item;
            if (e.Item == null)
                return;

            await Navigation.PushAsync(new DetailOrderView(order,false));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        private void OnItemClicked(object sender, EventArgs e)
        {

        }

        private void OnHeaderIconTapped(object sender, EventArgs e)
        {

        }

        async void addOrder(object sender, EventArgs e)
        {
            Order order= new Order();
            await Navigation.PushAsync(new DetailOrderView(order,true));
        }


        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
           Order order = (Order) ((CheckBox)sender).BindingContext;
            if (order == null)
                return;
           ListOrdersVM.CheckOrders.Add(order);
        }

        private async Task confirmOrderAsync()
        {

            await ListOrdersVM.confirmOrder();
        }


        private void confirmOrder(object sender, EventArgs e)
        {
            confirmOrderAsync();

        }

        protected override async void OnAppearing()
        {
            //base.OnAppearing();
            await ListOrdersVM.cargarDatos();
            MyListView.ItemsSource = ListOrdersVM.Orders;

            
        }
    }
}