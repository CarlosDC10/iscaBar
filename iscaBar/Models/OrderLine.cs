using iscaBar.Model;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace iscaBar.Models
{
    [Table("order")]
    public class OrderLine:ModelBase
    {
        private int id;
        private Product product;
        private int quantity;
        private decimal price;
        private string observations;
        private Order table;

        [PrimaryKey,AutoIncrement]
        public int Id { get { return id; } set { id = value; OnPropertyChanged(); } }
        [ManyToOne]
        public Product Product { get { return product; } set { product = value; OnPropertyChanged(); } }
        public int Quantity { get { return quantity; } set { quantity = value; OnPropertyChanged(); } }
        public decimal Price { get { return price; } set { price = value; OnPropertyChanged(); } }
        public string Observations { get { return observations; } set { observations = value;OnPropertyChanged(); } }
        public Order Table { get { return table; } set { table = value; OnPropertyChanged(); } }
    }
}
