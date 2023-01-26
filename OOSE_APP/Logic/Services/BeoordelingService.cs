using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class BeoordelingService : IBeoordelingService
    {
        private readonly IHttpService _httpService;

        public BeoordelingService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateBeoordeling(Beoordeling beoordeling, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Beoordeling/Create";

            await _httpService.PostAsync(uri, beoordeling, jwtToken);
        }
    }
}
