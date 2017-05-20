using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ActiveLearning.Business.ViewModel;
using ActiveLearning.Business.Implementation;

namespace ActiveLearning.Business.Common
{
    public class Util : BaseManager
    {
        #region Copy values

        public static void CopyNonNullProperty(object objFrom, object objTo)
        {
            List<Type> typeList = new List<Type>() { typeof(byte), typeof(byte?), typeof(sbyte),  typeof(sbyte?), typeof(int), typeof(int?), typeof(uint), typeof(uint?),
                typeof(short), typeof(short?), typeof(ushort), typeof(ushort?), typeof(long), typeof(long?), typeof(ulong), typeof(ulong?), typeof(float), typeof(float?),
                typeof(double), typeof(double?), typeof(char), typeof(char?), typeof(bool), typeof(bool?), typeof(string), typeof(decimal), typeof(decimal?),
                typeof(DateTime), typeof(DateTime?), typeof(Enum), typeof(Guid), typeof(Guid?), typeof(IntPtr), typeof(IntPtr?), typeof(TimeSpan), typeof(TimeSpan?),
                typeof(UIntPtr), typeof(UIntPtr?)  };
            if (objFrom == null || objTo == null) return;
            PropertyInfo[] allProps = objFrom.GetType().GetProperties();
            PropertyInfo toProp;
            foreach (PropertyInfo fromProp in allProps)
            {
                if (typeList.Contains(fromProp.PropertyType))
                {
                    toProp = objTo.GetType().GetProperty(fromProp.Name);
                    if (toProp == null) continue; //not here
                    if (!toProp.CanWrite) continue; //only if writeable
                    if (fromProp.GetValue(objFrom, null) == null) continue;
                    toProp.SetValue(objTo, fromProp.GetValue(objFrom, null), null);
                }
            }


        }
        #endregion

        #region Hash Password

        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        //public const int ITERATION_INDEX = 0;
        //public const int SALT_INDEX = 1;
        //public const int PBKDF2_INDEX = 2;

        public static string GenerateSalt()
        {
            // Generate a random salt
            byte[] salt = new byte[SALT_BYTE_SIZE];
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            csprng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string CreateHash(string passwordStr, string saltStr)
        {
            // Hash the password and encode the parameters
            byte[] salt = Convert.FromBase64String(saltStr);
            byte[] hash = PBKDF2(passwordStr, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return Convert.ToBase64String(hash);
        }

        public static bool ValidatePassword(string password, string correctHash, string correctSalt)
        {
            // Extract the parameters from the hash
            //char[] delimiter = { ':' };
            //string[] split = correctHash.Split(delimiter);
            //int iterations = Int32.Parse(split[ITERATION_INDEX]);
            //byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
            byte[] salt = Convert.FromBase64String(correctSalt);
            //byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);
            byte[] hash = Convert.FromBase64String(correctHash);

            byte[] testHash = PBKDF2(password, salt, PBKDF2_ITERATIONS, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
        #endregion

        #region Password Complexity
        public static bool IsPasswordComplex(string plainPassword)
        {
            string regexPattern = @"^(?=(.*\d){2})(?=.*[a-z]{2})(?=.*[A-Z]{2})(?=.*[^a-zA-Z\d]{2}).{8,}$";

            return Regex.IsMatch(plainPassword, regexPattern);
        }
        #endregion

        #region Upload

        public static string GetAllowedFileExtensionFromConfig()
        {
            string defaultExtensions = ".mp4,.ppt,.pptx,.txt,.doc,.docx,.pdf,.xls,.xlsx";
            string key = "AllowedFileExtensions";
            string[] settings = System.Web.Configuration.WebConfigurationManager.AppSettings.GetValues(key);
            return settings == null || settings.Length == 0 ? defaultExtensions : "." + settings[0].Replace(",", ",.");
        }
        public static string GetVideoFormatsFromConfig()
        {
            string defaultVideoFormats = "mp4";
            string key = "VideoFormats";
            string[] settings = System.Web.Configuration.WebConfigurationManager.AppSettings.GetValues(key);
            return settings == null || settings.Length == 0 ? defaultVideoFormats : settings[0];
        }
        public static int GetAllowedFileSizeFromConfig()
        {
            int defaultAllowedFileSize = 4;
            string key = "AllowedFileSize";
            string[] settings = System.Web.Configuration.WebConfigurationManager.AppSettings.GetValues(key);
            if (settings == null || settings.Length == 0)
            {
                return defaultAllowedFileSize;
            }
            else
            {
                int allowedFileSize = 0;
                if (int.TryParse(settings[0], out allowedFileSize))
                {
                    return allowedFileSize;
                }
                else
                {
                    return defaultAllowedFileSize;
                }
            }
        }
        public static string GetUploadFolderFromConfig()
        {
            string defaultFolder = "/Upload/";
            string key = "UploadPath";
            string[] settings = System.Web.Configuration.WebConfigurationManager.AppSettings.GetValues(key);
            return settings == null || settings.Length == 0 ? defaultFolder : settings[0];
        }

        public static bool IsFileExtensionAllowed(string fileName, out string message)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                message = "File name is empty";
                return false;
            }
            // define default allowed extension
            var defaultAllowedExtension = "jpg,png,bmp,gif";
            // retrieve allowed extension from app config
            var allowedExtension = GetAllowedFileExtensionFromConfig();
            if (!String.IsNullOrEmpty(allowedExtension))
            {
                allowedExtension = defaultAllowedExtension;
            }
            var extension = GetFileExtension(fileName);

            if (!allowedExtension.Contains(extension))
            {
                message = extension + " is not allowed";
                return false;
            }

            message = string.Empty;
            return true;
        }

        public static string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName).Substring(1);
        }
        #endregion

