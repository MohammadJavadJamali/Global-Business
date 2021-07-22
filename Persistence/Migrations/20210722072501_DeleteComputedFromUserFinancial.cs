using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class DeleteComputedFromUserFinancial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c55f019-6eab-4bbd-935c-c444c0a4629b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e195af5-4a53-4467-b5fd-ae7da80eff04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e78955d2-bf04-4409-90ce-ea68de6f86ff");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "e78955d2-bf04-4409-90ce-ea68de6f86ff", "9eff2d46-2d91-4894-af89-aecd8e01cb65", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4e195af5-4a53-4467-b5fd-ae7da80eff04", "645c59c8-7169-4580-b564-437eee51fef2", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c55f019-6eab-4bbd-935c-c444c0a4629b", "567d3508-35b2-4d92-a904-7a1946514ba4", "Node", "NODE" });
        }
    }
}
