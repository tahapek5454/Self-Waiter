using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfWaiter.DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserProfile",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "UserProfile",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserProfile",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "UserProfile",
                columns: new[] { "Id", "CreatedDate", "CreatorUserName", "Email", "ImageUrl", "IsValid", "Name", "PhoneNumber", "Surname", "UpdatedDate", "UpdatetorUserName", "UserName" },
                values: new object[] { new Guid("2d9b274f-753f-4aab-947d-cbf9d232b811"), new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "taha.pek", "taha_test@gmail.com", null, true, "Taha", null, "Pek", null, null, "taha.pek" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_Email",
                table: "UserProfile",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_IsValid",
                table: "UserProfile",
                column: "IsValid",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_PhoneNumber",
                table: "UserProfile",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserName",
                table: "UserProfile",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfile_Email",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_IsValid",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_PhoneNumber",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_UserName",
                table: "UserProfile");

            migrationBuilder.DeleteData(
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: new Guid("2d9b274f-753f-4aab-947d-cbf9d232b811"));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserProfile",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);
        }
    }
}
