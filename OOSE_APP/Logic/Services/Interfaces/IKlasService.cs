using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IKlasService
    {
        Task<List<Klas>> GetAllKlassen(string jwtToken);

        Task<Klas> GetKlasById(int id, string jwtToken);

        Task<List<Klas>> GetKlasenByOpleidingId(int id, string jwtToken);
    }
}
