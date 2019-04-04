using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CronJobTestCore
{
    public class JsonPlaceholderClient
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            _httpClient.Timeout = new TimeSpan(1, 0, 0);
        }

        public async Task<string> GetTodo(int todoID)
        {
            var responseString = await _httpClient.GetStringAsync("todos/" + todoID.ToString());
            return responseString;
        }
    }
}