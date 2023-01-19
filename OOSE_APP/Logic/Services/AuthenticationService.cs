using Logic.Models;
using Logic.Models.Constants;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class AuthenticationService : IUserService
    {
        private readonly IHttpService _httpService;

        public AuthenticationService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<LoggedInUserModel> Login(LoginModel loginModel)
        {
            var uri = $"{ApiUrl.BASE_URL}/Authentication/Login";

            return await _httpService.PostAsync<LoggedInUserModel>(uri, loginModel);
        }
    }
}