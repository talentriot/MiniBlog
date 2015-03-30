using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPL.AzureStorage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

/// <summary>
/// Summary description for AzureStorageService
/// </summary>
public class AzureStorageService
{
    private static CloudStorageAccount _azureAccount;

    private static volatile AzureStorageService _storage;
    private static object _lockObject = new Object();

    private static BlobPostStorage _blobPostStorage;

    public BlobPostStorage BlobPostStorage
    {
        get { return _blobPostStorage; }
    }

    private AzureStorageService() { }

       public static void SetupAzureStorage(string azureConnectionString)
       {
           if (_storage == null)
           {
               lock (_lockObject)
               {
                   if (_storage == null)
                   {
                       _storage = new AzureStorageService();
                       _storage.InitializeStorage(azureConnectionString);
                   }
               }
           }
       }

       public static AzureStorageService Instance()
       {
           if (_storage == null)
           {
               throw new Exception("Need to call SetupAzureStorage upon application startup");
           }
           return _storage;
       }

       public CloudStorageAccount AzureAccount
       {
           get { return _azureAccount; }
       }

       private void InitializeStorage(string azureConnectionString)
       {
           _azureAccount = azureConnectionString.GetAccount();

           _blobPostStorage = new BlobPostStorage(_azureAccount);
           _blobPostStorage.CreateStorageContainerIfDoesNotExist();
           _blobPostStorage.SetAccessAs(BlobContainerPublicAccessType.Blob);
       }
   }
