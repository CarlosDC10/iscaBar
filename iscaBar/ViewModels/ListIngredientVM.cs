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
    public class ListIngredientVM : ModelBase
    {
        private ObservableCollection<Ingredient> ingredients;
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set
            {
                ingredients = value;
                OnPropertyChanged();
            }
        }
        public ListIngredientVM()
        {
            cargarDatos();


        }
        private async Task cargarDatos()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            List<Ingredient> lingredients = await IngredientSDAO.GetAllAsync();
            Ingredients = new ObservableCollection<Ingredient>(lingredients);
        }
    }
}
