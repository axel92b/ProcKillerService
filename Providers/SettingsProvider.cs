using ProcKillerService.Managers;
using ProcKillerService.Model;
using Serilog;
using System;
using System.Linq;

namespace ProcKillerService.Providers
{
    internal class SettingsProvider
    {
        private SettingsModel _settings;

        public SettingsModel Settings
        {
            get
            {
                if (_settings == null)
                {
                    LoadSettings();
                }

                return _settings;
            }
        }

        private void LoadSettings()
        {
            try
            {
                if (!FileManager.IsFileExists(Constants.SettingsFilePath))
                {
                    FileManager.EnsureFileDirectoryExists(Constants.SettingsFilePath);
                    _settings = new SettingsModel();
                    FileManager.SaveFile(_settings, Constants.SettingsFilePath);
                    Log.Warning("No settings file were found, using defaults");
                    return;
                }

                _settings = FileManager.GetFile<SettingsModel>(Constants.SettingsFilePath);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed to load settings from: {Constants.SettingsFilePath}\nUsing defaults");
                _settings = new SettingsModel();
            }
        }
    }
}
