using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class ToetsinschrijvingService : IToetsinschrijvingService
    {
        private readonly IHttpService _httpService;

        public ToetsinschrijvingService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateToetsinschrijving(Toetsinschrijving toetsinschrijving, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Toetsinschrijving/Create/";

            await _httpService.PostAsync(uri, toetsinschrijving, jwtToken);
        }
    }
}
