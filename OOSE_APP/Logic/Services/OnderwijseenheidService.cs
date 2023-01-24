using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class OnderwijseenheidService : IOnderwijseenheidService
    {
        private readonly IHttpService _httpService;

        public OnderwijseenheidService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task UpdateOnderwijseenheid(int id, Onderwijseenheid onderwijseenheid, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijseenheid/Update/{id}";

            await _httpService.PutAsync(uri, onderwijseenheid, jwtToken);
        }

        public async Task<Onderwijseenheid> GetOnderwijseenheidById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijseenheid/GetById/{id}";

            return await _httpService.GetAsync<Onderwijseenheid>(uri, jwtToken);
        }
    }
}
