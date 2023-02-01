using iscaBar.Helpers;
using iscaBar.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace iscaBar.DAO.Servidor
{
    public class IngredientSDAO
    {
        public static async Task<List<Ingredient>> GetAllAsync()
        {
            string URL = Constant.UrlApi + "bar_app/getAllIngredient";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(URI);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                List<Ingredient> list = JsonConvert.DeserializeObject<List<Ingredient>>(content);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Ingredient> GetAsync(int ingredient)
        {
            string URL = Constant.UrlApi + "bar_app/getAllIngredient/"+ingredient;
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(URI);
            Ingredient ing = new Ingredient();
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                JArray array = JsonConvert.DeserializeObject<JArray>(content);
                foreach (JObject jObject in array)
                {
                    int id = int.Parse(jObject.GetValue("id").ToString());
                    string name = jObject.GetValue("name").ToString();
                    bool gluten = bool.Parse(jObject.GetValue("gluten").ToString());
                    string obs = jObject.GetValue("observations").ToString();
                    ing.Gluten = gluten;
                    ing.Id = id;
                    ing.Name = name;
                    ing.Observations = obs;
                }
                return ing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<String> UpdateAsync(Ingredient ingre)
        {
            string URL = Constant.UrlApi + "bar_app/updateIngredient";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(ingre);
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

        public static async Task<String> AddAsync(Ingredient ingre)
        {
            string URL = Constant.UrlApi + "bar_app/addIngredient";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(ingre);
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

        public static async Task<String> DeleteAsync(Ingredient ingre)
        {
            string URL = Constant.UrlApi + "bar_app/deleteIngredient";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(ingre.Id);
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
