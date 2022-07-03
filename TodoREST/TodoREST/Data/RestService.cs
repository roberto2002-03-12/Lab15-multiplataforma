using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodoREST.Model;
using Xamarin.Forms;

namespace TodoREST.Data
{
    public class RestService : IRestService
    {
        HttpClient client;

        public List<TodItem> Items { get; private set; }

        public RestService()
        {
#if DEBUG
            client = new HttpClient(DependencyService.Get<IHttpCHService>().GetInsecureHandler());

#else
            client = new HttpClient();
#endif
        }

        public async Task<List<TodItem>> RefreshDataAsync()
        {
            Items = new List<TodItem>();
            string action = "List";

            var uri = new Uri(string.Format(Constants.RestUrl, action));
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<TodItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tError {0}", ex.Message);
            }
            return Items;
        }

        public async Task SaveTodoItemAsync(TodItem item, bool isNewItem = false)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(String.Format(Constants.RestUrl, "Create"));
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(String.Format(Constants.RestUrl, "Edit"));
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully saved");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tError {0}", ex.Message);
            }
        }

        public async Task DeleteTodoItemAsync(string id)
        {
            var uri = new Uri(String.Format(Constants.RestUrl, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tError {0}", ex.Message);
            }
        }
    }
}
