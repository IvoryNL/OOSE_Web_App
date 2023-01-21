using Logic.Models.Dto;

namespace Logic.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IngelogdeGebruikerDto> Login(LoginModelDto loginModel);
    }
}
