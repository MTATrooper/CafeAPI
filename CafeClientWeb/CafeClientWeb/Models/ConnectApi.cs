using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CafeClientWeb.Models
{
    public class ConnectApi
    {
        public static string path = "http://localhost:58638/";
        public ConnectApi() { }
        public T DeserializeObject<T>(string uri)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);
                HttpResponseMessage response = client.GetAsync(uri).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<T>(result);
                return data;
            }

        }
        public void SendPostRequest<T>(T sv, string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<T>(uri, sv);
                postTask.Wait();
            }
        }
        public void SendPutRequest<T>(T p, string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<T>(uri, p);
                putTask.Wait();
            }
        }
        public void SendDeleteRequest(int id, string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(path);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync(uri + "/" + id);
                deleteTask.Wait();
            }
        }
    }
}