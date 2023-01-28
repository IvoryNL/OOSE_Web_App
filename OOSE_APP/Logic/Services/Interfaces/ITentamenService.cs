using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface ITentamenService
    {
        Task<List<Tentamen>> GetAllTentamens(string jwtToken);

        Task<Tentamen> GetTentamenById(int id, string jwtToken);

        Task<List<Tentamen>> GetAllTentamensVanOnderwijsuitvoeringStudent(int id, string jwtToken);

        Task<List<Tentamen>> GetAllTentamensZonderBeoordelingsmodel(string jwtToken);

        Task<List<Tentamen>> GetAllTentamensZonderBeoordelingsmodelVoorWijziging(int beoordelingsmodelId, string jwtToken);

        Task KoppelLeeruitkomstAanTentamen(int id, Tentamen tentamen, string jwtToken);

        Task OntkoppelLeeruitkomstVanTentamen(int id, int leeruitkomstId, string jwtToken);

        Task InplannenTentamen(int id, Tentamen tentamen, string jwtToken);

        Task VerwijderPlanningVanTentamen(int id, int planningId, string jwtToken);
    }
}
