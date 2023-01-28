using Logic.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Web.Http;

namespace Logic.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task DeleteAsync(string uri, string jwtToken = null)
        {
            var request = CreateRequest(HttpMethod.Delete, uri);

            await SendRequest(request, jwtToken);
        }

        public async Task<T> GetAsync<T>(string uri, string jwtToken = null)
        {
            var request = CreateRequest(HttpMethod.Get, uri);

            return await SendRequest<T>(request, jwtToken);
        }

        public async Task PostAsync(string uri, object value, string jwtToken = null)
        {
            var request = CreateRequest(HttpMethod.Post, uri, value);

            await SendRequest(request, jwtToken);
        }

        public async Task<T> PostAsync<T>(string uri, object value, string jwtToken = null)
        {
            var request = CreateRequest(HttpMethod.Post, uri, value);

            return await SendRequest<T>(request, jwtToken);
        }

        public async Task PutAsync(string uri, object value, string jwtToken = null)
        {
            var request = CreateRequest(HttpMethod.Put, uri, value);

            await SendRequest(request, jwtToken);
        }

        private HttpRequestMessage CreateRequest(HttpMethod httpMethod, string uri, object value = null)
        {
            var request = new HttpRequestMessage(httpMethod, uri);

            if (value != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            }

            return request;
        }

        private async Task SendRequest(HttpRequestMessage request, string jwtToken)
        {
            await AddJwtHeader(request, jwtToken);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpResponseException(response);
            }
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage request, string jwtToken)
        {
            await AddJwtHeader(request, jwtToken);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpResponseException(response);
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async Task AddJwtHeader(HttpRequestMessage request, string jwtToken)
        {
            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }
        }
    }
}
