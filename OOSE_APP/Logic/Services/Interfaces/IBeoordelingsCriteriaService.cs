using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IBeoordelingscriteriaService
    {
        Task<List<Beoordelingscriteria>> GetAllBeoordelingscriterium(string jwtToken);

        Task<Beoordelingscriteria> GetBeoordelingscriteriaById(int id, string jwtToken);

        Task CreateBeoordelingscriteria(Beoordelingscriteria beoordelingscriteria, string jwtToken);

        Task UpdateBeoordelingscriteria(int id, Beoordelingscriteria beoordelingscriteria, string jwtToken);
    }
}
