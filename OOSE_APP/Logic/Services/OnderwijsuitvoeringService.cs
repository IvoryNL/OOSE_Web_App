using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class OnderwijsuitvoeringService : IOnderwijsuitvoeringService
    {
        private readonly IHttpService _httpService;

        public OnderwijsuitvoeringService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Onderwijsuitvoering>> GetAllOnderwijsuitvoeringen(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsuitvoering/GetAll";

            return await _httpService.GetAsync<List<Onderwijsuitvoering>>(uri, jwtToken);
        }

        public async Task<Onderwijsuitvoering> GetOnderwijsuitvoeringById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsuitvoering/GetById/{id}";

            return await _httpService.GetAsync<Onderwijsuitvoering>(uri, jwtToken);
        }
    }
}
