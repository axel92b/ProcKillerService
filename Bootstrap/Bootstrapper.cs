using Autofac;
using ProcKillerService.Managers;
using Serilog;
using System;
using System.IO;
using System.Linq;

namespace ProcKillerService.Bootstrap
{
    internal static class Bootstrapper
    {
        public static T InitApp<T>()
        {
            CreateLogger();
            SubscribeToEvents();

            var builder = new ContainerBuilder();
            builder.RegisterModule<Module>();
            var container = builder.Build();
            return container.Resolve<T>();
        }

        private static void CreateLogger()
        {
            FileManager.EnsureDirectoryExists(Constants.LogFolderPath);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(Constants.LogFolderPath, "log-.txt"), rollingInterval: RollingInterval.Month)
                .CreateLogger();
        }

        private static void SubscribeToEvents()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error($"Unhandled exception - {e.ExceptionObject.ToString()}");
        }
    }
}
