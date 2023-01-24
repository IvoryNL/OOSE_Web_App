using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IOnderwijseenheidService
    {
        Task<Onderwijseenheid> GetOnderwijseenheidById(int id, string jwtToken);

        Task UpdateOnderwijseenheid(int id, Onderwijseenheid onderwijseenheid, string jwtToken);
    }
}
