using Logic.Constants;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class ConsistentieCheckService : IConsistentieCheckService
    {
        private readonly IHttpService _httpService;

        public ConsistentieCheckService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> ConsistentieCheckCoverage(int onderwijsmoduleId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/ConsistentieCheck/ConsistentieCheckCoverage/{onderwijsmoduleId}";

            return await _httpService.GetAsync<bool>(uri, jwtToken);
        }

        public async Task<bool> ConsistentieCheckTentamenPlanning(int onderwijsuitvoeringId, string jwtToken)
        {
            var uri = $"{ApiUrl.BASE_URL}/ConsistentieCheck/ConsistentieCheckTentamenPlanning/{onderwijsuitvoeringId}";

            return await _httpService.GetAsync<bool>(uri, jwtToken);
        }
    }
}
