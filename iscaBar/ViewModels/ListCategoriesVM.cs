using iscaBar.DAO.Servidor;
using iscaBar.Helpers;
using iscaBar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace iscaBar.ViewModel
{
    public class ListCategoriesVM : Base
    {
        //CREEM IEMS PER A BINDEJAR
        //LLISTA DE CATEGORIES
        private ObservableCollection<Category> _Items;
        public ObservableCollection<Category> Items { get { return _Items; } set { _Items = value; OnPropertyChanged(); } }
        private Category cat;
        public Category Cat { get { return cat; } set { cat = value; OnPropertyChanged(); } }

        //CONSTRUCTOR
        public ListCategoriesVM()
        {
            carregaDades();

        }

        public ListCategoriesVM(Category cat)
        {
            Cat = cat;
            carregaDades2(cat);

        }
        //LLAMAMOS AL GetAllAsync PARA LLENAR NUESTRO Items
        private async Task carregaDades()
        {
            List<Category> listCategories = await CategorySDAO.GetAllAsync();
            Items = new ObservableCollection<Category>(listCategories);
            List<Order> tables = await OrderSDAO.GetAllAsync();
            List<OrderLine> lines = await OrderLineSDAO.GetAllAsync();
        }

        public void carregaDades2(Category cat)
        {
            List<Category> listCategories = cat.Child_ids;
            Items = new ObservableCollection<Category>(listCategories);
        }

        public void buscarFills(Category cat)
        {
            Cat = cat;
            ObservableCollection<Category> categories = new ObservableCollection<Category>(cat.Child_ids);
            Items = categories;
        }
    }
}
