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
            category = fatherCategory;
            cargarDatos();
        }
        private async Task cargarDatos()
        {
            ListaProductos = new ObservableCollection<Product>();

            List<Product> lproductos = await ProductSDAO.GetAllAsync();

            foreach (int id in category.Products)
            {
                foreach (Product product in lproductos)
                {
                    if (id == product.Id)
                    {
                        ListaProductos.Add(product);
                        break;
                    }
                }
            }



        }


    }
}