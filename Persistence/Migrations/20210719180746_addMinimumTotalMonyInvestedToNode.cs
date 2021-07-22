using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class addMinimumTotalMonyInvestedToNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "123b8695-b43c-49b8-842e-a7f0eab8d657");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13393521-4647-4f2c-b13d-f4cd7d0bf7af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba354d1e-52cd-4b02-a747-2d54bf9638a7");

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumSubBrachInvested",
                table: "Nodes",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "763598ab-2922-42dd-8726-352aa45d9041", "a6d6c26e-63e3-4589-8914-d2b02964eb7e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c37b8f22-2874-4fa6-802c-b10eb7072381", "8cf857d0-a241-4f6b-b0be-b2a9484c9f02", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ec8a31fc-ab1c-42b8-87e6-d42d72bd53fb", "6d66aff5-3d71-4140-8f92-a6f22a06d505", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "763598ab-2922-42dd-8726-352aa45d9041");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c37b8f22-2874-4fa6-802c-b10eb7072381");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec8a31fc-ab1c-42b8-87e6-d42d72bd53fb");

            migrationBuilder.DropColumn(
                name: "MinimumSubBrachInvested",
                table: "Nodes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "123b8695-b43c-49b8-842e-a7f0eab8d657", "d794c723-fabb-4672-9a74-a316dca819a9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba354d1e-52cd-4b02-a747-2d54bf9638a7", "7cd5562e-f1ed-421a-80d0-57f95ac507b2", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13393521-4647-4f2c-b13d-f4cd7d0bf7af", "f9c93b79-c0bd-4af8-a327-372f3a7a50c1", "Node", "NODE" });
        }
    }
}
