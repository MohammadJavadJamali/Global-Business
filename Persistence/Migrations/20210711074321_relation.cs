using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "25299b09-a5e9-46cc-8f91-d248d54c200d", "6828c8d1-2e0a-4a23-95b9-7a3d2ae1e434", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce577e5b-e8af-4665-8c94-14b9724b780e", "3e28d6a7-e3c6-4f98-8e91-33154087e666", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25299b09-a5e9-46cc-8f91-d248d54c200d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce577e5b-e8af-4665-8c94-14b9724b780e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3dfeea3-aeaf-40d4-93f1-026739f63f63", "c3d82003-8c52-4569-9d47-6ad79ae4d80d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "61c76f61-273b-4e10-b1a2-8887c938efce", "36fa6c97-8561-4816-946d-95aba5d69c76", "Customer", "CUSTOMER" });
        }
    }
}
