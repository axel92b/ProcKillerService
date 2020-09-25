using System;
using Microsoft.Win32;
using Topshelf;

namespace ProcKillerService
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = string.Empty;

            HostFactory.Run(x =>
            {
                x.AddCommandLineDefinition("name", p =>
                {
                    name = p;
                });
                x.ApplyCommandLine();
                x.AfterInstall(ihc =>
                {
                    //save process name in registry
                    using var system = Registry.LocalMachine.OpenSubKey("System");
                    using var currentControlSet = system.OpenSubKey("CurrentControlSet");
                    using var services = currentControlSet.OpenSubKey("Services");
                    using var service = services.OpenSubKey(ihc.ServiceName, true);
                    const string v = "ImagePath";
                    var imagePath = (string)service.GetValue(v);
                    service.SetValue(v, imagePath + $" -name:\"{name}\" ");
                });
                x.Service<ProcKillerService>(s =>
                {
                    s.ConstructUsing(v => new ProcKillerService(name));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.EnableServiceRecovery(r => r.RestartService(TimeSpan.FromSeconds(5)));
                x.SetServiceName("Process Killer Service");
                x.SetDescription($"Process Killer Service - Monitors and kills specified process. Configured to kill:\"{name}\".");
                x.StartAutomatically();
            });
        }
    }
}
