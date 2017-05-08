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
using Knowlead.WebApi.Hubs;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using static Knowlead.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Knowlead.Common.Configurations.AppSettings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Knowlead.Common.Exceptions;
using Knowlead.DTO.ResponseModels;
using Newtonsoft.Json;

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

            return services;
        }

        public static IServiceCollection AddCustomPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.RegisteredUser,
                    policyBuilder => policyBuilder.RequireClaim(ClaimTypes.GivenName));
            });
                
            return services;
        }

        //RELATED TO IDENTITY        
        public class ConfirmEmailDataProtectorTokenProvider<TUser>: DataProtectorTokenProvider<TUser> where TUser:class
        {
            public ConfirmEmailDataProtectorTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<ConfirmEmailDataProtectionTokenProviderOptions> options) : base(dataProtectionProvider, options)
            {
            }
        }
        public class ConfirmEmailDataProtectionTokenProviderOptions : DataProtectionTokenProviderOptions { }
        private const string EmailConfirmationTokenProviderName = "ConfirmEmail";
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
                config.Lockout.MaxFailedAccessAttempts = 20;
                config.Lockout.AllowedForNewUsers = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<ConfirmEmailDataProtectorTokenProvider<ApplicationUser>>(EmailConfirmationTokenProviderName)
                .AddErrorDescriber<CustomIdentityErrorDescriber>();

                // services.Configure<IdentityOptions>(options =>
                // {
                //     options.Tokens.EmailConfirmationTokenProvider = EmailConfirmationTokenProviderName;
                // });

                // services.Configure<ConfirmEmailDataProtectionTokenProviderOptions>(options =>
                // {
                //     options.TokenLifespan = TimeSpan.FromDays(60);
                // });

                services.AddAuthenticationCore(options =>
                {
                    options.DefaultAuthenticateScheme = "jwt";
                    options.DefaultChallengeScheme = "jwt";
                    options.DefaultSignInScheme = "jwt";
                });

                services.AddJwtBearerAuthentication("jwt", o =>
                {
                    o.Authority = "https://knowlead.co:5005/";
                    o.Audience  = "https://knowlead.co:5005/resources";
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.HandleResponse();
                            c.Response.StatusCode = 401;
                            c.Response.ContentType = "application/json";

                            return c.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseModel(new ErrorModel(ErrorCodes.AuthorityError)), new JsonSerializerSettings 
                            { 
                                ContractResolver = new CamelCasePropertyNamesContractResolver() 
                            }));
                        }
                    };
                });

            return services;
        }
        public static IServiceCollection AddCustomSignalR(this IServiceCollection services) 
        {
            services.AddSignalR();
            services.AddSingleton(typeof(HubLifetimeManager<>), typeof(KLHubLifetimeManager<>));

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services) 
        {
            services.AddDbContext<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddHangfire(this IServiceCollection services, AppSettings appSettings) 
        {
              services.AddHangfire(options =>
                    options.UseSqlServerStorage(appSettings.ConnectionStrings.KnowleadSQL));

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<MessageServices>();
            services.AddScoped<IBlobServices, BlobServices>();
            services.AddScoped<IChatServices, ChatServices>();
            services.AddScoped<IRewardServices, RewardServices>();
            services.AddScoped<ITransactionServices, TransactionServices>();
            services.AddScoped<INotebookServices, NotebookServices>();
            services.AddScoped<IStickyNoteServices, StickyNoteServices>();
            services.AddScoped<IFeedbackServices, FeedbackServices>();
            services.AddTransient<ICallServices, CallServices<MainHub>>();
            services.AddTransient<INotificationServices, NotificationServices<MainHub>>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IP2PRepository, P2PRepository>();
            services.AddScoped<IBlobRepository, BlobRepository>();
            services.AddScoped<IFriendshipRepository, FriendshipRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IStickyNoteRepository, StickyNoteRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddScoped<ICallRepository, CallRepository>();
            services.AddScoped<IRewardRepository, RewardRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<INotebookRepository, NotebookRepository>();
            services.AddScoped<UnitOfWork>();

            return services;
        }
    }
}