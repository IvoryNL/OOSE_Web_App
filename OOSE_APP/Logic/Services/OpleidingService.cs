using Logic.Models;
using Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class OpleidingService : IOpleidingService
    {
        private readonly IHttpService _httpService;

        public OpleidingService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Opleiding>> GetAllOpleidingen(string jwtToken)
        {
            var uri = $"https://localhost:7081/api/Opleiding/GetAll";

            return await _httpService.GetAsync<List<Opleiding>>(uri, jwtToken);
        }
    }
}
