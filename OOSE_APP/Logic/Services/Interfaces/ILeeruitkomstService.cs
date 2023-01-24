using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface ILeeruitkomstService
    {
        Task<List<Leeruitkomst>> GetLeeruitkomstenByOpleidingId(int opleidingId, string jwtToken);

        Task<List<Leeruitkomst>> GetAllLeeruitkomsten(string jwtToken);

        Task<Leeruitkomst> GetLeeruitkomstById(int id, string jwtToken);
    }
}
