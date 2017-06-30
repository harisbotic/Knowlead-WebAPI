using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Knowlead.DAL.Migrations
{
    public partial class P2PDifficultyLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_AspNetUsers_ApplicationUserId",
                table: "AccountTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Blobs_ImageBlobId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserInterests_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserInterests_FOS_FosId",
                table: "ApplicationUserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserLanguages_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserLanguages_Languages_LanguageId",
                table: "ApplicationUserLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserReferrals_AspNetUsers_NewRegistredUserId",
                table: "ApplicationUserReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserReferrals_AspNetUsers_ReferralUserId",
                table: "ApplicationUserReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRewards_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRewards_Rewards_RewardId",
                table: "ApplicationUserRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Languages_MotherTongueId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Blobs_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Blobs_AspNetUsers_UploadedById",
                table: "Blobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_AspNetUsers_CallerId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_AspNetUsers_CallReceiverId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_P2Ps_P2PId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FOS_FosId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_StudentId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_TeacherId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_P2Ps_P2pId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_FOS_FOS_ParentFosId",
                table: "FOS");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserBiggerId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserSmallerId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_LastActionById",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Notebooks_AspNetUsers_CreatedById",
                table: "Notebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ForApplicationUserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_FromApplicationUserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_P2Ps_P2pId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_P2PMessages_P2pMessageId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PBookmarks_AspNetUsers_ApplicationUserId",
                table: "P2PBookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PBookmarks_P2Ps_P2pId",
                table: "P2PBookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PFiles_Blobs_FileBlobId",
                table: "P2PFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PFiles_P2Ps_P2pId",
                table: "P2PFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PImages_Blobs_ImageBlobId",
                table: "P2PImages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PImages_P2Ps_P2pId",
                table: "P2PImages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PLanguages_Languages_LanguageId",
                table: "P2PLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PLanguages_P2Ps_P2pId",
                table: "P2PLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PMessages_AspNetUsers_MessageFromId",
                table: "P2PMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PMessages_AspNetUsers_MessageToId",
                table: "P2PMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PMessages_P2PMessages_OfferAcceptedId",
                table: "P2PMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PMessages_P2Ps_P2pId",
                table: "P2PMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2Ps_AspNetUsers_CreatedById",
                table: "P2Ps");

            migrationBuilder.DropForeignKey(
                name: "FK_P2Ps_FOS_FosId",
                table: "P2Ps");

            migrationBuilder.DropForeignKey(
                name: "FK_P2Ps_AspNetUsers_ScheduledWithId",
                table: "P2Ps");

            migrationBuilder.DropForeignKey(
                name: "FK_PlatformFeedbacks_AspNetUsers_SubmittedById",
                table: "PlatformFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_StatesCountryId",
                table: "States");

            migrationBuilder.DropForeignKey(
                name: "FK_StickyNotes_AspNetUsers_CreatedById",
                table: "StickyNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_AspNetUsers_ApplicationUserId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_AspNetUsers_ApplicationUserId",
                table: "UserCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_Blobs_ImageBlobBlobId",
                table: "UserCertificates");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "CallReceiverId",
                table: "Calls",
                newName: "P2PCall_CallReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Calls_CallReceiverId",
                table: "Calls",
                newName: "IX_Calls_P2PCall_CallReceiverId");

            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevel",
                table: "P2Ps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CallReceiverId",
                table: "Calls",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calls_CallReceiverId",
                table: "Calls",
                column: "CallReceiverId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_AspNetUsers_ApplicationUserId",
                table: "AccountTransactions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Blobs_ImageBlobId",
                table: "Achievements",
                column: "ImageBlobId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserInterests_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserInterests",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserInterests_FOS_FosId",
                table: "ApplicationUserInterests",
                column: "FosId",
                principalTable: "FOS",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserLanguages_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserLanguages",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserLanguages_Languages_LanguageId",
                table: "ApplicationUserLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserReferrals_AspNetUsers_NewRegistredUserId",
                table: "ApplicationUserReferrals",
                column: "NewRegistredUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserReferrals_AspNetUsers_ReferralUserId",
                table: "ApplicationUserReferrals",
                column: "ReferralUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRewards_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserRewards",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRewards_Rewards_RewardId",
                table: "ApplicationUserRewards",
                column: "RewardId",
                principalTable: "Rewards",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "GeoLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Languages_MotherTongueId",
                table: "AspNetUsers",
                column: "MotherTongueId",
                principalTable: "Languages",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Blobs_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "States",
                principalColumn: "GeoLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blobs_AspNetUsers_UploadedById",
                table: "Blobs",
                column: "UploadedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_AspNetUsers_CallerId",
                table: "Calls",
                column: "CallerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_AspNetUsers_CallReceiverId",
                table: "Calls",
                column: "CallReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_AspNetUsers_P2PCall_CallReceiverId",
                table: "Calls",
                column: "P2PCall_CallReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_P2Ps_P2PId",
                table: "Calls",
                column: "P2PId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FOS_FosId",
                table: "Feedbacks",
                column: "FosId",
                principalTable: "FOS",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_StudentId",
                table: "Feedbacks",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_TeacherId",
                table: "Feedbacks",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_P2Ps_P2pId",
                table: "Feedbacks",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FOS_FOS_ParentFosId",
                table: "FOS",
                column: "ParentFosId",
                principalTable: "FOS",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserBiggerId",
                table: "Friendships",
                column: "ApplicationUserBiggerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserSmallerId",
                table: "Friendships",
                column: "ApplicationUserSmallerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_LastActionById",
                table: "Friendships",
                column: "LastActionById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notebooks_AspNetUsers_CreatedById",
                table: "Notebooks",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ForApplicationUserId",
                table: "Notifications",
                column: "ForApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_FromApplicationUserId",
                table: "Notifications",
                column: "FromApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_P2Ps_P2pId",
                table: "Notifications",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_P2PMessages_P2pMessageId",
                table: "Notifications",
                column: "P2pMessageId",
                principalTable: "P2PMessages",
                principalColumn: "P2pMessageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PBookmarks_AspNetUsers_ApplicationUserId",
                table: "P2PBookmarks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PBookmarks_P2Ps_P2pId",
                table: "P2PBookmarks",
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
                name: "FK_P2PFiles_P2Ps_P2pId",
                table: "P2PFiles",
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
                name: "FK_P2PImages_P2Ps_P2pId",
                table: "P2PImages",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PLanguages_Languages_LanguageId",
                table: "P2PLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PLanguages_P2Ps_P2pId",
                table: "P2PLanguages",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PMessages_AspNetUsers_MessageFromId",
                table: "P2PMessages",
                column: "MessageFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PMessages_AspNetUsers_MessageToId",
                table: "P2PMessages",
                column: "MessageToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PMessages_P2PMessages_OfferAcceptedId",
                table: "P2PMessages",
                column: "OfferAcceptedId",
                principalTable: "P2PMessages",
                principalColumn: "P2pMessageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PMessages_P2Ps_P2pId",
                table: "P2PMessages",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2Ps_AspNetUsers_CreatedById",
                table: "P2Ps",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2Ps_FOS_FosId",
                table: "P2Ps",
                column: "FosId",
                principalTable: "FOS",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2Ps_AspNetUsers_ScheduledWithId",
                table: "P2Ps",
                column: "ScheduledWithId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformFeedbacks_AspNetUsers_SubmittedById",
                table: "PlatformFeedbacks",
                column: "SubmittedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_States_Countries_StatesCountryId",
                table: "States",
                column: "StatesCountryId",
                principalTable: "Countries",
                principalColumn: "GeoLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StickyNotes_AspNetUsers_CreatedById",
                table: "StickyNotes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_AspNetUsers_ApplicationUserId",
                table: "UserAchievements",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_AspNetUsers_ApplicationUserId",
                table: "UserCertificates",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_Blobs_ImageBlobBlobId",
                table: "UserCertificates",
                column: "ImageBlobBlobId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_AspNetUsers_ApplicationUserId",
                table: "AccountTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Blobs_ImageBlobId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserInterests_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserInterests_FOS_FosId",
                table: "ApplicationUserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserLanguages_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserLanguages_Languages_LanguageId",
                table: "ApplicationUserLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserReferrals_AspNetUsers_NewRegistredUserId",
                table: "ApplicationUserReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserReferrals_AspNetUsers_ReferralUserId",
                table: "ApplicationUserReferrals");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRewards_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRewards_Rewards_RewardId",
                table: "ApplicationUserRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Languages_MotherTongueId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Blobs_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Blobs_AspNetUsers_UploadedById",
                table: "Blobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_AspNetUsers_CallerId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_AspNetUsers_CallReceiverId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_AspNetUsers_P2PCall_CallReceiverId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Calls_P2Ps_P2PId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FOS_FosId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_StudentId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_TeacherId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_P2Ps_P2pId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_FOS_FOS_ParentFosId",
                table: "FOS");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserBiggerId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserSmallerId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_LastActionById",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Notebooks_AspNetUsers_CreatedById",
                table: "Notebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ForApplicationUserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_FromApplicationUserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_P2Ps_P2pId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_P2PMessages_P2pMessageId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PBookmarks_AspNetUsers_ApplicationUserId",
                table: "P2PBookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PBookmarks_P2Ps_P2pId",
                table: "P2PBookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PFiles_Blobs_FileBlobId",
                table: "P2PFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PFiles_P2Ps_P2pId",
                table: "P2PFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PImages_Blobs_ImageBlobId",
                table: "P2PImages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PImages_P2Ps_P2pId",
                table: "P2PImages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PLanguages_Languages_LanguageId",
                table: "P2PLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PLanguages_P2Ps_P2pId",
                table: "P2PLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PMessages_AspNetUsers_MessageFromId",
                table: "P2PMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PMessages_AspNetUsers_MessageToId",
                table: "P2PMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PMessages_P2PMessages_OfferAcceptedId",
                table: "P2PMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2PMessages_P2Ps_P2pId",
                table: "P2PMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_P2Ps_AspNetUsers_CreatedById",
                table: "P2Ps");

            migrationBuilder.DropForeignKey(
                name: "FK_P2Ps_FOS_FosId",
                table: "P2Ps");

            migrationBuilder.DropForeignKey(
                name: "FK_P2Ps_AspNetUsers_ScheduledWithId",
                table: "P2Ps");

            migrationBuilder.DropForeignKey(
                name: "FK_PlatformFeedbacks_AspNetUsers_SubmittedById",
                table: "PlatformFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_StatesCountryId",
                table: "States");

            migrationBuilder.DropForeignKey(
                name: "FK_StickyNotes_AspNetUsers_CreatedById",
                table: "StickyNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_AspNetUsers_ApplicationUserId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_AspNetUsers_ApplicationUserId",
                table: "UserCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_Blobs_ImageBlobBlobId",
                table: "UserCertificates");

            migrationBuilder.DropIndex(
                name: "IX_Calls_CallReceiverId",
                table: "Calls");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "P2Ps");

            migrationBuilder.DropColumn(
                name: "CallReceiverId",
                table: "Calls");

            migrationBuilder.RenameColumn(
                name: "P2PCall_CallReceiverId",
                table: "Calls",
                newName: "CallReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Calls_P2PCall_CallReceiverId",
                table: "Calls",
                newName: "IX_Calls_CallReceiverId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_AspNetUsers_ApplicationUserId",
                table: "AccountTransactions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Blobs_ImageBlobId",
                table: "Achievements",
                column: "ImageBlobId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserInterests_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserInterests",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserInterests_FOS_FosId",
                table: "ApplicationUserInterests",
                column: "FosId",
                principalTable: "FOS",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserLanguages_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserLanguages",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserLanguages_Languages_LanguageId",
                table: "ApplicationUserLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserReferrals_AspNetUsers_NewRegistredUserId",
                table: "ApplicationUserReferrals",
                column: "NewRegistredUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserReferrals_AspNetUsers_ReferralUserId",
                table: "ApplicationUserReferrals",
                column: "ReferralUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRewards_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserRewards",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRewards_Rewards_RewardId",
                table: "ApplicationUserRewards",
                column: "RewardId",
                principalTable: "Rewards",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "GeoLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Languages_MotherTongueId",
                table: "AspNetUsers",
                column: "MotherTongueId",
                principalTable: "Languages",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Blobs_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "States",
                principalColumn: "GeoLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blobs_AspNetUsers_UploadedById",
                table: "Blobs",
                column: "UploadedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_AspNetUsers_CallerId",
                table: "Calls",
                column: "CallerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_AspNetUsers_CallReceiverId",
                table: "Calls",
                column: "CallReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_P2Ps_P2PId",
                table: "Calls",
                column: "P2PId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FOS_FosId",
                table: "Feedbacks",
                column: "FosId",
                principalTable: "FOS",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_StudentId",
                table: "Feedbacks",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_TeacherId",
                table: "Feedbacks",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_P2Ps_P2pId",
                table: "Feedbacks",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FOS_FOS_ParentFosId",
                table: "FOS",
                column: "ParentFosId",
                principalTable: "FOS",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserBiggerId",
                table: "Friendships",
                column: "ApplicationUserBiggerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserSmallerId",
                table: "Friendships",
                column: "ApplicationUserSmallerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_LastActionById",
                table: "Friendships",
                column: "LastActionById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notebooks_AspNetUsers_CreatedById",
                table: "Notebooks",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ForApplicationUserId",
                table: "Notifications",
                column: "ForApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_FromApplicationUserId",
                table: "Notifications",
                column: "FromApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_P2Ps_P2pId",
                table: "Notifications",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_P2PMessages_P2pMessageId",
                table: "Notifications",
                column: "P2pMessageId",
                principalTable: "P2PMessages",
                principalColumn: "P2pMessageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PBookmarks_AspNetUsers_ApplicationUserId",
                table: "P2PBookmarks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PBookmarks_P2Ps_P2pId",
                table: "P2PBookmarks",
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
                name: "FK_P2PFiles_P2Ps_P2pId",
                table: "P2PFiles",
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
                name: "FK_P2PImages_P2Ps_P2pId",
                table: "P2PImages",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PLanguages_Languages_LanguageId",
                table: "P2PLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PLanguages_P2Ps_P2pId",
                table: "P2PLanguages",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PMessages_AspNetUsers_MessageFromId",
                table: "P2PMessages",
                column: "MessageFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PMessages_AspNetUsers_MessageToId",
                table: "P2PMessages",
                column: "MessageToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PMessages_P2PMessages_OfferAcceptedId",
                table: "P2PMessages",
                column: "OfferAcceptedId",
                principalTable: "P2PMessages",
                principalColumn: "P2pMessageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2PMessages_P2Ps_P2pId",
                table: "P2PMessages",
                column: "P2pId",
                principalTable: "P2Ps",
                principalColumn: "P2pId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2Ps_AspNetUsers_CreatedById",
                table: "P2Ps",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2Ps_FOS_FosId",
                table: "P2Ps",
                column: "FosId",
                principalTable: "FOS",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2Ps_AspNetUsers_ScheduledWithId",
                table: "P2Ps",
                column: "ScheduledWithId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformFeedbacks_AspNetUsers_SubmittedById",
                table: "PlatformFeedbacks",
                column: "SubmittedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_States_Countries_StatesCountryId",
                table: "States",
                column: "StatesCountryId",
                principalTable: "Countries",
                principalColumn: "GeoLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StickyNotes_AspNetUsers_CreatedById",
                table: "StickyNotes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "CoreLookupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_AspNetUsers_ApplicationUserId",
                table: "UserAchievements",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_AspNetUsers_ApplicationUserId",
                table: "UserCertificates",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_Blobs_ImageBlobBlobId",
                table: "UserCertificates",
                column: "ImageBlobBlobId",
                principalTable: "Blobs",
                principalColumn: "BlobId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
