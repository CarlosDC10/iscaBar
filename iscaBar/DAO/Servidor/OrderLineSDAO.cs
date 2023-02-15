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
    public class OrderLineSDAO
    {
        public static async Task<List<OrderLine>> GetAllAsync()
        {
            string URL = Constant.UrlApi + "bar_app/getAllOrder";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(URI);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                List<OrderLine> list = JsonConvert.DeserializeObject<List<OrderLine>>(content);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<OrderLine> GetAsync(int order)
        {
            string URL = Constant.UrlApi + "bar_app/getAllOrder/"+order;
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> response = client.GetAsync(URI);
            OrderLine o = new OrderLine();
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                JArray array = JsonConvert.DeserializeObject<JArray>(content);
                foreach (JObject jObject in array)
                { 
                    int id = int.Parse(jObject.GetValue("id").ToString());
                    JToken llista = jObject.GetValue("product");
                    int idp = int.Parse(llista[0].ToString());
                    Product p = await ProductSDAO.GetAsync(idp);
                    int quant = int.Parse(jObject.GetValue("quant").ToString());
                    decimal price = decimal.Parse(jObject.GetValue("price").ToString());
                    string observations = jObject.GetValue("observations").ToString();
                    o.Id = id;
                    o.Product = p;
                    o.Quantity = quant;
                    o.Price = price;
                    o.Observations = observations;
                }
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<String> UpdateAsync(OrderLine ord)
        {
            string URL = Constant.UrlApi + "bar_app/updateOrder";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(ord);
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

        public static async Task<bool> AddAsync(OrderLine order)
        {
            string URL = Constant.UrlApi + "bar_app/addOrder";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var dic = new { numTable = order.Table, product = order.Product.Id, quant = order.Quantity };
            string json = JsonConvert.SerializeObject(dic);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(URI, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                JArray array = JsonConvert.DeserializeObject<JArray>(content);
                bool resu = false;
                foreach (JObject jObject in array)
                {
                    String status = jObject.GetValue("status").ToString();
                    if(status == "201")
                    {
                        resu = true;
                        break;
                    }
                    else
                    {
                        resu = false;
                        break;
                    }
                }
                return resu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<String> DeleteAsync(OrderLine ord)
        {
            string URL = Constant.UrlApi + "bar_app/deleteOrder";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var js = JsonConvert.SerializeObject(ord.Id);
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
