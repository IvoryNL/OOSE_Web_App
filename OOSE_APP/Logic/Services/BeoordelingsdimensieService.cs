using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class BeoordelingsdimensieService : IBeoordelingsdimensieService
    {
        private readonly IHttpService _httpService;

        public BeoordelingsdimensieService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateBeoordelingsdimensie(Beoordelingsdimensie beoordelingsdimensie, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsdimensie/Create";

            await _httpService.PostAsync(uri, beoordelingsdimensie, jwtToken);
        }

        public async Task<List<Beoordelingsdimensie>> GetAllBeoordelingsdimensies(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsdimensie/GetAll";

            return await _httpService.GetAsync<List<Beoordelingsdimensie>>(uri, jwtToken);
        }

        public async Task<Beoordelingsdimensie> GetBeoordelingsdimensieById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsdimensie/GetById/{id}";

            return await _httpService.GetAsync<Beoordelingsdimensie>(uri, jwtToken);
        }

        public async Task UpdateBeoordelingsdimensie(int id, Beoordelingsdimensie beoordelingsdimensie, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsdimensie/Update/{id}";

            await _httpService.PutAsync(uri, beoordelingsdimensie, jwtToken);
        }
    }
}
