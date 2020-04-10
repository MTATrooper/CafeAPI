using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuanCafeForm.DAO
{
    class ConnectAPI
    {
        private string url = "http://localhost:58638/";
        public string getUrl()
        {
            return url;
        }
        public  T GetObject<T>(string path)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = client.GetAsync(path).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<T>(result);
                return data;
            }

        }
        public T Post<T>(string path,T sv)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<T>(path, sv);
                //client.PostAsync("api/DONHANG/dathang?nguoinhan="+x+"&sdt="++"&diachi={diachi}&idKH={idKH}", null)

                postTask.Wait();
                var kq = postTask.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<T>(kq);
                return data;
            }
        }

        public void PostAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                //HTTP POST
                var postTask = client.PostAsync(uri, null);

                postTask.Wait();
            }
        }
        public void Put<T>(string path, T p)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<T>(path, p);
                putTask.Wait();
            }
        }
        public void Delete(string path, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync(path + id);
                deleteTask.Wait();
            }
        }
    }
}
