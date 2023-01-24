using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class LesmateriaalService : ILesmateriaalService
    {

        private readonly IHttpService _httpService;

        public LesmateriaalService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Lesmateriaal>> GetAllLesmaterialen(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Lesmateriaal/GetAll";

            return await _httpService.GetAsync<List<Lesmateriaal>>(uri, jwtToken);
        }

        public async Task<Lesmateriaal> GetLesmateriaalById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Lesmateriaal/GetById/{id}";

            return await _httpService.GetAsync<Lesmateriaal>(uri, jwtToken);
        }
    }
}
