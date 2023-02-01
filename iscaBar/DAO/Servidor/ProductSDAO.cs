using iscaBar.Helpers;
using iscaBar.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace iscaBar.DAO.Servidor
{
    public class ProductSDAO
    {
        public static async Task<List<Product>> GetAllAsync()
        {
            string URL = Constant.UrlApi + "bar_app/getAllProduct";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(URI);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                List<Product> list = JsonConvert.DeserializeObject<List<Product>>(content);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Product> GetAsync(int product)
        {
            string URL = Constant.UrlApi + "bar_app/getAllProduct/"+product;
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(URI);
            Product p = new Product();
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                JArray array = JsonConvert.DeserializeObject<JArray>(content);
                foreach (JObject ob in array)
                {
                    int id = int.Parse(ob.GetValue("id").ToString());
                    string name = ob.GetValue("name").ToString();
                    string desc = ob.GetValue("description").ToString();
                    decimal price = decimal.Parse(ob.GetValue("price").ToString());
                    JToken ingredients = ob.GetValue("ingredients");
                    List<Ingredient> ingres = new List<Ingredient>();
                    for (int i = 0; i < ingredients.Count(); i++)
                    {
                        Ingredient ing = await IngredientSDAO.GetAsync(int.Parse(ingredients[i].ToString()));
                        ingres.Add(ing);
                    }
                    p.Id = id;
                    p.Name = name;
                    p.Description = desc;
                    p.Price = price;
                    p.Ingredients = ingres;
                }
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<String> UpdateAsync(Product prod)
        {
            string URL = Constant.UrlApi + "bar_app/updateProduct";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(prod);
            var httpContent = new StringContent(js, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(URI, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                String list = JsonConvert.DeserializeObject<String>(content);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<String> AddAsync(Product prod)
        {
            string URL = Constant.UrlApi + "bar_app/addProduct";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(prod);
            var httpContent = new StringContent(js, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(URI, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                String list = JsonConvert.DeserializeObject<String>(content);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<String> DeleteAsync(Product prod)
        {
            string URL = Constant.UrlApi + "bar_app/deleteProduct";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(prod.Id);
            var httpContent = new StringContent(js, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(URI, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                String list = JsonConvert.DeserializeObject<String>(content);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
