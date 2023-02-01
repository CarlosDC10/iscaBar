using iscaBar.DAO.Servidor;
using iscaBar.Model;
using iscaBar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace iscaBar.ViewModels
{
    public class ListProductsVM : ModelBase
    {
        private Category category;
        public Category Category { get { return category; } set { category = value; OnPropertyChanged(); } }
        private ObservableCollection<Product> listaProductos;
        public ObservableCollection<Product> ListaProductos
        {
            get { return listaProductos; }
            set
            {
                listaProductos = value;
                OnPropertyChanged();
            }
        }
        public ListProductsVM(Category fatherCategory)
        {
            Category = fatherCategory;
            ListaProductos = new ObservableCollection<Product>(Category.Products);
        }
    }
}