using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Knowlead
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();
            
            X509Certificate2 certificate = new X509Certificate2("knowlead_co.pfx", "knowlead-hepek!");
            var host = new WebHostBuilder()
                .ConfigureLogging((context, factory) =>
                {
                    factory.UseConfiguration(context.Configuration.GetSection("Logging"));
                    factory.AddConsole();
                    factory.AddDebug();
                })
                .UseConfiguration(config)
                .UseKestrel(options => 
                {
                    options.Listen(IPAddress.Any, 8080, conf => conf.UseHttps(certificate));
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("https://*:8080")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
