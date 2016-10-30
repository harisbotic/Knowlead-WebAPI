using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Knowlead.DomainModel.UserModels;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.LookupModels.Geo;
using Knowlead.DomainModel.CoreModels;
using Knowlead.DomainModel.FeedbackModels;
using Knowlead.DomainModel.LookupModels.FeedbackModels;
using OpenIddict;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Knowlead.DomainModel.FriendshipModels;
using Knowlead.DomainModel.P2PModels;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Knowlead.DAL
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
            public DbSet<State> States {get; set; }
            public DbSet<Country> Countries {get; set; }
            public DbSet<FOS> Fos {get; set; }
            public DbSet<Language> Languages {get; set; }
            
        #endregion 

        #region Feedback Models
            public DbSet<FeedbackClass> FeedbackClasses {get; set;}
            public DbSet<FeedbackCourse> FeedbackCourses {get; set;}
            public DbSet<FeedbackP2P> FeedbackP2P {get; set;}
            public DbSet<FeedbackQuestion> FeedbackQuestions {get; set;}    

        #endregion   

        #region User Models
            public DbSet<ApplicationUser> ApplicationUsers { get; set; }
            public DbSet<ApplicationUserInterest> ApplicationUserInterests { get; set; }
            public DbSet<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
            public DbSet<UserAchievement> UserAchievements { get; set; }
            public DbSet<UserCertificate> UserCertificates { get; set; }
            public DbSet<UserNotebook> UserNotebooks { get; set; }

        #endregion      
        
        #region Friendship
            public DbSet<Friendship> Friendships { get; set; }
            public DbSet<FriendshipRequest> FriendshipRequests { get; set; }
            
        #endregion 

        #region Peer to Peer
            public DbSet<P2P> P2p { get; set; }
            public DbSet<P2PDiscussion> P2PDiscussions { get; set; }
            public DbSet<P2PFile> P2PFiles { get; set; }
            public DbSet<P2PImage> P2PImages { get; set; }
            public DbSet<P2PLanguage> P2PLanguages { get; set; }
        #endregion 

        #region Core Models
            public DbSet<Image> Images {get; set; }
            public DbSet<UploadedFile> UploadedFiles { get; set; }

        #endregion 


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseNpgsql(_config["ConnectionStrings:DefaultContextConnection"],
                                            b => b.MigrationsAssembly("Knowlead.WebApi"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Country>().ToTable("Countries");  
            modelBuilder.Entity<State>().ToTable("States");

            modelBuilder.Entity<Achievement>().ToTable("Achievements");  
            modelBuilder.Entity<FOS>().ToTable("FOS");
            modelBuilder.Entity<Language>().ToTable("Languages");

            // modelBuilder.Entity<_CoreLookup>()
            //     .HasDiscriminator<string>("Category")
            //     .HasValue<Achievement>("Achievement")
            //     .HasValue<FOS>("Fos")
            //     .HasValue<Language>("Language");

            // modelBuilder.Entity<_GeoLookup>()
            //     .HasDiscriminator<string>("Category")
            //     .HasValue<State>("State")
            //     .HasValue<Country>("Country");

            modelBuilder.Entity<_Feedback>()
                .HasDiscriminator<string>("Category")
                .HasValue<FeedbackClass>("Class")
                .HasValue<FeedbackCourse>("Course")
                .HasValue<FeedbackP2P>("P2P")
                .HasValue<FeedbackQuestion>("Question");
            
            //** Application User ***
            modelBuilder.Entity<ApplicationUserLanguage>()
                .HasKey(t => new { t.ApplicationUserId, t.LanguageId });

            modelBuilder.Entity<ApplicationUserInterest>()
                .HasKey(t => new { t.ApplicationUserId, t.FosId });

            //*** Friendship ***
            modelBuilder.Entity<Friendship>()
                .HasKey(t => new { t.UserSentId, t.UserAcceptedId });

            modelBuilder.Entity<FriendshipRequest>()
                .HasKey(t => new { t.UserSentId, t.UserReceivedId });
            
            //*** P2P ***
            modelBuilder.Entity<P2PFile>()
                .HasKey(t => new { t.P2pId, t.UploadedFileId });
           
            modelBuilder.Entity<P2PImage>()
                .HasKey(t => new { t.P2pId, t.ImageId });
           
            modelBuilder.Entity<P2PLanguage>()
                .HasKey(t => new { t.P2pId, t.LanguageId });

        }
    }
}
