using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IToetsinschrijvingService
    {
        Task CreateToetsinschrijving(Toetsinschrijving toetsinschrijving, string jwtToken);
    }
}
