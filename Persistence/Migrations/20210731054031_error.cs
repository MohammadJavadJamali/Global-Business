using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class error : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14d05215-4db6-451e-bbbb-7efa51d94333");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d136264b-cd20-4f09-96c0-17d37ef57883");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e47f3403-c6d3-4c75-9500-c1f993a4b59c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a6cf716-08b3-40d4-a415-e11c6519ca9a", "474e3298-1734-4c19-81f3-45bb45ad35c4", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6f8956b-50a1-42d5-b0a1-4f009a4e4b92", "d4ed1170-783b-4894-beb7-67b5a02dbd97", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae13e951-a476-45e5-843a-3fabbf2fe54a", "c7e36aeb-c40a-4a04-91b2-b3f949d88104", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a6cf716-08b3-40d4-a415-e11c6519ca9a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6f8956b-50a1-42d5-b0a1-4f009a4e4b92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae13e951-a476-45e5-843a-3fabbf2fe54a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e47f3403-c6d3-4c75-9500-c1f993a4b59c", "6a8b30c9-5d5c-4709-bdb7-fccd648ca16d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14d05215-4db6-451e-bbbb-7efa51d94333", "b8cc2162-1116-433c-bcf4-3546cc67236d", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d136264b-cd20-4f09-96c0-17d37ef57883", "22817626-06fb-496c-8d7b-01304ca02469", "Node", "NODE" });
        }
    }
}
