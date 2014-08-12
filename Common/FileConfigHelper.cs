using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class FileConfigHelper
    {
        private static readonly string configFolder = AppDomain.CurrentDomain.BaseDirectory;

        public static string GetConfig(string fileName)
        {
            string configPath = GetFilePath(fileName);
            if (!File.Exists(configPath))
                return null;
            else
                return File.ReadAllText(configPath);
        }

        public static void SaveConfig(string fileName, string content)
        {
            var configPath = GetFilePath(fileName);
            File.WriteAllText(configPath, content);
        }

        public static string GetFilePath(string fileName)
        {
            string configPath = string.Format(@"{0}\{1}.xml", configFolder, fileName);

            return configPath;
        }
    }
}
