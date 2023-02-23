using iscaBar.Helpers;
using iscaBar.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                    string state = jObject.GetValue("state").ToString();
                    JToken llista = jObject.GetValue("products");
                    List<OrderLine> lines = new List<OrderLine>();
                    for (int i = 0; i < llista.Count(); i++)
                    {
                        int idl = int.Parse(llista[i].ToString());
                        OrderLine l = await OrderLineSDAO.GetAsync(idl);
                        lines.Add(l);
                    }
                    decimal total = decimal.Parse(jObject.GetValue("total").ToString());
                    order.OrderLine = lines;
                    order.Id = id;
                    order.Num = num;
                    order.Diners = diners;
                    order.Waiter = waiter;
                    order.Client = cliente;
                    order.Total = total;
                    order.State = state;
                    list.Add(order);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Boolean> UpdateAsync(Order table)
        {
            var dic = new { id = table.Id, num = table.Num, diners = table.Diners, waiter = table.Waiter, client = table.Client };
            string json = JsonConvert.SerializeObject(dic);
            var client = new HttpClient();
            {
                client.BaseAddress = new Uri(Constant.UrlApi);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("/bar_app/updateTable", content);
                string responseContent = await response.Content.ReadAsStringAsync();

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        //order.Id = data.result.id;
                        return true;
                    }
                    else
                    {
                        throw new Exception("Error al modficar la orden");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public static async Task<Boolean> confirmAsync(Order table)
        {
            var dic = new { id = table.Id, state = 'F', active = false };
            string json = JsonConvert.SerializeObject(dic);
            var client = new HttpClient();
            {
                client.BaseAddress = new Uri(Constant.UrlApi);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("/bar_app/updateTable", content);
                string responseContent = await response.Content.ReadAsStringAsync();

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        //order.Id = data.result.id;
                        return true;
                    }
                    else
                    {
                        throw new Exception("Error al modficar la orden");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public static async Task<int> AddAsync(Order table)
        {
            string URL = Constant.UrlApi + "bar_app/addTable";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var dic = new { num = table.Num, diners = table.Diners, waiter = table.Waiter, client = table.Client };
            string json = JsonConvert.SerializeObject(dic);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(URI, httpContent);
            try
            {
                response.Result.EnsureSuccessStatusCode();
                string content = await response.Result.Content.ReadAsStringAsync();
                JObject result = JsonConvert.DeserializeObject<JObject>(content);
                int id = 0;
                JToken status = result.GetValue("result");
                if (status.Value<string>("status") == "201")
                {
                    id = status.Value<int>("id");
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Boolean> deleteAsync(int id)
        {
            string URL = Constant.UrlApi + "bar_app/deleteTable";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new { id }), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(URI, content);
            String reponseContent = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
