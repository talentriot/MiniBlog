using Microsoft.WindowsAzure.Storage;
namespace TP.AzureStorage
{
    public static class AzureStorageAccountFactory
    {
        public static CloudStorageAccount GetAccount(this string azureConnectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);
            return storageAccount;
        }
    }
}
