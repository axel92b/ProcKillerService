using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace ProcKillerService.Managers
{
    internal static class FileManager
    {
        public static T GetFile<T>(string path)
        {
            var content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static void SaveFile(object file, string filePath)
        {
            var content = JsonConvert.SerializeObject(file, Formatting.Indented);
            File.WriteAllText(filePath, content);
        }

        public static bool IsFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static void EnsureFileDirectoryExists(string path)
        {
            var dirPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }

        public static void EnsureDirectoryExists(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
    }
}
