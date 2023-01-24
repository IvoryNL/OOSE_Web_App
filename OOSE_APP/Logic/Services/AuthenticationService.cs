using Logic.Constants;
using Logic.Models.Dto;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;

        public AuthenticationService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IngelogdeGebruikerDto> Login(LoginModelDto loginModel)
        {
            var uri = $"{ApiUrl.BASE_URL}/Authentication/Login";

            return await _httpService.PostAsync<IngelogdeGebruikerDto>(uri, loginModel);
        }
    }
}