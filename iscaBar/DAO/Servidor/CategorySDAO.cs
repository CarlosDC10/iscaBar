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

namespace iscaBar.DAO.Servidor
{
    public class CategorySDAO
    {
        public static List<int> fillsIds = new List<int>();
        public static async Task<List<Category>> GetAllAsync()
        {
            string URL = Constant.UrlApi + "bar_app/getAllCategory";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(URI);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                JArray array = JsonConvert.DeserializeObject<JArray>(content);
                List<Category> list = new List<Category>();
                foreach (JObject o in array)
                {
                    Category c = new Category();
                    int id = int.Parse(o.GetValue("id").ToString());
                    if (fillsIds.Contains(id)) continue;
                    string name = o.GetValue("name").ToString();
                    string description = o.GetValue("description").ToString();
                    JToken llista = o.GetValue("products");
                    List<Product> products = new List<Product>();
                    for(int i = 0; i < llista.Count(); i++)
                    {
                        int idp = int.Parse(llista[i].ToString());
                        Product p = await ProductSDAO.GetAsync(idp);
                        p.Categories.Add(c);
                        products.Add(p);
                    }
                    JToken child_ids = o.GetValue("child_ids");
                    List<Category> childs = new List<Category>();
                    for (int i = 0; i < child_ids.Count(); i++)
                    {
                        int idc = int.Parse(child_ids[i].ToString());
                        Category fill = await CategorySDAO.GetAsync(idc);
                        fill.Parent_id = id;
                        fillsIds.Add(fill.Id);
                        childs.Add(fill);
                    }
                    c.Id = id;
                    c.Name = name;
                    c.Description = description;
                    c.Products = products;
                    c.Child_ids = childs;
                    list.Add(c);
                }
                return list;
            }catch(Exception ex){
                throw ex;
            }
        }

        public static async Task<Category> GetAsync(int cat)
        {
            string URL = Constant.UrlApi + "bar_app/getAllCategory/" + cat;
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(URI);
            Category c = new Category();
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                JArray array = JsonConvert.DeserializeObject<JArray>(content);
                foreach (JObject ob in array)
                {
                    int id = int.Parse(ob.GetValue("id").ToString());
                    if (fillsIds.Contains(id)) continue;
                    string name = ob.GetValue("name").ToString();
                    string desc = ob.GetValue("description").ToString();
                    JToken llista = ob.GetValue("products");
                    List<Product> products = new List<Product>();
                    for (int i = 0; i < llista.Count(); i++)
                    {
                        int idp = int.Parse(llista[i].ToString());
                        Product p = await ProductSDAO.GetAsync(idp);
                        products.Add(p);
                    }
                    JToken child_ids = ob.GetValue("child_ids");
                    List<Category> childs = new List<Category>();
                    for (int i = 0; i < child_ids.Count(); i++)
                    {
                        int idc = int.Parse(child_ids[i].ToString());
                        Category fill = await CategorySDAO.GetAsync(idc);
                        fillsIds.Add(fill.Id);
                        childs.Add(fill);
                    }
                    c.Id = id;
                    c.Name = name;
                    c.Description = desc;
                    c.Products = products;
                    c.Child_ids = childs;
                }
                return c;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<String> UpdateAsync(Category cat)
        {
            string URL = Constant.UrlApi + "bar_app/updateCategory";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(cat);
            var httpContent = new StringContent(js, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(URI,httpContent);
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

        public static async Task<String> AddAsync(Category cat)
        {
            string URL = Constant.UrlApi + "bar_app/addCategory";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(cat);
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

        public static async Task<String> DeleteAsync(Category cat)
        {
            string URL = Constant.UrlApi + "bar_app/deleteCategory";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(cat.Id);
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
