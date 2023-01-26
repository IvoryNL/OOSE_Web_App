using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface IOnderwijsuitvoeringService
    {
        Task<List<Onderwijsuitvoering>> GetAllOnderwijsuitvoeringen(string jwtToken);

        Task<Onderwijsuitvoering> GetOnderwijsuitvoeringById(int id, string jwtToken);
    }
}
