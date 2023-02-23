using iscaBar.Models;
using iscaBar.Services;
using iscaBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Xamarin.Forms.Xaml;

namespace iscaBar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListIngredient : ContentPage
    {
        private ListIngredientVM listIngredientVMvm;
        public ListIngredientVM ListIngredientVM { get { return listIngredientVMvm; } set { listIngredientVMvm = value; OnPropertyChanged(); } }
        public ListIngredient(Product prod,Order table)
        {
            InitializeComponent();
            ListIngredientVM = new ListIngredientVM(prod,table);
            this.BindingContext= ListIngredientVM;

        }

        private void DeleteIngre(object sender, EventArgs e)
        {
            ListIngredientVM.DeleteIngre();
            BotoDelete.IsEnabled = false;
            observationsEntry.Text = ListIngredientVM.OrderLine.Observations;
        }

        private void AddIngre(object sender, EventArgs e)
        {
            ListIngredientVM.AddIngre();
            BotoAdd.IsEnabled = false;
            observationsEntry.Text = ListIngredientVM.OrderLine.Observations;
        }

        private void Save(object sender, EventArgs e)
        {
            string s = "";
            if(observationsEntry.Text != null)
            {
                s = observationsEntry.Text.Trim();
            }

            int quant = 1;
            if(QuantityEntry.Text != null)
            {
                quant = int.Parse(QuantityEntry.Text);
            }

            ListIngredientVM.save(s,quant);
            PageStackService.Goto(new ListOrdersView());
        }

        private void CheckDelete(object sender, CheckedChangedEventArgs e)
        {
            Ingredient ing = (Ingredient)((CheckBox)sender).BindingContext;
            string ingreDelete = ing.Name;
            if (ingreDelete == null) { return; }

            if (!ListIngredientVM.IngredientsBorrar.Contains(ingreDelete)){
                ListIngredientVM.IngredientsBorrar.Add(ingreDelete);
                BotoDelete.IsEnabled = true;
            }
            else
            {
                ListIngredientVM.IngredientsBorrar.Remove(ingreDelete);
            }
            
        }

        private void CheckAdd(object sender, CheckedChangedEventArgs e)
        {
            Ingredient ing = (Ingredient)((CheckBox)sender).BindingContext;
            string ingreAdd = ing.Name;
            if (ingreAdd == null) { return; }

            if (!ListIngredientVM.IngredientsAfegir.Contains(ingreAdd))
            {
                ListIngredientVM.IngredientsAfegir.Add(ingreAdd);
                BotoAdd.IsEnabled = true;
            }
            else
            {
                ListIngredientVM.IngredientsAfegir.Remove(ingreAdd);
            }
        }
    }
}