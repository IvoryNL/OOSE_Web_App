using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IRolService
    {
        Task<Rol> GetById(int id, string jwtToken);

        Task<List<Rol>> GetAllRollen(string jwtToken);
    }
}
