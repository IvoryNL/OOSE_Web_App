using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IBeoordelingsmodelService
    {
        Task<List<Beoordelingsmodel>> GetAllBeoordelingsmodellen(string jwtToken);

        Task<Beoordelingsmodel> GetBeoordelingsmodelById(int id, string jwtToken);

        Task<Beoordelingsmodel> GetBeoordelingsmodelByTentamenId(int id, string jwtToken);

        Task CreateBeoordelingsmodel(Beoordelingsmodel beoordelingsmodel, string jwtToken);

        Task UpdateBeoordelingsmodel(int id, Beoordelingsmodel beoordelingsmodel, string jwtToken);
    }
}
