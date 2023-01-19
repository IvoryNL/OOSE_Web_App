using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoggedInUserModel> Login(LoginModel loginModel);
    }
}
