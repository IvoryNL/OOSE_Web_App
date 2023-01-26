using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class BeoordelingscriteriaService : IBeoordelingsCriteriaService
    {
        private readonly IHttpService _httpService;

        public BeoordelingscriteriaService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateBeoordelingscriteria(Beoordelingscriteria beoordelingscriteria, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingscriteria/Create";

            await _httpService.PostAsync(uri, beoordelingscriteria, jwtToken);
        }

        public async Task<List<Beoordelingscriteria>> GetAllBeoordelingscriterium(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingscriteria/GetAll";

            return await _httpService.GetAsync<List<Beoordelingscriteria>>(uri, jwtToken);
        }

        public async Task<Beoordelingscriteria> GetBeoordelingscriteriaById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingscriteria/GetById/{id}";

            return await _httpService.GetAsync<Beoordelingscriteria>(uri, jwtToken);
        }

        public async Task UpdateBeoordelingscriteria(int id, Beoordelingscriteria beoordelingscriteria, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordelingscriteria/Update/{id}";

            await _httpService.PutAsync(uri, beoordelingscriteria, jwtToken);
        }
    }
}
