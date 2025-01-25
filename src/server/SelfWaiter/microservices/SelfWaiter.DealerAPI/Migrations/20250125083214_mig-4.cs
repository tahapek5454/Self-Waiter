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
            migrationBuilder.CreateIndex(
                name: "IX_Districts_IsValid",
                table: "Districts",
                column: "IsValid");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Name_IsValid",
                table: "Districts",
                columns: new[] { "Name", "IsValid" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Districts_IsValid",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Districts_Name_IsValid",
                table: "Districts");
        }
    }
}
