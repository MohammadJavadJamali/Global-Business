using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class iscalculate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "517a49eb-091b-4a5b-99f9-5bed227b6381");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "594c37a0-5016-4fc3-9c5d-0ca357f7d666");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c904fd6-1493-4040-96c9-f0e474ea2815");

            migrationBuilder.AddColumn<bool>(
                name: "IsCalculate",
                table: "Nodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsCalculate",
                table: "Nodes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "594c37a0-5016-4fc3-9c5d-0ca357f7d666", "57b304f6-0bfa-408f-8465-c845c5bd6eaa", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9c904fd6-1493-4040-96c9-f0e474ea2815", "9481fc6f-0cd5-4f6a-a3c4-3f41126dad39", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "517a49eb-091b-4a5b-99f9-5bed227b6381", "003bae17-4256-4e07-be20-22898920ec03", "Node", "NODE" });
        }
    }
}
