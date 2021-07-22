using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class aasas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30cb9d42-d783-468b-8a92-1f1cf9149453");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "610c6111-ae94-4427-9c0f-da707ec0c02f");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMoneyInvested",
                table: "Nodes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMoneyInvestedBySubsets",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da49f851-8cc0-4faf-bc4a-35489124d89c", "a7a72abb-62f8-49da-bdae-e5b39e87ad23", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1ac8eb5c-bdfc-4f3a-8cce-ba569ab15cc8", "a24c378f-5352-4401-86c2-09932ff8efe9", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3b03400-32da-4c25-9e9e-9015ced7e5ed", "b6e7150c-58ad-4e27-ae15-cb904587e623", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ac8eb5c-bdfc-4f3a-8cce-ba569ab15cc8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da49f851-8cc0-4faf-bc4a-35489124d89c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3b03400-32da-4c25-9e9e-9015ced7e5ed");

            migrationBuilder.DropColumn(
                name: "TotalMoneyInvested",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "TotalMoneyInvestedBySubsets",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30cb9d42-d783-468b-8a92-1f1cf9149453", "4eb3008d-97b1-4f19-971e-c3c6980c8f6b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "610c6111-ae94-4427-9c0f-da707ec0c02f", "7fe6a424-f24f-4104-8472-424975b0e77c", "Customer", "CUSTOMER" });
        }
    }
}
