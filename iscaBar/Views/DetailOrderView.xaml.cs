using iscaBar.Models;
using iscaBar.Services;
using iscaBar.ViewModels;
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
    public partial class DetailOrderView : ContentPage
    {
        private Boolean nou;
        private DetailOrderViewVM detailOrderViewVM;
        public DetailOrderViewVM DetailOrderViewVM { get { return detailOrderViewVM; } set { detailOrderViewVM = value; OnPropertyChanged(); } }
        public DetailOrderView(Order order,Boolean nou)
        {
            InitializeComponent();
            DetailOrderViewVM = new DetailOrderViewVM(order);
            this.nou = nou;
            this.BindingContext = DetailOrderViewVM;
        }


        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            OrderLine orderLine = (OrderLine)e.Item;
            if (e.Item == null)
                return;
            await Navigation.PushAsync(new ListIngredient(orderLine.Product,orderLine.Table));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private void ClickedGuardar(object sender, EventArgs e)
        {
            if (!nou)
            {
                updateOrder();
                PageStackService.Goto(new ListOrdersView());
            }
            else
            {
                addOrder();
                PageStackService.Goto(new ListOrdersView());
            }
        }
        private async Task addOrder()
        {
            detailOrderViewVM.addOrder();
        }
        private async Task updateOrder()
        {
            detailOrderViewVM.updateOrder();
        }
        private void ClickedLimpiar(object sender, EventArgs e)
        {
             Order o = new Order();
            detailOrderViewVM.limpiar(o);
        }

        private async void ClickedAddLine(object sender, EventArgs e)
        {
            Order Table = new Order();
            Table.Num = int.Parse(xnum.Text);
            Table.Diners = int.Parse(xdiners.Text);
            Table.Waiter = xwaiter.Text;
            Table.Client = xclient.Text;
            DetailOrderViewVM.Order = Table;
            await DetailOrderViewVM.addOrder();
            await Navigation.PushAsync(new ListCategoryView(detailOrderViewVM.Order));
        }
        private async Task deleteOrderLine()
        {
            detailOrderViewVM.deteleOrderLineAsync();
            MyListView.ItemsSource = new ObservableCollection<OrderLine>(DetailOrderViewVM.OrderLines);
        }
        private void ClickedDeleteLine(object sender, EventArgs e)
        {

            deleteOrderLine();
        }

        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            OrderLine orderline = (OrderLine)((CheckBox)sender).BindingContext;
            if (orderline == null)
                return;
            detailOrderViewVM.CheckOrderLines.Add(orderline);
        }
    }
}