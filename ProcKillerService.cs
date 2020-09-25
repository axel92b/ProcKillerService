using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcKillerService
{
    class ProcKillerService
    {
        private Task _monitorTask;
        private CancellationTokenSource _cts;
        private readonly string _processName;

        public ProcKillerService(string processName)
        {
            _processName = processName;
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
                Process.GetProcesses().Where(pr => pr.ProcessName.Equals(_processName, StringComparison.OrdinalIgnoreCase)).AsParallel()
                    .ForAll(x => x.Kill());
                await Task.Delay(1000, token);
            }
            token.ThrowIfCancellationRequested();
        }
    }
}
