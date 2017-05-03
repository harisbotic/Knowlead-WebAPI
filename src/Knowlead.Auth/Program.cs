using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Knowlead.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            X509Certificate2 certificate = new X509Certificate2("knowlead_co.pfx", "knowlead-hepek!");
            
            var host = new WebHostBuilder()
                .UseKestrel(options => 
                {
                    options.UseHttps("knowlead_co.pfx", "knowlead-hepek!");
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseUrls("https://0.0.0.0:5005")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
