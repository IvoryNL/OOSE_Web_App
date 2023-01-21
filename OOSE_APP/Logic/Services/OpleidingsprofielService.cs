using Logic.Models;
using Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class OpleidingsprofielService : IOpleidingsprofielService
    {
        private readonly IHttpService _httpService;

        public OpleidingsprofielService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Opleidingsprofiel>> GetAllOpleidingsprofielen(string jwtToken)
        {
            var uri = $"https://localhost:7081/api/Opleidingsprofiel/GetAll";

            return await _httpService.GetAsync<List<Opleidingsprofiel>>(uri, jwtToken);
        }
    }
}
