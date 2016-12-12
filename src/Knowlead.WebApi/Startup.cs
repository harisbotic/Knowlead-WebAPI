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

namespace Knowlead
{
    public class Startup
    {
        private IConfigurationRoot _config;
        public static string _configPath = "./Config/Appsettings";
        public static IConfigurationRoot GetConfiguration(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile($"{_configPath}/appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            if (env != null)
            {
                builder
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile($"{_configPath}/appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            }

            return builder.Build();
        }

        public Startup(IHostingEnvironment env)
        {
            _config = GetConfiguration(env);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddScoped<Auth>();

            services.AddCrossOrigin();
            services.AddRepositories();
            services.AddAutoMapper();
            services.AddCustomServices();
            services.AddCustomizedMvc();
            services.AddCustomSignalR();
            services.AddDbContext();
            services.AddIdentityFramework();
            services.AddOpenIdDict();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("AllowAll");
            app.UseIdentity();
            //app.UseOAuthValidation();

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

            app.UseOpenIddict();
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                Audience = "http://knowlead.com:5000",
                Authority = "http://knowlead.com:5000"
            });

            app.UseWebSockets();
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<MainHub>("/mainHub");
            });

            app.UseMvc();
        }
    }
}
