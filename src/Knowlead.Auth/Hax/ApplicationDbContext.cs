using Microsoft.EntityFrameworkCore;
using Knowlead.Auth.Hax;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Knowlead.Common.Configurations.AppSettings;

namespace Knowlead.Auth.Hax
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly AppSettings _appSettings;
        public ApplicationDbContext(IOptions<AppSettings> appSettings, DbContextOptions options) : base(options)
        {
             _appSettings = appSettings.Value;
        }

        #region User Models
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_appSettings.ConnectionStrings.KnowleadSQL,
                                            b => b.MigrationsAssembly("Knowlead.DAL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}
