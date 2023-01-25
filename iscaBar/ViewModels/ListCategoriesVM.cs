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
            carregaDades2();

        }
        //LLAMAMOS AL GetAllAsync PARA LLENAR NUESTRO Items
        private async Task carregaDades()
        {
            List<Category> listCategories = await CategorySDAO.GetAllAsync();
            Items = new ObservableCollection<Category>(listCategories);
        }

        private async Task carregaDades2()
        {
            List<Category> listCategories = await CategorySDAO.GetAllAsync();
            Items = new ObservableCollection<Category>(listCategories);
            var list = buscarFills(Cat);
            Items = list;
        }

        public ObservableCollection<Category> buscarFills(Category cat)
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            foreach(Category c in Items)
            {
                if(c.CatFatherId == cat.Id)
                {
                    categories.Add(c);
                }
            }
            return categories;
        }

        //METODO USADO EN EL 'OnAppearing' DEL .cs
        public void ReordenaLlistaAlumnes()
        {
            //LLENAMOS LA LISTA LEYENDO DE LA BBDD POR EL DAO
            CategorySDAO.GetAllAsync().ContinueWith(
                    x =>
                    {
                        List<Category> list = x.Result;
                        Items = new ObservableCollection<Category>(list);
                    }
                );
        }
    }
}
