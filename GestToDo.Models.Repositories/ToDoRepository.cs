using GestToDo.Interfaces;
using GestToDo.Models.Data;
using G = GestToDo.Models.Global;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using GestToDo.Models.Repositories.Mappers;
using System.Linq;

namespace GestToDo.Models.Repositories
{
    public class ToDoRepository : IToDoRepository<ToDo>
    {
        private readonly HttpClient _httpClient;

        public ToDoRepository(Uri uri)
        {
            var handler = new HttpClientHandler
            {
                SslProtocols = SslProtocols.Default
            };

            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
            {
                return true;
            };

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = uri;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<ToDo> Get(int userId)
        {
            HttpResponseMessage responseMessage = _httpClient.GetAsync($"todo/getbyuserid/{userId}").Result;
            responseMessage.EnsureSuccessStatusCode();

            string json = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<G.ToDo[]>(json).Select(td => td.ToClient());
        }

        public ToDo Get(int userId, int id)
        {
            HttpResponseMessage responseMessage = _httpClient.GetAsync($"todo/{userId}/{id}").Result;
            responseMessage.EnsureSuccessStatusCode();

            string json = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<G.ToDo>(json)?.ToClient();
        }

        public ToDo Insert(ToDo entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage responseMessage = _httpClient.PostAsync($"todo/", content).Result;
            responseMessage.EnsureSuccessStatusCode();

            string json = responseMessage.Content.ReadAsStringAsync().Result;
            G.ToDo newToDo = JsonConvert.DeserializeObject<G.ToDo>(json);
            return newToDo.ToClient();
        }

        public bool Update(int id, ToDo entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage responseMessage = _httpClient.PutAsync($"todo/{id}", content).Result;
            return responseMessage.IsSuccessStatusCode;
        }

        public bool Delete(int userId, int id)
        {
            HttpResponseMessage responseMessage = _httpClient.DeleteAsync($"todo/{userId}/{id}").Result;
            return responseMessage.IsSuccessStatusCode;
        }
    }
}
