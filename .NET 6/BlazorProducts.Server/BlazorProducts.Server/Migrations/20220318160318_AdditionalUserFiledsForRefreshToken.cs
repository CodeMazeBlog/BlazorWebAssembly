using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorProducts.Server.Migrations
{
    public partial class AdditionalUserFiledsForRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46518814-30c2-4201-a2e4-cd238cfad862");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6950baa6-9d92-4fad-a2bb-9391a529f127");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9793f966-ff14-4bc8-964e-da41fecdcf71", "16252183-c888-454f-8b92-7d2fe6de2e39", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2522725-6fc2-4992-a805-482b461951b1", "c3bc5d4f-15a1-452b-9d84-96a9ad686082", "Viewer", "VIEWER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9793f966-ff14-4bc8-964e-da41fecdcf71");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2522725-6fc2-4992-a805-482b461951b1");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "46518814-30c2-4201-a2e4-cd238cfad862", "c4dd415f-a138-4ce9-b296-8f863e936f70", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6950baa6-9d92-4fad-a2bb-9391a529f127", "7eccd4af-b629-4da3-9a95-0a6e98afb944", "Administrator", "ADMINISTRATOR" });
        }
    }
}
