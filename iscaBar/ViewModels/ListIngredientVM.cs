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
        private Product product;
        public Product Product { get { return product; } set { product = value; OnPropertyChanged(); } }

        private Order table;
        public Order Table { get { return table; } set { table = value; OnPropertyChanged(); } }

        private OrderLine orderLine;
        public OrderLine OrderLine { get { return orderLine; } set { orderLine = value; OnPropertyChanged(); } }

        private List<string> ingredientsBorrar;
        public List<string> IngredientsBorrar { get { return ingredientsBorrar; } set { ingredientsBorrar = value; OnPropertyChanged(); } }

        private List<string> ingredientsAfegir;
        public List<string> IngredientsAfegir { get { return ingredientsAfegir; } set { ingredientsAfegir = value; OnPropertyChanged(); } }

        private ObservableCollection<Ingredient> prodIngre;
        public ObservableCollection<Ingredient> ProdIngre { get { return prodIngre; } set { prodIngre = value; OnPropertyChanged(); } }
        public ListIngredientVM(Product prod, Order table)
        {
            OrderLine = new OrderLine();
            Product = prod;
            Table = table;
            ProdIngre = new ObservableCollection<Ingredient>(prod.Ingredients);
            IngredientsBorrar = new List<string>();
            IngredientsAfegir = new List<string>();
            cargarDatos();


        }
        private async Task cargarDatos()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            List<Ingredient> lingredients = await IngredientSDAO.GetAllAsync();
            Ingredients = new ObservableCollection<Ingredient>(lingredients);
        }
        public void DeleteIngre()
        {
            List<Ingredient> ingres = new List<Ingredient>();
            bool flag = false;
            foreach(string s in ingredientsBorrar)
            {
                foreach(Ingredient i in ProdIngre)
                {
                    if(i.Name == s)
                    {
                        ingres.Add(i);
                        flag = true;
                    }
                    if (flag)
                    {
                        flag = false;
                        break;
                    }
                }
                foreach(Ingredient i in ingres)
                {
                    if (ProdIngre.Contains(i))
                    {
                        ProdIngre.Remove(i);
                    }
                }
            }
        }
        public void AddIngre()
        {
            List<Ingredient> ingres = new List<Ingredient>();
            bool flag = false;
            foreach (string s in ingredientsAfegir)
            {
                foreach (Ingredient i in Ingredients)
                {
                    if (i.Name == s)
                    {
                        ingres.Add(i);
                        flag = true;
                    }
                    if (flag)
                    {
                        flag = false;
                        break;
                    }
                }
                foreach (Ingredient i in ingres)
                {
                    if (Ingredients.Contains(i))
                    {
                        Ingredients.Remove(i);
                        ProdIngre.Add(i);
                    }
                }
            }
        }

        public void save(string mssg, int quant)
        {
            string resu = "";
            List<string> inges = new List<string>();
            foreach( string s in ingredientsAfegir)
            {
                if (ingredientsBorrar.Contains(s))
                {
                    ingredientsBorrar.Remove(s);
                    inges.Add(s);
                }
            }
            foreach(string s in inges)
            {
                ingredientsAfegir.Remove(s);
            }
            foreach(string l in ingredientsBorrar)
            {
                resu = resu + "--" + l + ", ";
            }
            foreach (string l in ingredientsAfegir)
            {
                resu = resu + "++" + l + ", ";
            }
            resu = resu + mssg;

            OrderLine.Observations = resu;
            OrderLine.Product = Product;
            OrderLine.Quantity = quant;
            OrderLine.Table = Table;
            OrderLine.Price = Product.Price * quant;

            OrderLine.Id = addApi(OrderLine).Result;
        }

        public async Task<int> addApi(OrderLine orderline)
        {
            return await OrderLineSDAO.AddAsync(OrderLine);
        }
    }
}
