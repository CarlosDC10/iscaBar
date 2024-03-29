﻿using iscaBar.Helpers;
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

        public static async Task<int> AddAsync(OrderLine order)
        {
            string URL = Constant.UrlApi + "bar_app/addOrder";
            Uri URI = new Uri(URL);
            HttpClient client = new HttpClient();
            var dic = new { numTable = order.Table.Id, product = order.Product.Id, quant = order.Quantity, observations = order.Observations };
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
                if(status.Value<string>("status") == "201")
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

        public static async Task<Boolean> DeleteAsync(int id)
        {
            string URL = Constant.UrlApi + "bar_app/deleteOrder";
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
