using System;
using System.IO;
using System.Linq;

namespace ProcKillerService
{
    internal static class Constants
    {
        public const string AppName = "ProcessKillerService";
        public const string AppDescription = "ProcessKillerService - Monitors and kills the specified process.";
        public const string SettingsFileName = "settings.json";
        public static readonly string AppWorkingFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Axel", AppName);
        public static readonly string SettingsFilePath = Path.Combine(AppWorkingFolderPath, SettingsFileName);
        public static readonly string LogFolderPath = Path.Combine(AppWorkingFolderPath, "Logs");
    }
}
