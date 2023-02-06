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
    public class OrderSDAO
    {
        public static async Task<List<Order>> GetAllAsync()
        {
            string URL = Constant.UrlApi + "bar_app/getAllTable";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(URI);
            List<Order> list = new List<Order>();
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                JArray array = JsonConvert.DeserializeObject<JArray>(content);
                foreach (JObject jObject in array)
                {
                    Order order = new Order();
                    int id = int.Parse(jObject.GetValue("id").ToString());
                    int num = int.Parse(jObject.GetValue("num").ToString());
                    int diners = int.Parse(jObject.GetValue("diners").ToString());
                    string waiter = jObject.GetValue("waiter").ToString();
                    string cliente = jObject.GetValue("client").ToString();
                    JToken llista = jObject.GetValue("products");
                    List<OrderLine> lines = new List<OrderLine>();
                    for(int i = 0; i < llista.Count(); i++)
                    {
                        int idl = int.Parse(llista[i].ToString());
                        OrderLine l = await OrderLineSDAO.GetAsync(idl);
                        lines.Add(l);
                    }
                    decimal total = decimal.Parse(jObject.GetValue("total").ToString());
                    order.OrderLine= lines;
                    order.Id = id;
                    order.Num = num;
                    order.Diners = diners;
                    order.Waiter = waiter;
                    order.Client = cliente;
                    order.Total = total;
                    list.Add(order);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<String> UpdateAsync(Order table)
        {
            string URL = Constant.UrlApi + "bar_app/updateTable";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(table);
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

        public static async Task<String> AddAsync(Order table)
        {
            string URL = Constant.UrlApi + "bar_app/addTable";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(table);
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

        public static async Task<String> DeleteAsync(Order table)
        {
            string URL = Constant.UrlApi + "bar_app/deleteTable";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(table.Id);
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
