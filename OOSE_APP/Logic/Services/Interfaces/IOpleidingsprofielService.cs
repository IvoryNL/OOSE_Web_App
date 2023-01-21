using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IOpleidingsprofielService
    {
        Task<List<Opleidingsprofiel>> GetAllOpleidingsprofielenByOpleidingId(int opleidingId, string jwtToken);
    }
}
