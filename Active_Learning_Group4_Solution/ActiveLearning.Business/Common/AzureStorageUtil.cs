using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;

namespace ActiveLearning.Business.Common
{
    public class AzureStorageUtil
    {
        public async static Task<UploadResult> UploadToAzureStorage(Stream stream, string newFileName)
        {
            var uploadResult = new UploadResult();
            uploadResult.success = true;
            uploadResult.message = string.Empty;

            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = await OpenCloudStorageConnection();
            }
            catch (Exception ex)
            {
                uploadResult.success = false;
                uploadResult.message = ex.Message;
                return uploadResult;
            }

            CloudBlobContainer container;
            try
            {
                container = await OpenCloudStorageContainer(storageAccount);
            }
            catch (Exception ex)
            {
                uploadResult.success = false;
                uploadResult.message = ex.Message;
                return uploadResult;
            }

            try
            {
                CloudBlockBlob blob = container.GetBlockBlobReference(newFileName);
                blob.Properties.ContentType = "image/jpeg";
                await blob.UploadFromStreamAsync(stream);
            }
            catch (StorageException sx)
            {
                LogUtil.WriteLog(sx.StackTrace);
                throw new Exception(sx.Message);
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex.StackTrace);
                throw ex;
            }

            return uploadResult;
        }

        public async static Task<CloudStorageAccount> OpenCloudStorageConnection()
        {
            var storagetConnectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            if (string.IsNullOrWhiteSpace(storagetConnectionString))
            {
                LogUtil.WriteLog("Storage connection string is missing in web config");
                throw new Exception("Storage connection string is missing in web config");
            }

            CloudStorageAccount storageAccount;

            try
            {
                storageAccount = CloudStorageAccount.Parse(storagetConnectionString);
            }
            catch (FormatException fe)
            {
                LogUtil.WriteLog(fe.StackTrace);
                throw new Exception(fe.Message);
            }
            catch (StorageException se)
            {
                LogUtil.WriteLog(se.StackTrace);
                throw new Exception(se.Message);
            }
            catch (ArgumentException ae)
            {
                LogUtil.WriteLog(ae.StackTrace);
                throw new Exception(ae.Message);
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex.StackTrace);
                throw ex;
            }

            if (storageAccount == null)
            {
                LogUtil.WriteLog("Storaget account is not available");
                throw new Exception("Storaget account is not available");
            }

            return storageAccount;
        }
        public async static Task<CloudBlobContainer> OpenCloudStorageContainer(CloudStorageAccount storageAccount)
        {
            if (storageAccount == null)
            {
                LogUtil.WriteLog("Storaget account is invalid");
                throw new Exception("Storaget account is invalid");
            }

            var storageContainer = ConfigurationManager.AppSettings["StorageContainerName"];
            if (string.IsNullOrWhiteSpace(storageContainer))
            {
                LogUtil.WriteLog("Storage container name is missing in web config");
                throw new Exception("Storage container name is missing in web config");
            }

            CloudBlobContainer container;

            try
            {
                container = storageAccount.CreateCloudBlobClient().GetContainerReference(storageContainer);
            }
            catch (StorageException sx)
            {
                LogUtil.WriteLog(sx.StackTrace);
                throw new Exception(sx.Message);
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex.StackTrace);
                throw ex;
            }

            if (container == null)
            {
                LogUtil.WriteLog("Storaget container is not available");
                throw new Exception("Storaget container is not available");
            }

            return container;
        }
    }

    public class UploadResult
    {
        public bool success;
        public string message;
    }
}