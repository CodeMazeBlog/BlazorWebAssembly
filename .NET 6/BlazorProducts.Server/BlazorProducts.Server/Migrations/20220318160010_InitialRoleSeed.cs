using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorProducts.Server.Migrations
{
    public partial class InitialRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "46518814-30c2-4201-a2e4-cd238cfad862", "c4dd415f-a138-4ce9-b296-8f863e936f70", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6950baa6-9d92-4fad-a2bb-9391a529f127", "7eccd4af-b629-4da3-9a95-0a6e98afb944", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46518814-30c2-4201-a2e4-cd238cfad862");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6950baa6-9d92-4fad-a2bb-9391a529f127");
        }
    }
}
