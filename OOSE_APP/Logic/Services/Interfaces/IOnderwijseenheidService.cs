using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IOnderwijseenheidService
    {
        Task<List<Onderwijseenheid>> GetAllOnderwijseenheden(string jwtToken);

        Task<Onderwijseenheid> GetOnderwijseenheidById(int id, string jwtToken);

        Task<Models.DocumentExportEnImport.Onderwijseenheid> GetOnderwijseenheidVoorExportById(int id, string jwtToken);

        Task UpdateOnderwijseenheid(int id, Onderwijseenheid onderwijseenheid, string jwtToken);
    }
}
