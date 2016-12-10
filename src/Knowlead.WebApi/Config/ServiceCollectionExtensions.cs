using System;
using Knowlead.BLL.Repositories;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Knowlead.Services;
using Knowlead.WebApi.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Knowlead.Services.Interfaces;
using Knowlead.BLL.Repositories.Interfaces;

namespace Knowlead.WebApi.Config
{
    public static class ServiceCollectionExtensions
    {
    
        public static IServiceCollection AddCustomizedMvc(this IServiceCollection services)
        {
                services.AddMvc(options =>
                {
                    options.Filters.Add(new HandleExceptionsAttribute());
                    
                })
                .AddJsonOptions(config => 
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
                
                services.Configure<IdentityOptions>(options=>
                {
                    options.Cookies.ApplicationCookie.LoginPath = null;
                });
    
            return services;
        }


        // Register the OpenIddict services, including the default Entity Framework stores.
        public static IServiceCollection AddOpenIdDict(this IServiceCollection services) 
        {
        services.AddOpenIddict<ApplicationDbContext, Guid>()
        
            // Register the ASP.NET Core MVC binder used by OpenIddict.
            // Note: if you don't call this method, you won't be able to
            // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
            .AddMvcBinders()

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

            return services;
        }


        public static IServiceCollection AddIdentityFramework(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Password.RequireUppercase = false;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireLowercase = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext, Guid>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();
        

            return services;
        }

        public static IServiceCollection AddCustomSignalR(this IServiceCollection services) 
        {
            services.AddSignalR();

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services) 
        {
            services.AddDbContext<ApplicationDbContext>();

            return services;
        }


        public static IServiceCollection AddMessageServices(this IServiceCollection services)
        {
            services.AddSingleton<MessageServices>();
            services.AddScoped<IBlobServices, BlobServices>();

            return services;
        }

        // Register Repository dependencies
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IP2PRepository, P2PRepository>();
            services.AddScoped<IBlobRepository, BlobRepository>();

            return services;
        }

        public static IServiceCollection AddCrossOrigin(this IServiceCollection services)
        {
            services.AddCors(options => 
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            return services;
        }
    }
}