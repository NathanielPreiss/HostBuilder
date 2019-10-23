using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace HostBuilder
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var host = new Microsoft.Extensions.Hosting.HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddJsonFile("appsettings.json", false, true);

                    if (args != null)
                        configuration.AddCommandLine(args);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((hostContext, builder) =>
                {

                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<IHostedService>();
                })
                .Build();

            host.Run();
        }
    }
}