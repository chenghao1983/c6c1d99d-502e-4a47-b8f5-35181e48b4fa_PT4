using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using ActiveLearning.DB;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ActiveLearning.Business.Common
{
    public class Util
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

    }
}
