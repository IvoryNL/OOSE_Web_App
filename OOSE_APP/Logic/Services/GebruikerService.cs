using Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Dto;
using Logic.Constants;

namespace Logic.Services
{
    public class GebruikerService : IGebruikerService
    {
        private readonly IHttpService _httpService;

        public GebruikerService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task UpdateGebruiker(int id, VolledigeGebruikerModelDto gebruiker, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Gebruiker/Update/{id}";

            await _httpService.PutAsync(uri, gebruiker, jwtToken);
        }

        public async Task<List<VolledigeGebruikerModelDto>> GetAllGebruikers(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Gebruiker/GetAll";

            var gebruikers = await _httpService.GetAsync<List<VolledigeGebruikerModelDto>>(uri, jwtToken);
            return gebruikers.ToList();
        }

        public async Task<VolledigeGebruikerModelDto> GetGebruikerById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Gebruiker/GetById/{id}";

            return await _httpService.GetAsync<VolledigeGebruikerModelDto>(uri, jwtToken);
        }

        public async Task<List<VolledigeGebruikerModelDto>> GetAllStudenten(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Gebruiker/GetAll";

            var gebruikers = await _httpService.GetAsync<List<VolledigeGebruikerModelDto>>(uri, jwtToken);
            return gebruikers.Where(g => g.Rol.Naam == Rollen.STUDENT).ToList();
        }

        public async Task KoppelStudentAanKlas(int id, VolledigeGebruikerModelDto gebruiker, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Gebruiker/KoppelStudentAanKlas/{id}";

            await _httpService.PutAsync(uri, gebruiker, jwtToken);
        }

        public async Task<VolledigeGebruikerModelDto> GetGebruikerByEmail(string email, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Gebruiker/GetGebruikerByEmail/{email}";

            return await _httpService.GetAsync<VolledigeGebruikerModelDto>(uri, jwtToken);
        }
    }
}
