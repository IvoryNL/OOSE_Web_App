using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class LesService : ILesService
    {
        private readonly IHttpService _httpService;

        public LesService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task KoppelLeeruitkomstAanLes(int id, Les les, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Les/KoppelLeeruitkomstAanLes/{id}";

            await _httpService.PutAsync(uri, les, jwtToken);
        }

        public async Task KoppelLesmateriaalAanLes(int id, Les les, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Les/KoppelLesmateriaalAanLes/{id}";

            await _httpService.PutAsync(uri, les, jwtToken);
        }

        public async Task InplannenLes(int id, Les les, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Les/InplannenLes/{id}";

            await _httpService.PutAsync(uri, les, jwtToken);
        }

        public async Task<List<Les>> GetAllLessen(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Les/GetAll";

            return await _httpService.GetAsync<List<Les>>(uri, jwtToken);
        }

        public async Task<Les> GetLesById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Les/GetById/{id}";

            return await _httpService.GetAsync<Les>(uri, jwtToken);
        }

        public async Task OntkoppelLeeruitkomstVanLes(int id, int leeruitkomstId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Les/OntkoppelLeeruitkomstVanLes/{id}/{leeruitkomstId}";

            await _httpService.DeleteAsync(uri, jwtToken);
        }

        public async Task VerwijderPlanningVanLes(int id, int planningId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Les/VerwijderPlanningVanLes/{id}/{planningId}";

            await _httpService.DeleteAsync(uri, jwtToken);
        }

        public async Task OntkoppelLesmateriaalVanLes(int id, int lesmateriaalId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Les/OntkoppelLesmateriaalVanLes/{id}/{lesmateriaalId}";

            await _httpService.DeleteAsync(uri, jwtToken);
        }
    }
}
