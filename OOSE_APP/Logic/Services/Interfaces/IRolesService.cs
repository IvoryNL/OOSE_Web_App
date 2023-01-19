using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IRolesService
    {
        Task<Rol> GetById(int id, string jwtToken);

        Task<List<Rol>> GetAll(string jwtToken);
    }
}
