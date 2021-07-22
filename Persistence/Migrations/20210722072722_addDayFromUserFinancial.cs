using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class addDayFromUserFinancial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "607fd5ad-8726-46d3-993b-c698aa2bc056");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78fdfc90-d96e-4337-925c-648d3acf2e53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86b2a24c-a0ad-43d3-924b-95092be113df");

            migrationBuilder.AddColumn<int>(
                name: "DayCount",
                table: "UserFinancialPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bdaea417-ba87-4b4f-8c6d-c19c76ce2f9e", "01baee7c-387f-4d23-a740-846458ee0a24", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "059e2049-7324-4da9-bc27-c1acc6af1f74", "7a9f42fd-cb89-442e-a835-f56170524ee4", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9259ac5-6117-4cd3-b080-cd94448e17fa", "a5f58566-4f36-43d4-abcd-e90480947072", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "059e2049-7324-4da9-bc27-c1acc6af1f74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9259ac5-6117-4cd3-b080-cd94448e17fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdaea417-ba87-4b4f-8c6d-c19c76ce2f9e");

            migrationBuilder.DropColumn(
                name: "DayCount",
                table: "UserFinancialPackages");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "607fd5ad-8726-46d3-993b-c698aa2bc056", "56f7b4d8-7862-4707-9271-e5d99dadc510", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "78fdfc90-d96e-4337-925c-648d3acf2e53", "ca6c7d59-35fe-445d-ab36-56dc76b986f9", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "86b2a24c-a0ad-43d3-924b-95092be113df", "4fd5cf21-828f-4b97-9c52-ba9a30411a96", "Node", "NODE" });
        }
    }
}
