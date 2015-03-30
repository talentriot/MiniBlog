using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GPL.AzureStorage;
using Microsoft.WindowsAzure.Storage;

/// <summary>
/// Saves blog items to blob system storage
/// </summary>
public class BlobPostStorage : BlobStorageBase
{
    public BlobPostStorage(CloudStorageAccount cloudStorageAccount) : base(cloudStorageAccount)
    {

    }

    public override string StorageContainerName
    {
        get { return "blogpost"; }
    }


}