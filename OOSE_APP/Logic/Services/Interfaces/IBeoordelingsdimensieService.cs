using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IBeoordelingsdimensieService
    {
        Task<List<Beoordelingsdimensie>> GetAllBeoordelingsdimensies(string jwtToken);

        Task<Beoordelingsdimensie> GetBeoordelingsdimensieById(int id, string jwtToken);

        Task CreateBeoordelingsdimensie(Beoordelingsdimensie beoordelingsdimensie, string jwtToken);

        Task UpdateBeoordelingsdimensie(int id, Beoordelingsdimensie beoordelingsdimensie, string jwtToken);
    }
}
