using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Knowlead.DomainModel.UserModels;
using Knowlead.DAL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Knowlead.Auth.IdentityServer;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using Knowlead.Common.Configurations.AppSettings;
using System.IO;

namespace Knowlead.Auth
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
            
            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_appSettings.ConnectionStrings.KnowleadSQL));

			services.AddIdentityServer()
					.AddTemporarySigningCredential()
                    // .AddConfigurationStore(options => options.UseSqlServer("Server=.;Database=Knowlead;Trusted_Connection=True;MultipleActiveResultSets=true"))
                    // .AddOperationalStore(options => options.UseSqlServer("Server=.;Database=Knowlead;Trusted_Connection=True;MultipleActiveResultSets=true"))
                    .AddInMemoryPersistedGrants()
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients())
                    .AddAspNetIdentity<ApplicationUser>();
                    
            services.AddScoped<IProfileService, KnowleadProfileService>();
            services.AddScoped<IResourceOwnerPasswordValidator, KnowleadResourceOwnerPasswordValidator>();

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

			app.UseIdentity();
			app.UseIdentityServer();
        }
    }
}
