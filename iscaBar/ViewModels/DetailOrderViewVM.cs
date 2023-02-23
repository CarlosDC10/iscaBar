using iscaBar.DAO.Servidor;
using iscaBar.Model;
using iscaBar.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace iscaBar.ViewModels
{
    public class DetailOrderViewVM:ModelBase
    {
        private List<OrderLine> orderLines;
        public List<OrderLine> OrderLines { get { return orderLines; } set { orderLines = value; OnPropertyChanged(); } }
        private Order order;
        public Order Order { get { return order; } set { order = value; OnPropertyChanged(); } }
        private OrderLine orderLine;
        public OrderLine OrderLine { get { return orderLine; } set { orderLine = value; OnPropertyChanged(); } }
        private List<OrderLine> checkOrderLines;
        public List<OrderLine> CheckOrderLines{ get { return checkOrderLines; } set { checkOrderLines = value; OnPropertyChanged(); } }
        public DetailOrderViewVM(Order o) {
            Order = o;
            OrderLines = o.OrderLine;
            CheckOrderLines = new List<OrderLine>();
        }
        public async Task addOrder()
        {
            int id = await OrderSDAO.AddAsync(Order);
            Order.Id = id;
        }
        public async Task updateOrder()
        {
            OrderSDAO.UpdateAsync(Order);
        }
        public async Task deteleOrderLineAsync()
        {
            foreach (OrderLine ol in CheckOrderLines)
            {
                OrderLineSDAO.DeleteAsync(ol.Id);
                OrderLines.Remove(ol);
            }
        }
        public void limpiar(Order o)
        {
            Order = o;
            OrderLines = o.OrderLine;

        }
        
    }
    
}
