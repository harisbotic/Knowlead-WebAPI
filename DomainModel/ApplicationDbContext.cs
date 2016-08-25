using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Knowlead.DomainModel.UserModels;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.LookupModels.Geo;
using Knowlead.DomainModel.CoreModels;
using Knowlead.DomainModel.FeedbackModels;
using Knowlead.DomainModel.LookupModels.FeedbackModels;
using OpenIddict;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Knowlead.DomainModel
{
    public class ApplicationDbContext : OpenIddictDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private IConfigurationRoot _config;
        public ApplicationDbContext(IConfigurationRoot config, DbContextOptions options) : base(options) 
        {
            _config = config;
        }
        //No need to put base classes in dbset
        #region Lookup Models
            public DbSet<Achievement> Achievements {get; set; }
            public DbSet<City> Cities {get; set; }
            public DbSet<Country> Countries {get; set; }
            public DbSet<FOS> Fos {get; set; }
            public DbSet<Language> Languages {get; set; }
            public DbSet<Status> Statuses {get; set;}
            
        #endregion 

        #region Feedback Models
            public DbSet<FeedbackClass> FeedbackClasses {get; set;}
            public DbSet<FeedbackCourse> FeedbackCourses {get; set;}
            public DbSet<FeedbackP2P> FeedbackP2P {get; set;}
            public DbSet<FeedbackQuestion> FeedbackQuestions {get; set;}    

        #endregion   

        #region User Models
            public DbSet<ApplicationUserInterest> ApplicationUserInterests { get; set; }
            public DbSet<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
            public DbSet<ApplicationUserSkill> ApplicationUserSkills { get; set; }
            public DbSet<UserAchievement> UserAchievements { get; set; }
            public DbSet<UserCertificate> UserCertificates { get; set; }
            public DbSet<UserNotebook> UserNotebooks { get; set; }

        #endregion      
        
        #region Core Models
            public DbSet<Image> Images {get; set; }

        #endregion 

        



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseNpgsql(_config["ConnectionStrings:DefaultContextConnection"]);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<_CoreLookup>()
                .HasDiscriminator<string>("Category")
                .HasValue<Achievement>("Achievement")
                .HasValue<FOS>("Fos")
                .HasValue<Language>("Language")
                .HasValue<Status>("Status");

            modelBuilder.Entity<_GeoLookup>()
                .HasDiscriminator<string>("Category")
                .HasValue<City>("City")
                .HasValue<Country>("Country");

            modelBuilder.Entity<_Feedback>()
                .HasDiscriminator<string>("Category")
                .HasValue<FeedbackClass>("Class")
                .HasValue<FeedbackCourse>("Course")
                .HasValue<FeedbackP2P>("P2P")
                .HasValue<FeedbackQuestion>("Question");

            modelBuilder.Entity<ApplicationUserLanguage>()
                .HasKey(t => new { t.ApplicationUserId, t.LanguageId });

            modelBuilder.Entity<ApplicationUserInterest>()
                .HasKey(t => new { t.ApplicationUserId, t.FosId });

            modelBuilder.Entity<ApplicationUserSkill>()
                .HasKey(t => new { t.ApplicationUserId, t.FosId });

        }
    }
}
