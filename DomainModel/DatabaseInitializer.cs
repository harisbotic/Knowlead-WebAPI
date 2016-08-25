using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using OpenIddict;

namespace Knowlead.DomainModel {
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext context;

        public DatabaseInitializer(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Seed()
        {
            await context.Database.EnsureCreatedAsync();
            //await context.Database.MigrateAsync();
            if (!context.Applications.Any()) {
                context.Applications.Add(new OpenIddictApplication<Guid> {
                    ClientId = "backend",
                    ClientSecret = Crypto.HashPassword("hepek_ovo_ono123.!faefawefceawcddevhybope;"),
                    DisplayName = "Knowlead backend",
                    LogoutRedirectUri = "http://localhost:5000/",
                    RedirectUri = "http://localhost:5000/signin-oidc",
                    Type = OpenIddictConstants.ClientTypes.Confidential
                });
                context.Applications.Add(new OpenIddictApplication<Guid> {
                    ClientId = "postman",
                    ClientSecret = "super_secret_postman",
                    DisplayName = "Postman",
                    RedirectUri = "https://www.getpostman.com/oauth2/callback",
                    Type = OpenIddictConstants.ClientTypes.Public
                });
                context.SaveChanges();
            }
        }
    }
    public interface IDatabaseInitializer
    {
        Task Seed();
    }
}