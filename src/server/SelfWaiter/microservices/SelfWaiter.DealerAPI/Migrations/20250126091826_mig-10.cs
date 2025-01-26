using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfWaiter.DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserProfile",
                columns: new[] { "Id", "CreatedDate", "CreatorUserName", "Email", "ImageUrl", "IsValid", "Name", "PhoneNumber", "Surname", "UpdatedDate", "UpdatetorUserName", "UserName" },
                values: new object[] { new Guid("e0585510-5747-4c20-85ba-f2325deaedfd"), new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "taha.pek", "ahmet_test@gmail.com", null, true, "Ahmet Zeyt", null, "Sertel", null, null, "azeyt.sertel" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: new Guid("e0585510-5747-4c20-85ba-f2325deaedfd"));
        }
    }
}
