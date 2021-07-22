using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class AddSomeFieldsToNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "TotalMoneyInvestedBySubsets",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMoneyInvestedBySubsets",
                table: "Nodes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "79ad4b24-ddab-4631-ae0b-63dd191b60df", "3e6ad8a6-cf78-4c1f-99be-7e8cdc8f4ede", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5a33ab00-d0e5-4bb9-9b4b-91231a7868ff", "d4e9a551-9a18-4efa-8ffb-14a7ced8a498", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51c979f1-f0f9-40f7-bee4-e39a063fbf2c", "54b9d5a2-7aef-45e8-8ebc-c7bbe1da38a6", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51c979f1-f0f9-40f7-bee4-e39a063fbf2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a33ab00-d0e5-4bb9-9b4b-91231a7868ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79ad4b24-ddab-4631-ae0b-63dd191b60df");

            migrationBuilder.DropColumn(
                name: "TotalMoneyInvestedBySubsets",
                table: "Nodes");

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
    }
}
