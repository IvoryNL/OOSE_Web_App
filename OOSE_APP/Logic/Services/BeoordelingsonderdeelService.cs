using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class BeoordelingsonderdeelService : IBeoordelingsonderdeelService
    {
        private readonly IHttpService _httpService;

        public BeoordelingsonderdeelService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateBeoordelingsonderdeel(Beoordelingsonderdeel beoordelingsonderdeel, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsonderdeel/Create";

            await _httpService.PostAsync(uri, beoordelingsonderdeel, jwtToken);
        }

        public async Task<List<Beoordelingsonderdeel>> GetAllBeoordelingsonderdelen(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsonderdeel/GetAll";

            return await _httpService.GetAsync<List<Beoordelingsonderdeel>>(uri, jwtToken);
        }

        public async Task<Beoordelingsonderdeel> GetBeoordelingsonderdeelById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsonderdeel/GetById/{id}";

            return await _httpService.GetAsync<Beoordelingsonderdeel>(uri, jwtToken);
        }

        public async Task UpdateBeoordelingsonderdeel(int id, Beoordelingsonderdeel beoordelingsonderdeel, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsonderdeel/Update/{id}";

            await _httpService.PutAsync(uri, beoordelingsonderdeel, jwtToken);
        }
    }
}
