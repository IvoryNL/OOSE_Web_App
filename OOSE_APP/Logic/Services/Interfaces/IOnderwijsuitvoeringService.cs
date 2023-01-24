using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IOnderwijsuitvoeringService
    {
        Task<List<Onderwijsuitvoering>> GetAllOnderwijsuitvoeringen(string jwtToken);
    }
}
