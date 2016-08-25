using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Knowlead.DomainModel;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Knowlead.DomainModel.UserModels;
using Knowlead.Migrations;

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

        // Service configuration related functions
        private void ConfigureConfig(IServiceCollection services) {
            services.AddSingleton(_config);
        }

        // Add Entity Framework DB context
        private void ConfigureDbContext(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>();
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        }

        // Register the Identity services.
        private void ConfigureIdentityFramework(IServiceCollection services) {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext, Guid>()
                .AddDefaultTokenProviders();
        }

        // Register the OpenIddict services, including the default Entity Framework stores.
        private void ConfigureOpenIdDict(IServiceCollection services) {
            services.AddOpenIddict<ApplicationUser, IdentityRole<Guid>, ApplicationDbContext, Guid>()
            
                // Enable the authorization, logout, token and userinfo endpoints.
                .EnableAuthorizationEndpoint("/connect/authorize")
                .EnableLogoutEndpoint("/connect/logout")
                .EnableTokenEndpoint("/connect/token")
                .EnableUserinfoEndpoint("/connect/userinfo")

                // Allow client applications to use the code flow.
                .AllowAuthorizationCodeFlow()

                // During development, you can disable the HTTPS requirement.
                .DisableHttpsRequirement()

                // Register a new ephemeral key, that is discarded when the application
                // shuts down. Tokens signed using this key are automatically invalidated.
                // This method should only be used during development.
                .AddEphemeralSigningKey();
        }

        // Add framework services.
        private void ConfigureMvc(IServiceCollection services) { 
            services.AddMvc()
                .AddJsonOptions(config => 
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureConfig(services);
            ConfigureDbContext(services);
            ConfigureIdentityFramework(services);
            ConfigureOpenIdDict(services);
            ConfigureMvc(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IDatabaseInitializer databaseInitializer)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseOAuthValidation();
            app.UseIdentity();
            app.UseOpenIddict();
            app.UseMvc();
            using (var context = new ApplicationDbContext(
                _config,
                app.ApplicationServices.GetRequiredService<DbContextOptions<ApplicationDbContext>>())) {
                context.Database.EnsureCreated();
            }
            databaseInitializer.Seed();
        }
    }
}
