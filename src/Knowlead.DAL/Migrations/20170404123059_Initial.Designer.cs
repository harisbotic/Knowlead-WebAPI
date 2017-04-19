using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Knowlead.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170404123059_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.2.0-preview1-24245")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Knowlead.DomainModel.BlobModels._Blob", b =>
                {
                    b.Property<Guid>("BlobId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlobType")
                        .IsRequired();

                    b.Property<string>("Extension");

                    b.Property<string>("Filename");

                    b.Property<long>("Filesize");

                    b.Property<Guid>("UploadedById");

                    b.HasKey("BlobId");

                    b.ToTable("Blobs");

                    b.HasDiscriminator<string>("BlobType").HasValue("_Blob");
                });

            modelBuilder.Entity("Knowlead.DomainModel.CallModels._Call", b =>
                {
                    b.Property<Guid>("CallId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CallType")
                        .IsRequired();

                    b.Property<Guid>("CallerId");

                    b.Property<double>("Duration");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FailReason");

                    b.Property<bool>("Failed");

                    b.HasKey("CallId");

                    b.HasIndex("CallerId");

                    b.ToTable("Calls");

                    b.HasDiscriminator<string>("CallType").HasValue("_Call");
                });

            modelBuilder.Entity("Knowlead.DomainModel.ChatModels.Friendship", b =>
                {
                    b.Property<Guid>("ApplicationUserSmallerId");

                    b.Property<Guid>("ApplicationUserBiggerId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<Guid>("LastActionById");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ApplicationUserSmallerId", "ApplicationUserBiggerId");

                    b.HasIndex("ApplicationUserBiggerId");

                    b.HasIndex("LastActionById");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("Knowlead.DomainModel.FeedbackModels._Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<string>("FeedbackText")
                        .IsRequired();

                    b.Property<int>("FosId");

                    b.Property<float>("Rating");

                    b.Property<Guid>("StudentId");

                    b.Property<Guid>("TeacherId");

                    b.Property<string>("TeacherReply");

                    b.HasKey("FeedbackId");

                    b.HasIndex("FosId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Feedbacks");

                    b.HasDiscriminator<string>("Category").HasValue("_Feedback");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LibraryModels.Notebook", b =>
                {
                    b.Property<int>("NotebookId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<Guid>("CreatedById");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Markdown");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PrimaryColor")
                        .IsRequired();

                    b.Property<string>("SecondaryColor")
                        .IsRequired();

                    b.HasKey("NotebookId");

                    b.HasIndex("CreatedById");

                    b.ToTable("Notebooks");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LibraryModels.StickyNote", b =>
                {
                    b.Property<int>("StickyNoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<Guid>("CreatedById");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NoteText");

                    b.HasKey("StickyNoteId");

                    b.HasIndex("CreatedById");

                    b.ToTable("StickyNotes");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.Achievement", b =>
                {
                    b.Property<int>("CoreLookupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Desc")
                        .IsRequired();

                    b.Property<Guid>("ImageBlobId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CoreLookupId");

                    b.HasIndex("ImageBlobId");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.FOS", b =>
                {
                    b.Property<int>("CoreLookupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("ParentFosId");

                    b.Property<bool>("Unlocked");

                    b.HasKey("CoreLookupId");

                    b.HasIndex("ParentFosId");

                    b.ToTable("FOS");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.Language", b =>
                {
                    b.Property<int>("CoreLookupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CoreLookupId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.Reward", b =>
                {
                    b.Property<int>("CoreLookupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<int>("MinutesReward");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PointsReward");

                    b.HasKey("CoreLookupId");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Geo.Country", b =>
                {
                    b.Property<int>("GeoLookupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("GeoLookupId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Geo.State", b =>
                {
                    b.Property<int>("GeoLookupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("StatesCountryId");

                    b.HasKey("GeoLookupId");

                    b.HasIndex("StatesCountryId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Knowlead.DomainModel.NotificationModels.Notification", b =>
                {
                    b.Property<Guid>("NotificationId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ForApplicationUserId");

                    b.Property<Guid?>("FromApplicationUserId");

                    b.Property<string>("NotificationType")
                        .IsRequired();

                    b.Property<int?>("P2pId");

                    b.Property<int?>("P2pMessageId");

                    b.Property<DateTime>("ScheduledAt");

                    b.Property<DateTime?>("SeenAt");

                    b.HasKey("NotificationId");

                    b.HasIndex("ForApplicationUserId");

                    b.HasIndex("FromApplicationUserId");

                    b.HasIndex("P2pId");

                    b.HasIndex("P2pMessageId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2P", b =>
                {
                    b.Property<int>("P2pId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookmarkCount");

                    b.Property<Guid>("CreatedById");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateTimeAgreed");

                    b.Property<DateTime?>("Deadline");

                    b.Property<int>("FosId");

                    b.Property<int>("InitialPrice");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("OfferCount");

                    b.Property<int?>("PriceAgreed");

                    b.Property<Guid?>("ScheduledWithId");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("TeacherReady");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("P2pId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("FosId");

                    b.HasIndex("ScheduledWithId");

                    b.ToTable("P2Ps");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PBookmark", b =>
                {
                    b.Property<int>("P2pId");

                    b.Property<Guid>("ApplicationUserId");

                    b.HasKey("P2pId", "ApplicationUserId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("P2PBookmarks");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PFile", b =>
                {
                    b.Property<int>("P2pId");

                    b.Property<Guid>("FileBlobId");

                    b.HasKey("P2pId", "FileBlobId");

                    b.HasIndex("FileBlobId");

                    b.ToTable("P2PFiles");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PImage", b =>
                {
                    b.Property<int>("P2pId");

                    b.Property<Guid>("ImageBlobId");

                    b.HasKey("P2pId", "ImageBlobId");

                    b.HasIndex("ImageBlobId");

                    b.ToTable("P2PImages");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PLanguage", b =>
                {
                    b.Property<int>("P2pId");

                    b.Property<int>("LanguageId");

                    b.HasKey("P2pId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("P2PLanguages");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PMessage", b =>
                {
                    b.Property<int>("P2pMessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTimeOffer");

                    b.Property<Guid>("MessageFromId");

                    b.Property<Guid>("MessageToId");

                    b.Property<int?>("OfferAcceptedId");

                    b.Property<int>("P2pId");

                    b.Property<int>("PriceOffer");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("P2pMessageId");

                    b.HasIndex("MessageFromId");

                    b.HasIndex("MessageToId");

                    b.HasIndex("OfferAcceptedId");

                    b.HasIndex("P2pId");

                    b.ToTable("P2PMessages");
                });

            modelBuilder.Entity("Knowlead.DomainModel.StatisticsModels.PlatformFeedback", b =>
                {
                    b.Property<Guid>("PlatformFeedbackId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Feedback");

                    b.Property<Guid>("SubmittedById");

                    b.HasKey("PlatformFeedbackId");

                    b.HasIndex("SubmittedById");

                    b.ToTable("PlatformFeedbacks");
                });

            modelBuilder.Entity("Knowlead.DomainModel.TransactionModels.AccountTransaction", b =>
                {
                    b.Property<Guid>("AccountTransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationUserId");

                    b.Property<int>("FinalMinutesBalance");

                    b.Property<int>("FinalPointsBalance");

                    b.Property<int>("MinutesChangeAmount");

                    b.Property<int>("PointsChangeAmount");

                    b.Property<string>("Reason")
                        .IsRequired();

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("AccountTransactionId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("AccountTransactions");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AboutMe");

                    b.Property<int>("AccessFailedCount");

                    b.Property<float>("AverageRating");

                    b.Property<DateTime?>("Birthdate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int?>("CountryId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool?>("IsMale");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<int?>("MotherTongueId");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<Guid?>("ProfilePictureId");

                    b.Property<string>("SecurityStamp");

                    b.Property<int?>("StateId");

                    b.Property<int>("Status");

                    b.Property<string>("Surname");

                    b.Property<string>("Timezone");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("MotherTongueId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasAnnotation("SqlServer:Filter", "[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ProfilePictureId");

                    b.HasIndex("StateId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserInterest", b =>
                {
                    b.Property<Guid>("ApplicationUserId");

                    b.Property<int>("FosId");

                    b.Property<int>("Stars");

                    b.HasKey("ApplicationUserId", "FosId");

                    b.HasIndex("FosId");

                    b.ToTable("ApplicationUserInterests");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserLanguage", b =>
                {
                    b.Property<Guid>("ApplicationUserId");

                    b.Property<int>("LanguageId");

                    b.HasKey("ApplicationUserId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("ApplicationUserLanguages");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserReferral", b =>
                {
                    b.Property<Guid>("NewRegistredUserId");

                    b.Property<Guid>("ReferralUserId");

                    b.Property<bool>("UserRegistred");

                    b.HasKey("NewRegistredUserId", "ReferralUserId");

                    b.HasIndex("ReferralUserId");

                    b.ToTable("ApplicationUserReferrals");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserReward", b =>
                {
                    b.Property<Guid>("ApplicationUserId");

                    b.Property<int>("RewardId");

                    b.HasKey("ApplicationUserId", "RewardId");

                    b.HasIndex("RewardId");

                    b.ToTable("ApplicationUserRewards");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.UserAchievement", b =>
                {
                    b.Property<int>("UserAchievementId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AchievementId");

                    b.Property<Guid>("ApplicationUserId");

                    b.Property<DateTime>("CreatedAt");

                    b.HasKey("UserAchievementId");

                    b.HasIndex("AchievementId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserAchievements");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.UserCertificate", b =>
                {
                    b.Property<int>("UserCertificateId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationUserId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Desc");

                    b.Property<Guid?>("ImageBlobBlobId");

                    b.Property<int>("ImageBlobId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("UserCertificateId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ImageBlobBlobId");

                    b.ToTable("UserCertificates");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasAnnotation("SqlServer:Filter", "[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictApplication<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<string>("ClientSecret");

                    b.Property<string>("DisplayName");

                    b.Property<string>("LogoutRedirectUri");

                    b.Property<string>("RedirectUri");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Filter", "[ClientId] IS NOT NULL");

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationId");

                    b.Property<string>("Scope");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictScope<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationId");

                    b.Property<Guid?>("AuthorizationId");

                    b.Property<string>("Subject");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("Knowlead.DomainModel.BlobModels.FileBlob", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.BlobModels._Blob");


                    b.HasIndex("UploadedById");

                    b.ToTable("FileBlob");

                    b.HasDiscriminator().HasValue("File");
                });

            modelBuilder.Entity("Knowlead.DomainModel.BlobModels.ImageBlob", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.BlobModels._Blob");

                    b.Property<int>("Height");

                    b.Property<int>("Width");

                    b.HasIndex("UploadedById");

                    b.ToTable("ImageBlob");

                    b.HasDiscriminator().HasValue("Image");
                });

            modelBuilder.Entity("Knowlead.DomainModel.CallModels.FriendCall", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.CallModels._Call");

                    b.Property<Guid>("CallReceiverId");

                    b.HasIndex("CallReceiverId");

                    b.ToTable("FriendCall");

                    b.HasDiscriminator().HasValue("FriendCall");
                });

            modelBuilder.Entity("Knowlead.DomainModel.CallModels.P2PCall", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.CallModels._Call");

                    b.Property<Guid>("CallReceiverId");

                    b.Property<int>("P2PId");

                    b.HasIndex("CallReceiverId");

                    b.HasIndex("P2PId");

                    b.ToTable("P2PCall");

                    b.HasDiscriminator().HasValue("P2PCall");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.FeedbackModels.ClassFeedback", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.FeedbackModels._Feedback");


                    b.ToTable("Feedbacks");

                    b.HasDiscriminator().HasValue("Class");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.FeedbackModels.CourseFeedback", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.FeedbackModels._Feedback");


                    b.ToTable("Feedbacks");

                    b.HasDiscriminator().HasValue("Course");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.FeedbackModels.P2PFeedback", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.FeedbackModels._Feedback");

                    b.Property<int>("Expertise");

                    b.Property<int>("Helpful");

                    b.Property<int>("P2pId");

                    b.HasIndex("P2pId");

                    b.ToTable("Feedbacks");

                    b.HasDiscriminator().HasValue("P2P");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.FeedbackModels.QuestionFeedback", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.FeedbackModels._Feedback");


                    b.ToTable("Feedbacks");

                    b.HasDiscriminator().HasValue("Question");
                });

            modelBuilder.Entity("Knowlead.DomainModel.CallModels._Call", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "Caller")
                        .WithMany()
                        .HasForeignKey("CallerId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.ChatModels.Friendship", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUserBigger")
                        .WithMany()
                        .HasForeignKey("ApplicationUserBiggerId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUserSmaller")
                        .WithMany()
                        .HasForeignKey("ApplicationUserSmallerId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "LastActionBy")
                        .WithMany()
                        .HasForeignKey("LastActionById");
                });

            modelBuilder.Entity("Knowlead.DomainModel.FeedbackModels._Feedback", b =>
                {
                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.FOS", "Fos")
                        .WithMany()
                        .HasForeignKey("FosId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LibraryModels.Notebook", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LibraryModels.StickyNote", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.Achievement", b =>
                {
                    b.HasOne("Knowlead.DomainModel.BlobModels.ImageBlob", "ImageBlob")
                        .WithMany()
                        .HasForeignKey("ImageBlobId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.FOS", b =>
                {
                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.FOS", "ParentFos")
                        .WithMany()
                        .HasForeignKey("ParentFosId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Geo.State", b =>
                {
                    b.HasOne("Knowlead.DomainModel.LookupModels.Geo.Country", "StatesCountry")
                        .WithMany()
                        .HasForeignKey("StatesCountryId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.NotificationModels.Notification", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ForApplicationUser")
                        .WithMany()
                        .HasForeignKey("ForApplicationUserId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "FromApplicationUser")
                        .WithMany()
                        .HasForeignKey("FromApplicationUserId");

                    b.HasOne("Knowlead.DomainModel.P2PModels.P2P", "P2p")
                        .WithMany()
                        .HasForeignKey("P2pId");

                    b.HasOne("Knowlead.DomainModel.P2PModels.P2PMessage", "P2pMessage")
                        .WithMany()
                        .HasForeignKey("P2pMessageId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2P", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.FOS", "Fos")
                        .WithMany()
                        .HasForeignKey("FosId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ScheduledWith")
                        .WithMany()
                        .HasForeignKey("ScheduledWithId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PBookmark", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Knowlead.DomainModel.P2PModels.P2P", "P2p")
                        .WithMany()
                        .HasForeignKey("P2pId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PFile", b =>
                {
                    b.HasOne("Knowlead.DomainModel.BlobModels.FileBlob", "FileBlob")
                        .WithMany()
                        .HasForeignKey("FileBlobId");

                    b.HasOne("Knowlead.DomainModel.P2PModels.P2P", "P2p")
                        .WithMany("P2PFiles")
                        .HasForeignKey("P2pId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PImage", b =>
                {
                    b.HasOne("Knowlead.DomainModel.BlobModels.ImageBlob", "ImageBlob")
                        .WithMany()
                        .HasForeignKey("ImageBlobId");

                    b.HasOne("Knowlead.DomainModel.P2PModels.P2P", "P2p")
                        .WithMany("P2PImages")
                        .HasForeignKey("P2pId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PLanguage", b =>
                {
                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("Knowlead.DomainModel.P2PModels.P2P", "P2p")
                        .WithMany("P2PLanguages")
                        .HasForeignKey("P2pId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.P2PModels.P2PMessage", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "MessageFrom")
                        .WithMany()
                        .HasForeignKey("MessageFromId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "MessageTo")
                        .WithMany()
                        .HasForeignKey("MessageToId");

                    b.HasOne("Knowlead.DomainModel.P2PModels.P2PMessage", "OfferAccepted")
                        .WithMany()
                        .HasForeignKey("OfferAcceptedId");

                    b.HasOne("Knowlead.DomainModel.P2PModels.P2P", "P2p")
                        .WithMany()
                        .HasForeignKey("P2pId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.StatisticsModels.PlatformFeedback", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "SubmittedBy")
                        .WithMany()
                        .HasForeignKey("SubmittedById");
                });

            modelBuilder.Entity("Knowlead.DomainModel.TransactionModels.AccountTransaction", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUser", b =>
                {
                    b.HasOne("Knowlead.DomainModel.LookupModels.Geo.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.Language", "MotherTongue")
                        .WithMany()
                        .HasForeignKey("MotherTongueId");

                    b.HasOne("Knowlead.DomainModel.BlobModels.ImageBlob", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId");

                    b.HasOne("Knowlead.DomainModel.LookupModels.Geo.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserInterest", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany("ApplicationUserInterests")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.FOS", "Fos")
                        .WithMany()
                        .HasForeignKey("FosId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserLanguage", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany("ApplicationUserLanguages")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserReferral", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "NewRegistredUser")
                        .WithMany()
                        .HasForeignKey("NewRegistredUserId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ReferralUser")
                        .WithMany()
                        .HasForeignKey("ReferralUserId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserReward", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.Reward", "Reward")
                        .WithMany()
                        .HasForeignKey("RewardId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.UserAchievement", b =>
                {
                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.UserCertificate", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Knowlead.DomainModel.BlobModels.ImageBlob", "ImageBlob")
                        .WithMany()
                        .HasForeignKey("ImageBlobBlobId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<System.Guid>")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<System.Guid>")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization<System.Guid>", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication<System.Guid>", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken<System.Guid>", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication<System.Guid>", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("OpenIddict.Models.OpenIddictAuthorization<System.Guid>", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.BlobModels.FileBlob", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById");
                });

            modelBuilder.Entity("Knowlead.DomainModel.BlobModels.ImageBlob", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById");
                });

            modelBuilder.Entity("Knowlead.DomainModel.CallModels.FriendCall", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "CallReceiver")
                        .WithMany()
                        .HasForeignKey("CallReceiverId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.CallModels.P2PCall", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "CallReceiver")
                        .WithMany()
                        .HasForeignKey("CallReceiverId");

                    b.HasOne("Knowlead.DomainModel.P2PModels.P2P", "P2P")
                        .WithMany()
                        .HasForeignKey("P2PId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.FeedbackModels.P2PFeedback", b =>
                {
                    b.HasOne("Knowlead.DomainModel.P2PModels.P2P", "P2p")
                        .WithMany()
                        .HasForeignKey("P2pId");
                });
        }
    }
}
