using iscaBar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iscaBar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListIngredient : ContentPage
    {
        private ListIngredientVM listIngredientVMvm;
        public ListIngredientVM ListIngredientVM { get { return listIngredientVMvm; } set { listIngredientVMvm = value; OnPropertyChanged(); } }
        public ListIngredient()
        {
            InitializeComponent();
            ListIngredientVM = new ListIngredientVM();
            this.BindingContext= ListIngredientVM;

        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void Handle_ItemTapped_Add(object sender, ItemTappedEventArgs e)
        {

        }

        private void ClickedGuardar(object sender, EventArgs e)
        {

        }

        private void ClickedAdd(object sender, EventArgs e)
        {

        }

        private void ClickedDelete(object sender, EventArgs e)
        {

        }
    }
}