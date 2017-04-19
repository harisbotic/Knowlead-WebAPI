using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Knowlead.WebApi.Config;
using Knowlead.Common.HttpRequestItems;
using System.Linq;
using Knowlead.WebApi.Hubs;
using Hangfire;
using Knowlead.DAL;
using Microsoft.Extensions.Options;
using Knowlead.Common.Configurations.AppSettings;
using System.IO;

namespace Knowlead
{
    public class Startup
    {
        private readonly IConfigurationRoot _config;
        private readonly AppSettings _appSettings; 
        public const string _configPath = AppSettings.Path;

        public Startup(IHostingEnvironment env)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(env.ContentRootPath, @"../")))
                .AddJsonFile($"{_configPath}/appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"{_configPath}/appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            _appSettings = new AppSettings();
            _config.GetSection("AppSettings").Bind(_appSettings);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(_config.GetSection("AppSettings"));
            services.AddScoped<Auth>();

            services.AddCrossOrigin();
            services.AddRepositories();
            services.AddAutoMapper();
            services.AddCustomServices();
            services.AddCustomizedMvc();
            services.AddCustomPolicies();
            services.AddCustomSignalR();
            services.AddDbContext();
            services.AddHangfire(_appSettings);
            services.AddIdentityFramework();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IOptions<AppSettings> appOptions, IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var azureDataStore = new AzureDataStore(appOptions); 
            azureDataStore.Init(); //Initializes azure storages (Table and Blobs)

            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("AllowAll");
            app.UseIdentity();

            app.Use(async (context, next) =>
            {
                //if(context.Request.Path.Value.Contains("/ws"))
                if (string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]))
                {
                    if (context.Request.QueryString.HasValue)
                    {
                        var token = context.Request.QueryString.Value
                            .Split('&').SingleOrDefault(x => x.Contains("accessToken"))?.Split('=')[1];

                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            context.Request.Headers.Add("Authorization", new[] {$"Bearer {token}"});
                        }
                    }
                }
                await next.Invoke();
            });

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = _appSettings.BaseUrls.Auth,
                RequireHttpsMetadata = false,
                
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
            });

            app.UseWebSockets();
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<MainHub>("/mainHub");
            });

            //app.UseHangfireDashboard(); // Will be available under http://localhost:5000/hangfire
            app.UseHangfireServer();

            app.UseMvc();
        }
    }
}
