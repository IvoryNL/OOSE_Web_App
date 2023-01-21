using Logic.Models;
using Logic.Models.Constants;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class KlasService : IKlasService
    {
        private readonly IHttpService _httpService;

        public KlasService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Klas>> GetAllKlassen(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Klas/GetAll";

            return await _httpService.GetAsync<List<Klas>>(uri, jwtToken);
        }

        public async Task<Klas> GetKlasById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Klas/GetById/{id}";

            return await _httpService.GetAsync<Klas>(uri, jwtToken);
        }

        public async Task<List<Klas>> GetKlasenByOpleidingId(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Klas/GetByOpleidingId/{id}";

            return await _httpService.GetAsync<List<Klas>>(uri, jwtToken);
        }
    }
}
