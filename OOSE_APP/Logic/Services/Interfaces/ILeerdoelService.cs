using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface ILeerdoelService
    {
        Task<Leerdoel> GetLeerdoelById(int id, string jwtToken);

        Task CreateLeerdoel(Leerdoel leerdoel, string jwtToken);

        Task UpdateLeerdoel(int id, Leerdoel leerdoel, string jwtToken);
    }
}
