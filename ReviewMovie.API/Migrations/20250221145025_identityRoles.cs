using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReviewMovie.API.Migrations
{
    /// <inheritdoc />
    public partial class identityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "285547ad-94d0-4f44-86a0-00e559d46207", null, "Administrator", "ADMINISTRATOR" },
                    { "d3e6be60-f4e0-43ef-9408-9cb862cb8665", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "285547ad-94d0-4f44-86a0-00e559d46207");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3e6be60-f4e0-43ef-9408-9cb862cb8665");
        }
    }
}
