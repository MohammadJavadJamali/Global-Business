using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class AddParentIdToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "ParentId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff66c58d-f181-42c4-89f6-1b1378d4bf87", "93d22304-59ae-4fb5-b92e-c9e172768b0c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45bc3669-7aa6-447d-b92b-10321b92d68e", "043446d5-416b-421f-b30c-7f3bab4a1277", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45bc3669-7aa6-447d-b92b-10321b92d68e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff66c58d-f181-42c4-89f6-1b1378d4bf87");

            migrationBuilder.RenameColumn(
                name: "ParentId",
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
    }
}
