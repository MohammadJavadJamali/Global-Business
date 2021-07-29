using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class iscalculate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsCalculate",
                table: "Nodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe7a5a38-61db-4ac8-9266-dfe4cb56a411", "eeec81f4-22b4-4bed-b592-68e465e0dc2c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e8861eab-0539-4d1f-8a98-58388334edc4", "b6896ed6-e318-408d-9ea4-9f915ea1dbd6", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc1e2650-eb67-4e5f-b5a5-c96fa5bc0ec8", "ede9ddf0-e11f-4936-aa51-a6a533bc4d9a", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc1e2650-eb67-4e5f-b5a5-c96fa5bc0ec8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8861eab-0539-4d1f-8a98-58388334edc4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe7a5a38-61db-4ac8-9266-dfe4cb56a411");

            migrationBuilder.DropColumn(
                name: "IsCalculate",
                table: "Nodes");

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
    }
}
