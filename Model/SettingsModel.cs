using System;
using System.Linq;

namespace ProcKillerService.Model
{
    internal class SettingsModel
    {
        public string ProcessName { get; set; }
        public int MonitoringFrequency { get; set; } = 1000;

        public bool AreValid()
        {
            return !string.IsNullOrEmpty(ProcessName) && MonitoringFrequency > 0;
        }
    }
}
