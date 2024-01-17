using eSusFarmInternal.API.Models;

namespace eSusFarmInternal.API.Interfaces
{
    public interface IInternaleSusFarmService
    {
        Task<List<CitiesDto>?> GetCitiesSummary(int provinceId
                                              , CancellationToken cancellationToken);

        Task<List<ProvincesDto>?> GetProvinceSummary(CancellationToken cancellationToken);
    }
}
