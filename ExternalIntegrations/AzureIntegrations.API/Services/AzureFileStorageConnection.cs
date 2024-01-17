using AzureIntegrations.API.Interfaces;
using AzureIntegrations.API.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace AzureIntegrations.API.Services
{
    public class AzureFileStorageConnection : IAzureFileStorageConnection
    {
        private readonly StorageConfiguration _storageConfiguration;

        public AzureFileStorageConnection(StorageConfiguration storageConfiguration)
        {
            _storageConfiguration = storageConfiguration;
        }

        public async Task<CloudFileDirectory?> StorageConnection()
        {
            try
            {
                StorageCredentials storageCredentials = new StorageCredentials(_storageConfiguration.AccountName, _storageConfiguration.AccountKey);
                CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
                CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
                CloudFileShare share = fileClient.GetShareReference(_storageConfiguration.FileShare);
                await share.CreateIfNotExistsAsync();
                if (await share.ExistsAsync())
                {
                    return share.GetRootDirectoryReference();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return default(CloudFileDirectory?);
        }

        public async Task<string> SaveDocumentInAzure(AzureDocument document)
        {
            string documentPath = string.Empty;
            try
            {
                if (document != null)
                {
                    CloudFile file = await GetCloudFile(document);
                    documentPath = await SaveDocument(file, document.DocumentData);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return documentPath;
        }

        private async Task<CloudFile?> GetCloudFile(AzureDocument document)
        {
            //root
            CloudFileDirectory? rootDirectory = await StorageConnection();

            if (await rootDirectory?.ExistsAsync())
            {
                try
                {
                    //root/{mainDirectory}
                    CloudFileDirectory mainFolder = rootDirectory.GetDirectoryReference(document.MainDirectory);
                    await mainFolder.CreateIfNotExistsAsync();

                    //root/{mainDirectory}/{childDirectory}
                    CloudFileDirectory subFolder = mainFolder.GetDirectoryReference(document.ChildDirectory);
                    await subFolder.CreateIfNotExistsAsync();

                    if (!String.IsNullOrEmpty(document.ChildSubDirectory))
                    {
                        //root/{mainDirectory}/{childDirectory}/{childSubDirectory}/
                        CloudFileDirectory childSubFolder = subFolder.GetDirectoryReference(document.ChildSubDirectory);
                        await childSubFolder.CreateIfNotExistsAsync();
                        if (await childSubFolder.ExistsAsync())
                        {
                            return childSubFolder.GetFileReference(document.DocumentName);
                        }
                    }
                    if (await subFolder.ExistsAsync())
                    {
                        return subFolder.GetFileReference(document.DocumentName);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return default(CloudFile);
        }

        private async Task<string> SaveDocument(CloudFile file, byte[] documentData)
        {
            try
            {
                string documentPath = string.Empty;

                if (await file.ExistsAsync())
                {
                    using (MemoryStream ms = new MemoryStream(documentData))
                    {
                        await file.UploadFromStreamAsync(ms);
                    }
                    Uri filePath = file.Uri;
                    documentPath = filePath.AbsoluteUri;
                }
                else
                {
                    long length = documentData.LongLength;
                    await file.CreateAsync(length);
                    using (MemoryStream ms = new MemoryStream(documentData))
                    {
                        await file.UploadFromStreamAsync(ms);
                        Uri filePath = file.Uri;
                        documentPath = filePath.AbsoluteUri;
                    }
                }
                return documentPath;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<string> SaveInsuranceProvidersDocumentInAzure(AzureDocument document)
        {
            string documentPath = string.Empty;
            try
            {
                if (document != null)
                {
                    CloudFile? file = await InsuranceProvidersDocumentCloudFile(document);

                    if (file != null)
                    {
                        documentPath = await SaveDocument(file, document.DocumentData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return documentPath;
        }

        private async Task<CloudFile?> InsuranceProvidersDocumentCloudFile(AzureDocument document)
        {
            //root
            CloudFileDirectory rootDirectory = await StorageConnection();
            if (await rootDirectory.ExistsAsync())
            {
                try
                {
                    //root/{mainDirectory}
                    CloudFileDirectory mainFolder = rootDirectory.GetDirectoryReference(document.MainDirectory);
                    await mainFolder.CreateIfNotExistsAsync();

                    //root/{mainDirectory}/{childDirectory}
                    CloudFileDirectory subFolder = mainFolder.GetDirectoryReference(document.ChildDirectory);
                    await subFolder.CreateIfNotExistsAsync();

                    if (!String.IsNullOrEmpty(document.ChildSubDirectory))
                    {
                        //root/{mainDirectory}/{childDirectory}/{childSubDirectory}/
                        CloudFileDirectory childSubFolder = subFolder.GetDirectoryReference(document.ChildSubDirectory);
                        await childSubFolder.CreateIfNotExistsAsync();
                        if (await childSubFolder.ExistsAsync())
                        {
                            return childSubFolder.GetFileReference(document.DocumentName);
                        }
                    }
                    if (await subFolder.ExistsAsync())
                    {
                        return subFolder.GetFileReference(document.DocumentName);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return default(CloudFile);
        }

        public async Task<string?> UploadInsuranceProviderFiles(AzureDocument azureDocument)
        {
            try
            {

                var documentPath = await SaveInsuranceProvidersDocumentInAzure(azureDocument);

                if (documentPath != null)
                {
                    return documentPath + _storageConfiguration.ProxyKey;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private async Task DeleteInsuranceProviderFiles(List<InsuranceProviderFiles> insuranceProviderFiles)
        //{
        //    foreach (var file in insuranceProviderFiles)
        //    {
        //        var fileDetails = await _context.InsuranceProviderDocuments.Where(s => s.InsuranceProviderDocumentId == file.InsuranceProviderDocumentId).FirstOrDefaultAsync();
        //        fileDetails.IsActive = false;
        //    }
        //    await _context.SaveChangesAsync();
        //}
    }
}
