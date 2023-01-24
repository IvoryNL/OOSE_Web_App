using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class RolService : IRolService
    {
        private readonly IHttpService _httpService;

        public RolService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Rol>> GetAllRollen(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Rol/GetAll";

            return await _httpService.GetAsync<List<Rol>>(uri, jwtToken);
        }

        public async Task<Rol> GetById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Rol/GetById/{id}";

            return await _httpService.GetAsync<Rol>(uri, jwtToken);
        }
    }
}
