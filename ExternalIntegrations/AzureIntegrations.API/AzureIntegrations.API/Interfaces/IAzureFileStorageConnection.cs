using AzureIntegrations.API.Models;

namespace AzureIntegrations.API.Interfaces
{
    public interface IAzureFileStorageConnection
    {
        Task<string> SaveDocumentInAzure(AzureDocument document);
        Task<string> SaveInsuranceProvidersDocumentInAzure(AzureDocument document);
        Task<string?> UploadInsuranceProviderFiles(AzureDocument azureDocument);
    }
}
