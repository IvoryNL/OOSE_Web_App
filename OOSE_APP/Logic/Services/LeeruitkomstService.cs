using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class LeeruitkomstService : ILeeruitkomstService
    {
        private readonly IHttpService _httpService;

        public LeeruitkomstService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Leeruitkomst>> GetLeeruitkomstenByOpleidingId(int opleidingId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Leeruitkomst/GetLeeruitkomstenByOpleidingId/{opleidingId}";

            return await _httpService.GetAsync<List<Leeruitkomst>>(uri, jwtToken);
        }

        public async Task<List<Leeruitkomst>> GetAllLeeruitkomsten(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Leeruitkomst/GetAll";

            return await _httpService.GetAsync<List<Leeruitkomst>>(uri, jwtToken);
        }

        public async Task<Leeruitkomst> GetLeeruitkomstById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Leeruitkomst/GetById/{id}";

            return await _httpService.GetAsync<Leeruitkomst>(uri, jwtToken);
        }

        public async Task Createleeruitkomst(Leeruitkomst leeruitkomst, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Leeruitkomst/Create";

            await _httpService.PostAsync(uri, leeruitkomst, jwtToken);
        }

        public async Task UpdateLeeruitkomst(int id, Leeruitkomst leeruitkomst, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Leeruitkomst/Update/{id}";

            await _httpService.PutAsync(uri, leeruitkomst, jwtToken);
        }
    }
}
