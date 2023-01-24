using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface ILesmateriaalService
    {
        Task<List<Lesmateriaal>> GetAllLesmaterialen(string jwtToken);

        Task<Lesmateriaal> GetLesmateriaalById(int id, string jwtToken);
    }
}
