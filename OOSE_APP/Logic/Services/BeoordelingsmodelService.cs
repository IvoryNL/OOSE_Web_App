using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class BeoordelingsmodelService : IBeoordelingsmodelService
    {
        private readonly IHttpService _httpService;

        public BeoordelingsmodelService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateBeoordelingsmodel(Beoordelingsmodel beoordelingsmodel, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsmodel/Create";

            await _httpService.PostAsync(uri, beoordelingsmodel, jwtToken);
        }

        public async Task<List<Beoordelingsmodel>> GetAllBeoordelingsmodellen(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsmodel/GetAll";

            return await _httpService.GetAsync<List<Beoordelingsmodel>>(uri, jwtToken);
        }

        public async Task<Beoordelingsmodel> GetBeoordelingsmodelById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsmodel/GetById/{id}";

            return await _httpService.GetAsync<Beoordelingsmodel>(uri, jwtToken);
        }

        public async Task<Beoordelingsmodel> GetBeoordelingsmodelByTentamenId(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsmodel/GetBeoordelingsmodelByTentamenId/{id}";

            return await _httpService.GetAsync<Beoordelingsmodel>(uri, jwtToken);
        }

        public async Task UpdateBeoordelingsmodel(int id, Beoordelingsmodel beoordelingsmodel, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingsmodel/Update/{id}";

            await _httpService.PutAsync(uri, beoordelingsmodel, jwtToken);
        }
    }
}
