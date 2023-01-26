using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IBeoordelingsonderdeelService
    {
        Task<List<Beoordelingsonderdeel>> GetAllBeoordelingsonderdelen(string jwtToken);

        Task<Beoordelingsonderdeel> GetBeoordelingsonderdeelById(int id, string jwtToken);

        Task CreateBeoordelingsonderdeel(Beoordelingsonderdeel beoordelingsonderdeel, string jwtToken);

        Task UpdateBeoordelingsonderdeel(int id, Beoordelingsonderdeel beoordelingsonderdeel, string jwtToken);
    }
}
