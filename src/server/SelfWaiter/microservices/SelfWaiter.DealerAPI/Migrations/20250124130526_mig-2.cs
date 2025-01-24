using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfWaiter.DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name_IsValid",
                table: "Countries",
                columns: new[] { "Name", "IsValid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_IsValid",
                table: "Cities",
                columns: new[] { "Name", "IsValid" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Countries_Name_IsValid",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_IsValid",
                table: "Cities");
        }
    }
}
