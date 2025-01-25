using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfWaiter.DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Districts_IsValid",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Districts_Name_IsValid",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Countries_IsValid",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Name_IsValid",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Cities_IsValid",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_IsValid",
                table: "Cities");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_IsValid",
                table: "Districts",
                column: "IsValid",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Name_IsValid",
                table: "Districts",
                columns: new[] { "Name", "IsValid" },
                unique: true,
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_IsValid",
                table: "Countries",
                column: "IsValid",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name_IsValid",
                table: "Countries",
                columns: new[] { "Name", "IsValid" },
                unique: true,
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IsValid",
                table: "Cities",
                column: "IsValid",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_IsValid",
                table: "Cities",
                columns: new[] { "Name", "IsValid" },
                unique: true,
                descending: new bool[0]);
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

            migrationBuilder.DropIndex(
                name: "IX_Countries_IsValid",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Name_IsValid",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Cities_IsValid",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_IsValid",
                table: "Cities");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_IsValid",
                table: "Districts",
                column: "IsValid");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Name_IsValid",
                table: "Districts",
                columns: new[] { "Name", "IsValid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_IsValid",
                table: "Countries",
                column: "IsValid");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name_IsValid",
                table: "Countries",
                columns: new[] { "Name", "IsValid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IsValid",
                table: "Cities",
                column: "IsValid");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_IsValid",
                table: "Cities",
                columns: new[] { "Name", "IsValid" },
                unique: true);
        }
    }
}
