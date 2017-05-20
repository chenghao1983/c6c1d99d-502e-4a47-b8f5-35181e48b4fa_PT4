using System;
using System.IO;

namespace ActiveLearning.Business.Common
{
    public class FileUtil
    {
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
            var allowedExtension = System.Configuration.ConfigurationManager.AppSettings["AllowedExtension"];
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
    }
}