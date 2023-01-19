namespace Logic.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri, string jwtToken = null);
        Task PostAsync(string uri, object value, string jwtToken = null);
        Task<T> PostAsync<T>(string uri, object value, string jwtToken = null);
        Task PutAsync(string uri, object value, string jwtToken = null);
        Task DeleteAsync(string url, string jwtToken = null);
    }
}
