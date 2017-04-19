using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Knowlead.DAL.Migrations
{
    public partial class AddBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationUserRewards",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationUserReferrals",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationUserLanguages",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ApplicationUserInterests",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AccountTransactions",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql("UPDATE AccountTransactions SET CreatedAt = Timestamp");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PlatformFeedbacks",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "P2PMessages",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql("UPDATE P2PMessages SET CreatedAt = Timestamp");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "P2PLanguages",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "P2PImages",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "P2PFiles",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "P2PBookmarks",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "P2Ps",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql("UPDATE P2Ps SET CreatedAt = DateCreated");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedbacks",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Calls",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Blobs",
                nullable: false,
                defaultValue: new DateTime(2017, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ApplicationUserRewards");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ApplicationUserReferrals");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ApplicationUserLanguages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ApplicationUserInterests");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PlatformFeedbacks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "P2PMessages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "P2PLanguages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "P2PImages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "P2PFiles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "P2PBookmarks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "P2Ps");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Calls");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Blobs");
        }
    }
}
