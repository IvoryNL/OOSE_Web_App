using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class LeerdoelService : ILeerdoelService
    {
        private readonly IHttpService _httpService;

        public LeerdoelService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateLeerdoel(Leerdoel leerdoel, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Leerdoel/Create";

            await _httpService.PostAsync(uri, leerdoel, jwtToken);
        }

        public async Task UpdateLeerdoel(int id, Leerdoel leerdoel, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Leerdoel/Update/{id}";

            await _httpService.PutAsync(uri, leerdoel, jwtToken);
        }

        public async Task<Leerdoel> GetLeerdoelById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Leerdoel/GetById/{id}";

            return await _httpService.GetAsync<Leerdoel>(uri, jwtToken);
        }
    }
}
