namespace Logic.Services.Interfaces
{
    public interface IConsistentieCheckService
    {
        Task<bool> ConsistentieCheckCoverage(int onderwijsmoduleId, string jwtToken);

        Task<bool> ConsistentieCheckTentamenPlanning(int onderwijsuitvoeringId, string jwtToken);
    }
}