        #region Chat


        public static int GetChatHistoryCount()
        {
            int defaultChatHistoryCount = 100;
            string key = "ChatHistoryCount";
            string[] settings = System.Web.Configuration.WebConfigurationManager.AppSettings.GetValues(key);
            if (settings == null || settings.Length == 0)
            {
                return defaultChatHistoryCount;
            }
            else
            {
                int chatHistoryCount = 0;
                if (int.TryParse(settings[0], out chatHistoryCount))
                {
                    return chatHistoryCount;
                }
                else
                {
                    return defaultChatHistoryCount;
                }
            }
        }
        #endregion


        #region Azure Storage

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
                ExceptionLog(sx);
               // LogUtil.WriteLog(sx.StackTrace);
                throw new Exception(sx.Message);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                //LogUtil.WriteLog(ex.StackTrace);
                throw ex;
            }

            return uploadResult;
        }

        public async static Task<CloudStorageAccount> OpenCloudStorageConnection()
        {
            var storagetConnectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["StorageConnectionString"]; //ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            if (string.IsNullOrWhiteSpace(storagetConnectionString))
            {
                ExceptionLog("Storage connection string is missing in web config");
                //LogUtil.WriteLog("Storage connection string is missing in web config");
                throw new Exception("Storage connection string is missing in web config");
            }

            CloudStorageAccount storageAccount;

            try
            {
                storageAccount = CloudStorageAccount.Parse(storagetConnectionString);
            }
            catch (FormatException fe)
            {
                ExceptionLog(fe);
                //LogUtil.WriteLog(fe.StackTrace);
                throw new Exception(fe.Message);
            }
            catch (StorageException se)
            {
                ExceptionLog(se);
                //LogUtil.WriteLog(se.StackTrace);
                throw new Exception(se.Message);
            }
            catch (ArgumentException ae)
            {
                ExceptionLog(ae);
                //LogUtil.WriteLog(ae.StackTrace);
                throw new Exception(ae.Message);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                //LogUtil.WriteLog(ex.StackTrace);
                throw ex;
            }

            if (storageAccount == null)
            {
                ExceptionLog("Storaget account is not available");
                //LogUtil.WriteLog("Storaget account is not available");
                throw new Exception("Storaget account is not available");
            }

            return storageAccount;
        }
        public async static Task<CloudBlobContainer> OpenCloudStorageContainer(CloudStorageAccount storageAccount)
        {
            if (storageAccount == null)
            {
                ExceptionLog("Storaget account is invalid");
                //LogUtil.WriteLog("Storaget account is invalid");
                throw new Exception("Storaget account is invalid");
            }

            var storageContainer = System.Web.Configuration.WebConfigurationManager.AppSettings["StorageContainerName"]; //ConfigurationManager.AppSettings["StorageContainerName"];
            if (string.IsNullOrWhiteSpace(storageContainer))
            {
                ExceptionLog("Storage container name is missing in web config");
                //LogUtil.WriteLog("Storage container name is missing in web config");
                throw new Exception("Storage container name is missing in web config");
            }

            CloudBlobContainer container;

            try
            {
                container = storageAccount.CreateCloudBlobClient().GetContainerReference(storageContainer);
            }
            catch (StorageException sx)
            {
                ExceptionLog(sx);
                //LogUtil.WriteLog(sx.StackTrace);
                throw new Exception(sx.Message);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                //LogUtil.WriteLog(ex.StackTrace);
                throw ex;
            }

            if (container == null)
            {
                ExceptionLog("Storaget container is not available");
                //LogUtil.WriteLog("Storaget container is not available");
                throw new Exception("Storaget container is not available");
            }

            return container;
        }


        #endregion

        #region Email



        #endregion

    }
}
