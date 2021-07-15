using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class TotalMonyInFinancialPackagesInAppUsre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "277f51e4-828d-4ce0-bab6-b5453d4a60d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5eac9564-2481-4570-a0e6-dc18dea2458e");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMonyInFinancialPackages",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aea9d212-24fc-4204-879d-9ba020f5fcdd", "ca1ffc54-b416-4b06-a96f-dc58a622580b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d98a13c1-de12-47d6-812e-dcae9dccb07d", "470306bb-8d69-43bd-9525-5f5bfb8ca392", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aea9d212-24fc-4204-879d-9ba020f5fcdd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d98a13c1-de12-47d6-812e-dcae9dccb07d");

            migrationBuilder.DropColumn(
                name: "TotalMonyInFinancialPackages",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "277f51e4-828d-4ce0-bab6-b5453d4a60d6", "7bb4b62f-68dd-4ccd-9c08-98a6a4c3cb2a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5eac9564-2481-4570-a0e6-dc18dea2458e", "9aa575c8-63f3-41a9-9930-c5b40f0bb845", "Customer", "CUSTOMER" });
        }
    }
}
