using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Knowlead.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FOS",
                columns: table => new
                {
                    CoreLookupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentFosId = table.Column<int>(nullable: true),
                    Unlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOS", x => x.CoreLookupId);
                    table.ForeignKey(
                        name: "FK_FOS_FOS_ParentFosId",
                        column: x => x.ParentFosId,
                        principalTable: "FOS",
                        principalColumn: "CoreLookupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    CoreLookupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.CoreLookupId);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    CoreLookupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    MinutesReward = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PointsReward = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.CoreLookupId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    GeoLookupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.GeoLookupId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<string>(nullable: true),
                    ClientSecret = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    LogoutRedirectUri = table.Column<string>(nullable: true),
                    RedirectUri = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    GeoLookupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StatesCountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.GeoLookupId);
                    table.ForeignKey(
                        name: "FK_States_Countries_StatesCountryId",
                        column: x => x.StatesCountryId,
                        principalTable: "Countries",
                        principalColumn: "GeoLookupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: true),
                    Scope = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: true),
                    AuthorizationId = table.Column<Guid>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    CoreLookupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Desc = table.Column<string>(nullable: false),
                    ImageBlobId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.CoreLookupId);
                });

            migrationBuilder.CreateTable(
                name: "P2PFiles",
                columns: table => new
                {
                    P2pId = table.Column<int>(nullable: false),
                    FileBlobId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P2PFiles", x => new { x.P2pId, x.FileBlobId });
                });

            migrationBuilder.CreateTable(
                name: "P2PImages",
                columns: table => new
                {
                    P2pId = table.Column<int>(nullable: false),
                    ImageBlobId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P2PImages", x => new { x.P2pId, x.ImageBlobId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AboutMe = table.Column<string>(nullable: true),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AverageRating = table.Column<float>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    IsMale = table.Column<bool>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    MotherTongueId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    ProfilePictureId = table.Column<Guid>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Surname = table.Column<string>(nullable: true),
                    Timezone = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "GeoLookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Languages_MotherTongueId",
                        column: x => x.MotherTongueId,
                        principalTable: "Languages",
                        principalColumn: "CoreLookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "GeoLookupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blobs",
                columns: table => new
                {
                    BlobId = table.Column<Guid>(nullable: false),
                    BlobType = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: true),
                    Filename = table.Column<string>(nullable: true),
                    Filesize = table.Column<long>(nullable: false),
                    UploadedById = table.Column<Guid>(nullable: false),
                    Height = table.Column<int>(nullable: true),
                    Width = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blobs", x => x.BlobId);
                    table.ForeignKey(
                        name: "FK_Blobs_AspNetUsers_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    ApplicationUserSmallerId = table.Column<Guid>(nullable: false),
                    ApplicationUserBiggerId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastActionById = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.ApplicationUserSmallerId, x.ApplicationUserBiggerId });
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_ApplicationUserBiggerId",
                        column: x => x.ApplicationUserBiggerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_ApplicationUserSmallerId",
                        column: x => x.ApplicationUserSmallerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_LastActionById",
                        column: x => x.LastActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notebooks",
                columns: table => new
                {
                    NotebookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Markdown = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    PrimaryColor = table.Column<string>(nullable: false),
                    SecondaryColor = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notebooks", x => x.NotebookId);
                    table.ForeignKey(
                        name: "FK_Notebooks_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StickyNotes",
                columns: table => new
                {
                    StickyNoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NoteText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StickyNotes", x => x.StickyNoteId);
                    table.ForeignKey(
                        name: "FK_StickyNotes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "P2Ps",
                columns: table => new
                {
                    P2pId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookmarkCount = table.Column<int>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateTimeAgreed = table.Column<DateTime>(nullable: true),
                    Deadline = table.Column<DateTime>(nullable: true),
                    FosId = table.Column<int>(nullable: false),
                    InitialPrice = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OfferCount = table.Column<int>(nullable: false),
                    PriceAgreed = table.Column<int>(nullable: true),
                    ScheduledWithId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TeacherReady = table.Column<DateTime>(nullable: true),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P2Ps", x => x.P2pId);
                    table.ForeignKey(
                        name: "FK_P2Ps_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_P2Ps_FOS_FosId",
                        column: x => x.FosId,
                        principalTable: "FOS",
                        principalColumn: "CoreLookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_P2Ps_AspNetUsers_ScheduledWithId",
                        column: x => x.ScheduledWithId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlatformFeedbacks",
                columns: table => new
                {
                    PlatformFeedbackId = table.Column<Guid>(nullable: false),
                    Feedback = table.Column<string>(nullable: true),
                    SubmittedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformFeedbacks", x => x.PlatformFeedbackId);
                    table.ForeignKey(
                        name: "FK_PlatformFeedbacks_AspNetUsers_SubmittedById",
                        column: x => x.SubmittedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                columns: table => new
                {
                    AccountTransactionId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    FinalMinutesBalance = table.Column<int>(nullable: false),
                    FinalPointsBalance = table.Column<int>(nullable: false),
                    MinutesChangeAmount = table.Column<int>(nullable: false),
                    PointsChangeAmount = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactions", x => x.AccountTransactionId);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserInterests",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    FosId = table.Column<int>(nullable: false),
                    Stars = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserInterests", x => new { x.ApplicationUserId, x.FosId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserInterests_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserInterests_FOS_FosId",
                        column: x => x.FosId,
                        principalTable: "FOS",
                        principalColumn: "CoreLookupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserLanguages",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserLanguages", x => new { x.ApplicationUserId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserLanguages_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "CoreLookupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserReferrals",
                columns: table => new
                {
                    NewRegistredUserId = table.Column<Guid>(nullable: false),
                    ReferralUserId = table.Column<Guid>(nullable: false),
                    UserRegistred = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserReferrals", x => new { x.NewRegistredUserId, x.ReferralUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserReferrals_AspNetUsers_NewRegistredUserId",
                        column: x => x.NewRegistredUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserReferrals_AspNetUsers_ReferralUserId",
                        column: x => x.ReferralUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRewards",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    RewardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRewards", x => new { x.ApplicationUserId, x.RewardId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRewards_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRewards_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "CoreLookupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    UserAchievementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AchievementId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievements", x => x.UserAchievementId);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "CoreLookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAchievements_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCertificates",
                columns: table => new
                {
                    UserCertificateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Desc = table.Column<string>(nullable: true),
                    ImageBlobBlobId = table.Column<Guid>(nullable: true),
                    ImageBlobId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCertificates", x => x.UserCertificateId);
                    table.ForeignKey(
                        name: "FK_UserCertificates_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCertificates_Blobs_ImageBlobBlobId",
                        column: x => x.ImageBlobBlobId,
                        principalTable: "Blobs",
                        principalColumn: "BlobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calls",
                columns: table => new
                {
                    CallId = table.Column<Guid>(nullable: false),
                    CallType = table.Column<string>(nullable: false),
                    CallerId = table.Column<Guid>(nullable: false),
                    Duration = table.Column<double>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FailReason = table.Column<string>(nullable: true),
                    Failed = table.Column<bool>(nullable: false),
                    CallReceiverId = table.Column<Guid>(nullable: true),
                    P2PId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calls", x => x.CallId);
                    table.ForeignKey(
                        name: "FK_Calls_AspNetUsers_CallerId",
                        column: x => x.CallerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calls_AspNetUsers_CallReceiverId",
                        column: x => x.CallReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calls_P2Ps_P2PId",
                        column: x => x.P2PId,
                        principalTable: "P2Ps",
                        principalColumn: "P2pId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: false),
                    FeedbackText = table.Column<string>(nullable: false),
                    FosId = table.Column<int>(nullable: false),
                    Rating = table.Column<float>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    TeacherReply = table.Column<string>(nullable: true),
                    Expertise = table.Column<int>(nullable: true),
                    Helpful = table.Column<int>(nullable: true),
                    P2pId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_FOS_FosId",
                        column: x => x.FosId,
                        principalTable: "FOS",
                        principalColumn: "CoreLookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedbacks_P2Ps_P2pId",
                        column: x => x.P2pId,
                        principalTable: "P2Ps",
                        principalColumn: "P2pId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "P2PBookmarks",
                columns: table => new
                {
                    P2pId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P2PBookmarks", x => new { x.P2pId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_P2PBookmarks_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_P2PBookmarks_P2Ps_P2pId",
                        column: x => x.P2pId,
                        principalTable: "P2Ps",
                        principalColumn: "P2pId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "P2PLanguages",
                columns: table => new
                {
                    P2pId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P2PLanguages", x => new { x.P2pId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_P2PLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "CoreLookupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_P2PLanguages_P2Ps_P2pId",
                        column: x => x.P2pId,
                        principalTable: "P2Ps",
                        principalColumn: "P2pId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "P2PMessages",
                columns: table => new
                {
                    P2pMessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTimeOffer = table.Column<DateTime>(nullable: false),
                    MessageFromId = table.Column<Guid>(nullable: false),
                    MessageToId = table.Column<Guid>(nullable: false),
                    OfferAcceptedId = table.Column<int>(nullable: true),
                    P2pId = table.Column<int>(nullable: false),
                    PriceOffer = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P2PMessages", x => x.P2pMessageId);
                    table.ForeignKey(
                        name: "FK_P2PMessages_AspNetUsers_MessageFromId",
                        column: x => x.MessageFromId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_P2PMessages_AspNetUsers_MessageToId",
                        column: x => x.MessageToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_P2PMessages_P2PMessages_OfferAcceptedId",
                        column: x => x.OfferAcceptedId,
                        principalTable: "P2PMessages",
                        principalColumn: "P2pMessageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_P2PMessages_P2Ps_P2pId",
                        column: x => x.P2pId,
                        principalTable: "P2Ps",
                        principalColumn: "P2pId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(nullable: false),
                    ForApplicationUserId = table.Column<Guid>(nullable: false),
                    FromApplicationUserId = table.Column<Guid>(nullable: true),
                    NotificationType = table.Column<string>(nullable: false),
                    P2pId = table.Column<int>(nullable: true),
                    P2pMessageId = table.Column<int>(nullable: true),
                    ScheduledAt = table.Column<DateTime>(nullable: false),
                    SeenAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_ForApplicationUserId",
                        column: x => x.ForApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_FromApplicationUserId",
                        column: x => x.FromApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_P2Ps_P2pId",
                        column: x => x.P2pId,
                        principalTable: "P2Ps",
                        principalColumn: "P2pId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_P2PMessages_P2pMessageId",
                        column: x => x.P2pMessageId,
                        principalTable: "P2PMessages",
                        principalColumn: "P2pMessageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blobs_UploadedById",
                table: "Blobs",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_Calls_CallerId",
                table: "Calls",
                column: "CallerId");

            migrationBuilder.CreateIndex(
                name: "IX_Calls_CallReceiverId",
                table: "Calls",
                column: "CallReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Calls_P2PId",
                table: "Calls",
                column: "P2PId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ApplicationUserBiggerId",
                table: "Friendships",
                column: "ApplicationUserBiggerId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_LastActionById",
                table: "Friendships",
                column: "LastActionById");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FosId",
                table: "Feedbacks",
                column: "FosId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_StudentId",
                table: "Feedbacks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TeacherId",
                table: "Feedbacks",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_P2pId",
                table: "Feedbacks",
                column: "P2pId");

            migrationBuilder.CreateIndex(
                name: "IX_Notebooks_CreatedById",
                table: "Notebooks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StickyNotes_CreatedById",
                table: "StickyNotes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_ImageBlobId",
                table: "Achievements",
                column: "ImageBlobId");

            migrationBuilder.CreateIndex(
                name: "IX_FOS_ParentFosId",
                table: "FOS",
                column: "ParentFosId");

            migrationBuilder.CreateIndex(
                name: "IX_States_StatesCountryId",
                table: "States",
                column: "StatesCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ForApplicationUserId",
                table: "Notifications",
                column: "ForApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FromApplicationUserId",
                table: "Notifications",
                column: "FromApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_P2pId",
                table: "Notifications",
                column: "P2pId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_P2pMessageId",
                table: "Notifications",
                column: "P2pMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_P2Ps_CreatedById",
                table: "P2Ps",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_P2Ps_FosId",
                table: "P2Ps",
                column: "FosId");

            migrationBuilder.CreateIndex(
                name: "IX_P2Ps_ScheduledWithId",
                table: "P2Ps",
                column: "ScheduledWithId");

            migrationBuilder.CreateIndex(
                name: "IX_P2PBookmarks_ApplicationUserId",
                table: "P2PBookmarks",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_P2PFiles_FileBlobId",
                table: "P2PFiles",
                column: "FileBlobId");

            migrationBuilder.CreateIndex(
                name: "IX_P2PImages_ImageBlobId",
                table: "P2PImages",
                column: "ImageBlobId");

            migrationBuilder.CreateIndex(
                name: "IX_P2PLanguages_LanguageId",
                table: "P2PLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_P2PMessages_MessageFromId",
                table: "P2PMessages",
                column: "MessageFromId");

            migrationBuilder.CreateIndex(
                name: "IX_P2PMessages_MessageToId",
                table: "P2PMessages",
                column: "MessageToId");

            migrationBuilder.CreateIndex(
                name: "IX_P2PMessages_OfferAcceptedId",
                table: "P2PMessages",
                column: "OfferAcceptedId");

            migrationBuilder.CreateIndex(
                name: "IX_P2PMessages_P2pId",
                table: "P2PMessages",
                column: "P2pId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformFeedbacks_SubmittedById",
                table: "PlatformFeedbacks",
                column: "SubmittedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_ApplicationUserId",
                table: "AccountTransactions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MotherTongueId",
                table: "AspNetUsers",
                column: "MotherTongueId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StateId",
                table: "AspNetUsers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserInterests_FosId",
                table: "ApplicationUserInterests",
                column: "FosId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserLanguages_LanguageId",
                table: "ApplicationUserLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserReferrals_ReferralUserId",
                table: "ApplicationUserReferrals",
                column: "ReferralUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRewards_RewardId",
                table: "ApplicationUserRewards",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_ApplicationUserId",
                table: "UserAchievements",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCertificates_ApplicationUserId",
                table: "UserCertificates",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCertificates_ImageBlobBlobId",
                table: "UserCertificates",
                column: "ImageBlobBlobId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true,
                filter: "[ClientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                table: "OpenIddictAuthorizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                table: "OpenIddictTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Blobs_ImageBlobId",
                table: "Achievements",
                column: "ImageBlobId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PFiles_P2Ps_P2pId",
                table: "P2PFiles",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PFiles_Blobs_FileBlobId",
                table: "P2PFiles",
                column: "FileBlobId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PImages_P2Ps_P2pId",
                table: "P2PImages",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PImages_Blobs_ImageBlobId",
                table: "P2PImages",
                column: "ImageBlobId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Blobs_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blobs_AspNetUsers_UploadedById",
                table: "Blobs");

            migrationBuilder.DropTable(
                name: "Calls");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Notebooks");

            migrationBuilder.DropTable(
                name: "StickyNotes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "P2PBookmarks");

            migrationBuilder.DropTable(
                name: "P2PFiles");

            migrationBuilder.DropTable(
                name: "P2PImages");

            migrationBuilder.DropTable(
                name: "P2PLanguages");

            migrationBuilder.DropTable(
                name: "PlatformFeedbacks");

            migrationBuilder.DropTable(
                name: "AccountTransactions");

            migrationBuilder.DropTable(
                name: "ApplicationUserInterests");

            migrationBuilder.DropTable(
                name: "ApplicationUserLanguages");

            migrationBuilder.DropTable(
                name: "ApplicationUserReferrals");

            migrationBuilder.DropTable(
                name: "ApplicationUserRewards");

            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "UserCertificates");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "P2PMessages");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "P2Ps");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");

            migrationBuilder.DropTable(
                name: "FOS");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Blobs");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
