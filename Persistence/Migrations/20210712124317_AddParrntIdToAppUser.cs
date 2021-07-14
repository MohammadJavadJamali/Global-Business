using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddParrntIdToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54f46fd8-ccc6-4eb2-8b88-2846c4ce81d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bc778d5-45f8-4f0c-b71e-2471c7895f21");

            migrationBuilder.RenameColumn(
                name: "RootIntroductionCode",
                table: "AspNetUsers",
                newName: "ParrentId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "06ba2103-2505-4f45-be58-9e7f5a5dd570", "96700514-53ee-4378-ae10-26c06e92b8ba", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4eb04d49-bbba-4164-9a0a-328e3dba2468", "89863624-babd-4945-9a7e-8f46f965e73c", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06ba2103-2505-4f45-be58-9e7f5a5dd570");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4eb04d49-bbba-4164-9a0a-328e3dba2468");

            migrationBuilder.RenameColumn(
                name: "ParrentId",
                table: "AspNetUsers",
                newName: "RootIntroductionCode");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54f46fd8-ccc6-4eb2-8b88-2846c4ce81d6", "8510d7d8-1baa-4e61-9b39-b585e0c07b06", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7bc778d5-45f8-4f0c-b71e-2471c7895f21", "b6941682-e974-4486-ba9d-aa9bcaeeb294", "Customer", "CUSTOMER" });
        }
    }
}
