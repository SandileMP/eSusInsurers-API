using eSusInsurers.Models;

namespace eSusInsurers.Services.Interfaces
{
    public interface IInsuranceProviderService
    {
        Task<CreateInsuranceProviderResponse> CreateInuranceProvider(CreateInsuranceProviderRequest request, CancellationToken cancellationToken);
    }
}
