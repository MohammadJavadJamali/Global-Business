using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class IdUserFinancial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc4b9810-58de-4d1b-b8b0-88edee4fa9e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3146779-ae63-4bc9-9efe-254a984b7d06");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3dfeea3-aeaf-40d4-93f1-026739f63f63", "c3d82003-8c52-4569-9d47-6ad79ae4d80d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "61c76f61-273b-4e10-b1a2-8887c938efce", "36fa6c97-8561-4816-946d-95aba5d69c76", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61c76f61-273b-4e10-b1a2-8887c938efce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3dfeea3-aeaf-40d4-93f1-026739f63f63");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3146779-ae63-4bc9-9efe-254a984b7d06", "5b54572d-105f-40dd-a0e0-6ec5a78e6131", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc4b9810-58de-4d1b-b8b0-88edee4fa9e9", "4436db51-99ae-4393-ae9b-16c795a9ae1a", "Customer", "CUSTOMER" });
        }
    }
}
