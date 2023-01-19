using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class RolesService : IRolesService
    {
        private readonly IHttpService _httpService;

        public RolesService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Rol>> GetAll(string jwtToken)
        {
            var uri = "https://localhost:7081/api/role/getall";

            return await _httpService.GetAsync<List<Rol>>(uri, jwtToken);
        }

        public async Task<Rol> GetById(int id, string jwtToken)
        {
            var uri = "https://localhost:7081/api/role/getbyd/1";

            return await _httpService.GetAsync<Rol>(uri, jwtToken);
        }
    }
}
