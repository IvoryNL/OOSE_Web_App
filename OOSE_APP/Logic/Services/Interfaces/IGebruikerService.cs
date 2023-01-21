using Logic.Models.Dto;

namespace Logic.Services.Interfaces
{
    public interface IGebruikerService
    {
        Task<List<VolledigeGebruikerModelDto>> GetAllGebruikers(string jwtToken);

        Task<List<VolledigeGebruikerModelDto>> GetAllStudenten(string jwtToken);

        Task<VolledigeGebruikerModelDto> GetGebruikerById(int id, string jwtToken);

        Task<VolledigeGebruikerModelDto> GetGebruikerByEmail(string email, string jwtToken);

        Task UpdateGebruiker(int id, VolledigeGebruikerModelDto gebruiker, string jwtToken);

        Task AddGebruikerToKlas(int id, VolledigeGebruikerModelDto gebruiker, string jwtToken); 
    }
}
