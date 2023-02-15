
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iscaBar.Services
{
    public  class PageStackService
    {
        public static async Task Goto(Page page)
        {
            Page pageInStack;
            bool trobada= false;
            int pos = 0;
            //string pila="";
            //foreach (Page p in App.Current.MainPage.Navigation.NavigationStack)
            //{
            //    pila += p.Title + " ";
            //}
            //await App.Current.MainPage.DisplayAlert("Pila abans", pila, "ok");
                foreach (Page p in App.Current.MainPage.Navigation.NavigationStack)
            {
                if (p.Title.ToLower()== page.Title.ToLower())
                {
                    pageInStack=Application.Current.MainPage.Navigation.NavigationStack[pos];
                    trobada =true;
                    break;
                }
                pos++;
            }
            if (!trobada) {
                await App.Current.MainPage.Navigation.PushAsync(page);
            }
            else
            {
                int ini = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
                for (int i= ini; i>pos;i--)
                {
                    //App.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[i]);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                
            }
            //pila = "";
            //foreach (Page p in App.Current.MainPage.Navigation.NavigationStack)
            //{
            //    pila += p.Title + " ";
            //}
            //await App.Current.MainPage.DisplayAlert("Pila despres", pila, "ok");
        }
    }
}
