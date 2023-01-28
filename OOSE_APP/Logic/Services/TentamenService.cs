using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class TentamenService : ITentamenService
    {

        private readonly IHttpService _httpService;

        public TentamenService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task KoppelLeeruitkomstAanTentamen(int id, Tentamen tentamen, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Tentamen/KoppelLeeruitkomstAanTentamen/{id}";

            await _httpService.PutAsync(uri, tentamen, jwtToken);
        }

        public async Task InplannenTentamen(int id, Tentamen tentamen, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Tentamen/InplannenTentamen/{id}";

            await _httpService.PutAsync(uri, tentamen, jwtToken);
        }

        public async Task<List<Tentamen>> GetAllTentamens(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Tentamen/GetAll";

            return await _httpService.GetAsync<List<Tentamen>>(uri, jwtToken); 
        }

        public async Task<List<Tentamen>> GetAllTentamensZonderBeoordelingsmodel(string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Tentamen/GetAllTentamensZonderBeoordelingsmodel";

            return await _httpService.GetAsync<List<Tentamen>>(uri, jwtToken);
        }

        public async Task<List<Tentamen>> GetAllTentamensZonderBeoordelingsmodelVoorWijziging(int beoordelingsmodelId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Tentamen/GetAllTentamensZonderBeoordelingsmodelVoorWijziging/{beoordelingsmodelId}";

            return await _httpService.GetAsync<List<Tentamen>>(uri, jwtToken);
        }

        public async Task<List<Tentamen>> GetAllTentamensVanOnderwijsuitvoeringStudent(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Tentamen/GetAllTentamensVanOnderwijsuitvoeringStudent/{id}";

            return await _httpService.GetAsync<List<Tentamen>>(uri, jwtToken); 
        }

        public async Task<Tentamen> GetTentamenById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Tentamen/GetById/{id}";

            return await _httpService.GetAsync<Tentamen>(uri, jwtToken);
        }

        public async Task OntkoppelLeeruitkomstVanTentamen(int id, int leeruitkomstId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Tentamen/OntkoppelLeeruitkomstVanTentamen/{id}/{leeruitkomstId}";

            await _httpService.DeleteAsync(uri, jwtToken);
        }

        public async Task VerwijderPlanningVanTentamen(int id, int planningId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Tentamen/VerwijderPlanningVanTentamen/{id}/{planningId}";

            await _httpService.DeleteAsync(uri, jwtToken);
        }
    }
}
