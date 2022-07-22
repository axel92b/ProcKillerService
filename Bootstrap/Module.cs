using Autofac;
using ProcKillerService.Providers;
using System;
using System.Linq;

namespace ProcKillerService.Bootstrap
{
    internal class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Services.ProcKillerService>().SingleInstance();
            builder.RegisterType<SettingsProvider>().SingleInstance();
        }
    }
}
