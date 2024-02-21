using eSusInsurers.Models;

namespace eSusInsurers.Services.Interfaces
{
    public interface IInsuranceProviderService
    {
        Task<CreateInsuranceProviderResponse> CreateInsuranceProvider(CreateInsuranceProviderRequest request, CancellationToken cancellationToken);
    }
}
