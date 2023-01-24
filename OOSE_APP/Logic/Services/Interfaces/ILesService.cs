using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface ILesService
    {
        Task<List<Les>> GetAllLessen(string jwtToken);

        Task<Les> GetLesById(int id, string jwtToken);

        Task KoppelLeeruitkomstAanLes(int id, Les les, string jwtToken);

        Task OntkoppelLeeruitkomstVanLes(int id, int leeruitkomstId, string jwtToken);

        Task KoppelLesmateriaalAanLes(int id, Les les, string jwtToken);

        Task OntkoppelLesmateriaalVanLes(int id, int lesmateriaalId, string jwtToken);

        Task InplannenLes(int id, Les les, string jwtToken);

        Task VerwijderPlanningVanLes(int id, int planningId,  string jwtToken);
    }
}
