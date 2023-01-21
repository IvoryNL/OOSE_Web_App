using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface ILessenService
    {
        Task<List<Les>> GetAllLessen(string jwtToken);
    }
}
