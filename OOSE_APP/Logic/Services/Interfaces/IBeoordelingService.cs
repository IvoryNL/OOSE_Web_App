using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IBeoordelingService
    {
        Task CreateBeoordeling(Beoordeling beoordeling, string jwtToken);
    }
}
