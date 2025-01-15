using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfWaiter.DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "UserProfileAndDealers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "UserProfile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Districts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Dealers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Countries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7333d5e7-ae6a-4523-88ac-7383c9a9f6a5"),
                column: "IsValid",
                value: true);

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("24818128-6c4e-453d-8a1f-15f75e8aa746"),
                column: "IsValid",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "UserProfileAndDealers");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Cities");
        }
    }
}
