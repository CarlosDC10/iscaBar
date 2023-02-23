using iscaBar.DAO.Servidor;
using iscaBar.Model;
using iscaBar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iscaBar.ViewModels
{
    public class ListOrdersVM : ModelBase
    {
        private Order order;
        public Order Order { get { return order; } set { order = value; OnPropertyChanged(); } }
        private List<Order> checkOrders;
        public List<Order> CheckOrders { get { return checkOrders; } set { checkOrders = value; OnPropertyChanged(); } }
        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged();
            }
        }
        public ListOrdersVM()
        {
            //cargarDatos();
        }
        public async Task cargarDatos()
        {
            List<Order> lOrders = await OrderSDAO.GetAllAsync();
            Orders = new ObservableCollection<Order>(lOrders);
            //OnPropertyChanged("Orders");
            CheckOrders = new List<Order>();
        }
        public async Task confirmOrder()
        {
                foreach (Order o in CheckOrders)
                {
                    await OrderSDAO.confirmAsync(o);
                    Orders.Remove(o);
                }
        }
    }
}
