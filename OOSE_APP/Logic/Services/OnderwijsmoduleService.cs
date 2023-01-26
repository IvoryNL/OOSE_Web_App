using Logic.Constants;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class OnderwijsmoduleService : IOnderwijsmoduleService
    {
        private readonly IHttpService _httpService;

        public OnderwijsmoduleService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Onderwijsmodule>> GetAllOnderwijsmodulesViaOpleidingId(int opleidingId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsmodule/GetAllOnderwijsmodulesViaOpleidingId/{opleidingId}";

            return await _httpService.GetAsync<List<Onderwijsmodule>>(uri, jwtToken);
        }

        public async Task<Onderwijsmodule> GetOnderwijsmoduleById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsmodule/GetById/{id}";

            return await _httpService.GetAsync<Onderwijsmodule>(uri, jwtToken);
        }

        public async Task<Models.DocumentExportEnImport.Onderwijsmodule> GetOnderwijsmoduleVoorExportById(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsmodule/GetOnderwijsmoduleVoorExportById/{id}";

            return await _httpService.GetAsync<Models.DocumentExportEnImport.Onderwijsmodule>(uri, jwtToken);
        }

        public async Task CreateOnderwijsmodule(Onderwijsmodule onderwijsmodule, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsmodule/Create";

            await _httpService.PostAsync(uri, onderwijsmodule, jwtToken);
        }

        public async Task UpdateOnderwijsmodule(int id, Onderwijsmodule onderwijsmodule, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsmodule/Update/{id}";

            await _httpService.PutAsync(uri, onderwijsmodule, jwtToken);
        }

        public async Task DeleteOnderwijsmodule(int id, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsmodule/Upudate/{id}";

            await _httpService.DeleteAsync(uri, jwtToken);
        }

        public async Task VoegOnderwijseenheidToe(int id, Onderwijseenheid onderwijseenheid, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsmodule/VoegOnderwijseenheidToe/{id}";

            await _httpService.PutAsync(uri, onderwijseenheid, jwtToken);
        }

        public async Task ImporteerOnderwijsmodule(Models.DocumentExportEnImport.Onderwijsmodule onderwijsmodule, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsmodule/Create";

            await _httpService.PostAsync(uri, onderwijsmodule, jwtToken);
        }

        public async Task ImporteerOnderwijseenheid(int id, Models.DocumentExportEnImport.Onderwijseenheid onderwijseenheid, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/Onderwijsmodule/VoegOnderwijseenheidToe/{id}";

            await _httpService.PutAsync(uri, onderwijseenheid, jwtToken);
        }
    }
}
