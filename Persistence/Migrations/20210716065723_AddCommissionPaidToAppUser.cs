using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class AddCommissionPaidToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7af1ad0c-358f-4279-8030-c25332263ef6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a116615-9a8a-447d-b772-708d482da3d6");

            migrationBuilder.AddColumn<bool>(
                name: "CommissionPaid",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30cb9d42-d783-468b-8a92-1f1cf9149453", "4eb3008d-97b1-4f19-971e-c3c6980c8f6b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "610c6111-ae94-4427-9c0f-da707ec0c02f", "7fe6a424-f24f-4104-8472-424975b0e77c", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30cb9d42-d783-468b-8a92-1f1cf9149453");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "610c6111-ae94-4427-9c0f-da707ec0c02f");

            migrationBuilder.DropColumn(
                name: "CommissionPaid",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a116615-9a8a-447d-b772-708d482da3d6", "82d393cc-f4de-4efd-9d3a-47803f274f0e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7af1ad0c-358f-4279-8030-c25332263ef6", "41a88110-214f-4811-ad62-d2c86d4e9798", "Customer", "CUSTOMER" });
        }
    }
}
