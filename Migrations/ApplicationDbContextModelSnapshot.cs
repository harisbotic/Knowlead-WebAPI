using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Knowlead.DomainModel;

namespace KnowleadWebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("Knowlead.DomainModel.CoreModels.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Filename");

                    b.Property<int>("Height");

                    b.Property<int>("Width");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Knowlead.DomainModel.FeedbackModels._Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<string>("FeedbackText");

                    b.Property<int>("FosId");

                    b.Property<string>("StudentId");

                    b.Property<string>("TeacherId");

                    b.HasKey("FeedbackId");

                    b.HasIndex("FosId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Feedback");

                    b.HasDiscriminator<string>("Category").HasValue("_Feedback");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core._CoreLookup", b =>
                {
                    b.Property<int>("CoreLookupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("CoreLookupId");

                    b.ToTable("CoreLookup");

                    b.HasDiscriminator<string>("Category").HasValue("_CoreLookup");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Geo._GeoLookup", b =>
                {
                    b.Property<int>("GeoLookupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("GeoLookupId");

                    b.ToTable("GeoLookup");

                    b.HasDiscriminator<string>("Category").HasValue("_GeoLookup");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("AboutMe");

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("CityId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("StatusId");

                    b.Property<string>("Timezone");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("StatusId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserInterest", b =>
                {
                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("FosId");

                    b.HasKey("ApplicationUserId", "FosId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("FosId");

                    b.ToTable("ApplicationUserInterests");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserLanguage", b =>
                {
                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("LanguageId");

                    b.HasKey("ApplicationUserId", "LanguageId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("LanguageId");

                    b.ToTable("ApplicationUserLanguages");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserSkill", b =>
                {
                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("FosId");

                    b.HasKey("ApplicationUserId", "FosId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("FosId");

                    b.ToTable("ApplicationUserSkills");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.UserAchievement", b =>
                {
                    b.Property<int>("UserAchievementId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AchievementId");

                    b.Property<string>("ApplicationUserId");

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

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Desc");

                    b.Property<int>("ImageId");

                    b.Property<string>("Name");

                    b.HasKey("UserCertificateId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ImageId");

                    b.ToTable("UserCertificates");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.UserNotebook", b =>
                {
                    b.Property<int>("UserNotebookId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("ImageId");

                    b.Property<string>("Markdown");

                    b.Property<string>("Name");

                    b.HasKey("UserNotebookId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ImageId");

                    b.ToTable("UserNotebooks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.FeedbackModels.FeedbackClass", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.FeedbackModels._Feedback");


                    b.ToTable("Feedback");

                    b.HasDiscriminator().HasValue("Class");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.FeedbackModels.FeedbackCourse", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.FeedbackModels._Feedback");


                    b.ToTable("Feedback");

                    b.HasDiscriminator().HasValue("Course");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.FeedbackModels.FeedbackP2P", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.FeedbackModels._Feedback");

                    b.Property<float>("Accurate");

                    b.Property<float>("Knowleadge");

                    b.ToTable("Feedback");

                    b.HasDiscriminator().HasValue("P2P");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.FeedbackModels.FeedbackQuestion", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.FeedbackModels._Feedback");


                    b.ToTable("Feedback");

                    b.HasDiscriminator().HasValue("Question");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.Achievement", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.LookupModels.Core._CoreLookup");

                    b.Property<string>("Desc");

                    b.Property<int>("ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("CoreLookup");

                    b.HasDiscriminator().HasValue("Achievement");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.FOS", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.LookupModels.Core._CoreLookup");

                    b.Property<string>("FosDesc");

                    b.ToTable("CoreLookup");

                    b.HasDiscriminator().HasValue("Fos");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.Language", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.LookupModels.Core._CoreLookup");


                    b.ToTable("CoreLookup");

                    b.HasDiscriminator().HasValue("Language");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.Status", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.LookupModels.Core._CoreLookup");


                    b.ToTable("CoreLookup");

                    b.HasDiscriminator().HasValue("Status");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Geo.City", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.LookupModels.Geo._GeoLookup");


                    b.ToTable("GeoLookup");

                    b.HasDiscriminator().HasValue("City");
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Geo.Country", b =>
                {
                    b.HasBaseType("Knowlead.DomainModel.LookupModels.Geo._GeoLookup");


                    b.ToTable("GeoLookup");

                    b.HasDiscriminator().HasValue("Country");
                });

            modelBuilder.Entity("Knowlead.DomainModel.FeedbackModels._Feedback", b =>
                {
                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.FOS", "Fos")
                        .WithMany()
                        .HasForeignKey("FosId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUser", b =>
                {
                    b.HasOne("Knowlead.DomainModel.LookupModels.Geo.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserInterest", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.FOS", "Fos")
                        .WithMany()
                        .HasForeignKey("FosId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserLanguage", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany("ApplicationUserLanguages")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.Language", "Language")
                        .WithMany("ApplicationUserLanguages")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.ApplicationUserSkill", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.FOS", "Fos")
                        .WithMany()
                        .HasForeignKey("FosId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.UserAchievement", b =>
                {
                    b.HasOne("Knowlead.DomainModel.LookupModels.Core.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.UserCertificate", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Knowlead.DomainModel.CoreModels.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knowlead.DomainModel.UserModels.UserNotebook", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Knowlead.DomainModel.CoreModels.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Knowlead.DomainModel.UserModels.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knowlead.DomainModel.LookupModels.Core.Achievement", b =>
                {
                    b.HasOne("Knowlead.DomainModel.CoreModels.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
