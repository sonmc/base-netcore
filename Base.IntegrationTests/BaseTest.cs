
using Base.Application;
using Base.IntegrationTests.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text; 

namespace Base.IntegrationTests
{
    public class BaseTest
    {
        protected WebApplicationFactory<Program> webApplication;
        protected HttpClient client;
        protected MockData md;

        protected string token = "";
        protected int SuperAdminId = 1;

        public BaseTest()
        {
            webApplication = new WebApplicationFactory<Program>();
            client = webApplication.CreateClient();
            md = new MockData();
        }

        public async Task<string> GetToken(dynamic model)
        {
            var response = await client.PostAsync("api/auth/login", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string jsonString = await response.Content.ReadAsStringAsync();
            TokenModel tm = JsonConvert.DeserializeObject<TokenModel>(jsonString);

            this.token = tm.Token;

            return tm.Token;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> GetAsync(string uri, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            return await client.GetAsync(uri);
        }
        public async Task<HttpResponseMessage> PostAsync(string uri, object model)
        {
            return await client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
        }
        public async Task<HttpResponseMessage> PostAsync(string uri, object model, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            return await client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            return await client.DeleteAsync(uri);
        }
    }
}
