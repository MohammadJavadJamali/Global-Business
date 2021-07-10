using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class noKeyUserFinancial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19578e70-06a0-4c5b-9197-7852cea37d98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e17001c-33c3-41c3-bb53-30c1f4a582c1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "476e7b79-fd84-4447-9d5c-f6fb8598759b", "16d3a608-d781-4d04-9d37-ac492861f8cb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b8d760eb-545a-4a8c-9bdd-20622c7eea2e", "c4675cdb-a2d0-4a52-9aff-141cc96acf83", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "476e7b79-fd84-4447-9d5c-f6fb8598759b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8d760eb-545a-4a8c-9bdd-20622c7eea2e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5e17001c-33c3-41c3-bb53-30c1f4a582c1", "b1c4653b-bb2b-4fe3-a20a-478137450a76", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19578e70-06a0-4c5b-9197-7852cea37d98", "b5b3c058-3587-429e-aa8e-fe566a211602", "Customer", "CUSTOMER" });
        }
    }
}
