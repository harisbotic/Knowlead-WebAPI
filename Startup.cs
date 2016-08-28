using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Knowlead.DomainModel;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Knowlead.DomainModel.UserModels;
using Knowlead.BLL;
using Knowlead.BLL.Interfaces;

namespace Knowlead
{
    public class Startup
    {
        private IConfigurationRoot _config;

        public static IConfigurationRoot getConfiguration(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            if (env != null) {
                builder
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            }
            return builder.Build();
        }

        public Startup(IHostingEnvironment env)
        {
            _config = getConfiguration(env);
        }

        // Register configuration related dependencies
        private void ConfigureConfig(IServiceCollection services) {
            services.AddSingleton(_config);
        }
        
        // Register Repository dependencies
        private void ConfigureRepositories(IServiceCollection services) {
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
        
        // Register Entity Framework DB context
        private void ConfigureDbContext(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>();
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
        }

        // Register the Identity dependencies.
        private void ConfigureIdentityFramework(IServiceCollection services) {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Password.RequireUppercase = false;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
            })
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
                // .AllowAuthorizationCodeFlow()
                .AllowPasswordFlow()
                .AllowRefreshTokenFlow()
                .UseJsonWebTokens()

                // During development, you can disable the HTTPS requirement.
                .DisableHttpsRequirement()

                // Register a new ephemeral key, that is discarded when the application
                // shuts down. Tokens signed using this key are automatically invalidated.
                // This method should only be used during development.
                .AddEphemeralSigningKey();
        }

        // Add framework dependencies.
        private void ConfigureMvc(IServiceCollection services) { 
            services.AddMvc()
                .AddJsonOptions(config => 
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            services.Configure<IdentityOptions>(options=>
            {
                options.Cookies.ApplicationCookie.LoginPath = null;
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureConfig(services);
            ConfigureRepositories(services);
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
            //app.UseOAuthValidation();
            app.UseIdentity();
            app.UseOpenIddict();
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                Audience = "http://localhost:5000",
                Authority = "http://localhost:5000/"
            });
            app.UseMvc();
            databaseInitializer.Seed();
        }
    }
}
