using System;
using System.Linq;
using Broker.System.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Broker.System.Covers.Installers
{
    public static class InstallerExtension
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            var concreteInstallers = typeof(Startup).Assembly.ExportedTypes.Where(i =>
                    typeof(IInstaller).IsAssignableFrom(i) &&
                    !i.IsAbstract &&
                    !i.IsInterface)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            concreteInstallers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}