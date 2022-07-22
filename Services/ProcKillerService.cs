using ProcKillerService.Providers;
using Serilog;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcKillerService.Services
{
    class ProcKillerService
    {
        private Task _monitorTask;
        private CancellationTokenSource _cts;
        private readonly string _processName;
        private readonly int _monitoringFrequency;

        public ProcKillerService(SettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Settings;
            if (!settings.AreValid())
            {
                throw new ArgumentException("Settings are not valid, can't proceed");
            }
            _processName = settings.ProcessName;
            _monitoringFrequency = settings.MonitoringFrequency;
        }

        public bool Start()
        {
            try
            {
                _cts = new CancellationTokenSource();
                _monitorTask = ObserverTask(_cts.Token);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Stop()
        {
            _cts.Cancel();

            try
            {
                _monitorTask.GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                //OK
            }

            _cts.Dispose();
            return true;
        }

        private async Task ObserverTask(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    Process.GetProcesses().Where(pr => pr.ProcessName.Equals(_processName, StringComparison.OrdinalIgnoreCase)).AsParallel()
                                .ForAll(x => x.Kill());
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Failed to close some or all instances of {_processName}");
                    throw;
                }
                await Task.Delay(_monitoringFrequency, token);
            }
            token.ThrowIfCancellationRequested();
        }
    }
}
