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
using Knowlead.DomainModel.TransactionModels;
using Knowlead.DomainModel.LibraryModels;

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
        public DbSet<Reward> Rewards { get; set; }
        #endregion

        #region Feedback Models
        public DbSet<_Feedback> Feedbacks { get; set; }
        public DbSet<ClassFeedback> ClassFeedbacks { get; set; }
        public DbSet<CourseFeedback> CourseFeedbacks { get; set; }
        public DbSet<P2PFeedback> P2PFeedbacks { get; set; }
        public DbSet<QuestionFeedback> QuestionsFeedbacks { get; set; }
        #endregion

        #region Library Models
        public DbSet<Notebook> Notebooks { get; set; }
        #endregion

        #region User Models
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserInterest> ApplicationUserInterests { get; set; }
        public DbSet<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
        public DbSet<ApplicationUserReferral> ApplicationUserReferrals { get; set; }
        public DbSet<ApplicationUserReward> ApplicationUserRewards { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<UserCertificate> UserCertificates { get; set; }
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
        #region Transaction Models
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
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
            modelBuilder.Entity<Reward>().ToTable("Rewards");

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
                .HasValue<ClassFeedback>("Class")
                .HasValue<CourseFeedback>("Course")
                .HasValue<P2PFeedback>("P2P")
                .HasValue<QuestionFeedback>("Question");

            /* Application User */
            modelBuilder.Entity<ApplicationUserInterest>()
                .HasKey(t => new { t.ApplicationUserId, t.FosId });

            modelBuilder.Entity<ApplicationUserLanguage>()
                .HasKey(t => new { t.ApplicationUserId, t.LanguageId });

            modelBuilder.Entity<ApplicationUserReferral>()
                .HasKey(t => new { t.NewRegistredUserId, t.ReferralUserId });

            modelBuilder.Entity<ApplicationUserReward>()
                .HasKey(t => new { t.ApplicationUserId, t.RewardId });

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
