using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfWaiter.DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorUserId",
                table: "Dealers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_CreatorUserId",
                table: "Dealers",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dealers_UserProfile_CreatorUserId",
                table: "Dealers",
                column: "CreatorUserId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealers_UserProfile_CreatorUserId",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Dealers_CreatorUserId",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Dealers");
        }
    }
}
