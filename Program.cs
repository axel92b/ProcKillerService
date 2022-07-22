using System;
using Topshelf;
using ProcKillerService.Bootstrap;
using Serilog;

namespace ProcKillerService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<Services.ProcKillerService>(s =>
                {
                    s.ConstructUsing(v => Bootstrapper.InitApp<Services.ProcKillerService>());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.EnableServiceRecovery(r => r.RestartService(TimeSpan.FromSeconds(5)));
                x.SetServiceName(Constants.AppName);
                x.SetDescription(Constants.AppDescription);
                x.StartAutomatically();
                x.OnException(e => Log.Error(e, "Failed to initialize the app"));
            });
        }
    }
}
