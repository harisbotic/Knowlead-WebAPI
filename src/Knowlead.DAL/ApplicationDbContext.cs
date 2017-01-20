using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Knowlead.DomainModel.UserModels;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.LookupModels.Geo;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DomainModel.FeedbackModels;
using Knowlead.DomainModel.LookupModels.FeedbackModels;
using Knowlead.DomainModel.P2PModels;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Knowlead.DomainModel.CallModels;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;
using Knowlead.DomainModel.ChatModels;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.DomainModel.StatisticsModels;

namespace Knowlead.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private IConfigurationRoot _config;
        public ApplicationDbContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        #region Lookup Models
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<FOS> Fos { get; set; }
        public DbSet<Language> Languages { get; set; }
        #endregion

        #region Feedback Models
        public DbSet<FeedbackClass> FeedbackClasses { get; set; }
        public DbSet<FeedbackCourse> FeedbackCourses { get; set; }
        public DbSet<FeedbackP2P> FeedbackP2P { get; set; }
        public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
        #endregion

        #region User Models
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserInterest> ApplicationUserInterests { get; set; }
        public DbSet<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<UserCertificate> UserCertificates { get; set; }
        public DbSet<UserNotebook> UserNotebooks { get; set; }
        #endregion

        #region Peer to Peer
        public DbSet<P2P> P2p { get; set; }
        public DbSet<P2PMessage> P2PMessages { get; set; }
        public DbSet<P2PFile> P2PFiles { get; set; }
        public DbSet<P2PImage> P2PImages { get; set; }
        public DbSet<P2PLanguage> P2PLanguages { get; set; }
        #endregion

        #region Blob Models
        public DbSet<_Blob> Blobs { get; set; }
        public DbSet<ImageBlob> ImageBlobs { get; set; }
        public DbSet<FileBlob> FileBlobs { get; set; }
        #endregion

        #region Call Models
        public DbSet<_Call> Calls { get; set; }
        public DbSet<P2PCall> P2PCalls { get; set; }
        public DbSet<FriendCall> FriendCalls { get; set; }
        #endregion

        #region Statistics Models
        public DbSet<PlatformFeedback> PlatformFeedbacks { get; set; }
        #endregion

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql(_config["ConnectionStrings:LocalPostgreSQL"],
                                            b => b.MigrationsAssembly("Knowlead.WebApi"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseOpenIddict<Guid>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            /* GeoLookups */
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<State>().ToTable("States");

            /* CoreLookups */
            modelBuilder.Entity<Achievement>().ToTable("Achievements");
            modelBuilder.Entity<FOS>().ToTable("FOS");
            modelBuilder.Entity<Language>().ToTable("Languages");

            /* Blobs */
            modelBuilder.Entity<_Blob>()
                .HasDiscriminator<string>("BlobType")
                .HasValue<ImageBlob>("Image")
                .HasValue<FileBlob>("File");

            /* Calls */
            modelBuilder.Entity<_Call>()
                .HasDiscriminator<string>("CallType")
                .HasValue<P2PCall>("Image")
                .HasValue<FriendCall>("File");

            modelBuilder.Entity<_Feedback>()
                .HasDiscriminator<string>("Category")
                .HasValue<FeedbackClass>("Class")
                .HasValue<FeedbackCourse>("Course")
                .HasValue<FeedbackP2P>("P2P")
                .HasValue<FeedbackQuestion>("Question");

            /* Application User */
            modelBuilder.Entity<ApplicationUserInterest>()
                .HasKey(t => new { t.ApplicationUserId, t.FosId });

            modelBuilder.Entity<ApplicationUserLanguage>()
                .HasKey(t => new { t.ApplicationUserId, t.LanguageId });

            /* Chat */
            modelBuilder.Entity<Friendship>()
                .HasKey(t => new { t.ApplicationUserSmallerId, t.ApplicationUserBiggerId });

            /* P2P */
            modelBuilder.Entity<P2PFile>()
                .HasKey(t => new { t.P2pId, t.FileBlobId });

            modelBuilder.Entity<P2PImage>()
                .HasKey(t => new { t.P2pId, t.ImageBlobId });

            modelBuilder.Entity<P2PLanguage>()
                .HasKey(t => new { t.P2pId, t.LanguageId });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}
