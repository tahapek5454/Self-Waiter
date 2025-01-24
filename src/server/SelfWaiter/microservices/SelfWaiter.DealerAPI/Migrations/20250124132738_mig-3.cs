using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfWaiter.DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Countries_IsValid",
                table: "Countries",
                column: "IsValid");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IsValid",
                table: "Cities",
                column: "IsValid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Countries_IsValid",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Cities_IsValid",
                table: "Cities");
        }
    }
}
