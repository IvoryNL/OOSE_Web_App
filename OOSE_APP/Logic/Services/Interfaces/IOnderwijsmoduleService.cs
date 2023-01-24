using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IOnderwijsmoduleService
    {
        Task<List<Onderwijsmodule>> GetAllOnderwijsmodulesViaOpleidingId(int opleidingId, string jwtToken);

        Task<Onderwijsmodule> GetOnderwijsmoduleById(int id, string jwtToken);

        Task<Onderwijsmodule> GetOnderwijsmoduleVoorExportById(int id, string jwtToken);

        Task CreateOnderwijsmodule(Onderwijsmodule onderwijsmodule, string jwtToken);

        Task UpdateOnderwijsmodule(int id, Onderwijsmodule onderwijsmodule, string jwtToken);

        Task DeleteOnderwijsmodule(int id, string jwtToken);

        Task VoegOnderwijseenheidToe(int id, Onderwijseenheid onderwijseenheid, string jwtToken);
    }
}
