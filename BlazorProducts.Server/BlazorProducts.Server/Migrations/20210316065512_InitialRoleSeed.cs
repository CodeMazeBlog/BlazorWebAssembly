using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorProducts.Server.Migrations
{
    public partial class InitialRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "912443f3-44dd-4b86-b579-be7ed236db7a", "caf4b2b6-26e5-42cc-b8a5-a3f48dae3d35", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9fb74770-f526-4cdb-8f87-744dedbe0400", "189f9e0d-bba7-44ab-b29b-64c870575453", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "912443f3-44dd-4b86-b579-be7ed236db7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fb74770-f526-4cdb-8f87-744dedbe0400");
        }
    }
}
