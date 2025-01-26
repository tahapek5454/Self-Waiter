using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfWaiter.DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfileAndDealers_DealerId",
                table: "UserProfileAndDealers");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileAndDealers_DealerId_UserProfileId_IsValid",
                table: "UserProfileAndDealers",
                columns: new[] { "DealerId", "UserProfileId", "IsValid" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfileAndDealers_DealerId_UserProfileId_IsValid",
                table: "UserProfileAndDealers");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileAndDealers_DealerId",
                table: "UserProfileAndDealers",
                column: "DealerId");
        }
    }
}
