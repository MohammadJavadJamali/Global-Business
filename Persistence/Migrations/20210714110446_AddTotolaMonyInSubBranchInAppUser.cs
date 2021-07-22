using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class AddTotolaMonyInSubBranchInAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45bc3669-7aa6-447d-b92b-10321b92d68e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff66c58d-f181-42c4-89f6-1b1378d4bf87");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMonyInSubBrache",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "277f51e4-828d-4ce0-bab6-b5453d4a60d6", "7bb4b62f-68dd-4ccd-9c08-98a6a4c3cb2a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5eac9564-2481-4570-a0e6-dc18dea2458e", "9aa575c8-63f3-41a9-9930-c5b40f0bb845", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "277f51e4-828d-4ce0-bab6-b5453d4a60d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5eac9564-2481-4570-a0e6-dc18dea2458e");

            migrationBuilder.DropColumn(
                name: "TotalMonyInSubBrache",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff66c58d-f181-42c4-89f6-1b1378d4bf87", "93d22304-59ae-4fb5-b92e-c9e172768b0c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45bc3669-7aa6-447d-b92b-10321b92d68e", "043446d5-416b-421f-b30c-7f3bab4a1277", "Customer", "CUSTOMER" });
        }
    }
}
