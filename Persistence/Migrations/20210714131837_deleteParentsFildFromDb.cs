using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class deleteParentsFildFromDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IntroductionCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TotalMonyInFinancialPackages",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TotalMonyInSubBrache",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc1a450f-e585-415f-8c2c-e45ae1692365", "6ddbb9f8-9377-4789-a076-9f27612f950c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5c31a703-2411-458d-87d3-035fc8a89699", "de31ca77-659e-4e2f-bbaf-c0d4e94a897d", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c31a703-2411-458d-87d3-035fc8a89699");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc1a450f-e585-415f-8c2c-e45ae1692365");

            migrationBuilder.AddColumn<string>(
                name: "IntroductionCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMonyInFinancialPackages",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMonyInSubBrache",
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
    }
}
