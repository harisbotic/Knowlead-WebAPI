using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Knowlead.DomainModel;
using Newtonsoft.Json.Serialization;

namespace Knowlead
{
    public class Startup
    {
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            // Add Entity Framework DB context
            services.AddDbContext<ApplicationDbContext>();

            //services.AddScoped<IFlinkRepository, FlinkRepository>();

            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(config => 
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}