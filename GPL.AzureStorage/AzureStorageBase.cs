using Microsoft.WindowsAzure.Storage;
namespace GPL.AzureStorage
{
    public abstract class AzureStorageBase
    {
        protected CloudStorageAccount CloudStorageAccount { get; set; }

        public abstract string StorageContainerName { get; }

        protected AzureStorageBase(CloudStorageAccount cloudStorageAccount)
        {
            CloudStorageAccount = cloudStorageAccount;
        }

        public abstract void CreateStorageContainerIfDoesNotExist();
        public abstract void DeleteStorageContainer();
    }
}
