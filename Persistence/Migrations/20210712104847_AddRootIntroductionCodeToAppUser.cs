using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddRootIntroductionCodeToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb16d812-df12-4005-ad05-6f15415ad06c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eee619ab-65db-4617-8ba6-c5934824a095");

            migrationBuilder.AddColumn<string>(
                name: "RootIntroductionCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "653f6eff-5107-4e6f-800e-407274a55bc5", "9c489ede-f28b-4f41-811f-f8b468150a64", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "de78d81f-66bb-4ab7-8c09-48c09f1fe577", "439fca93-1a93-4f28-b31b-3b5d1dcad3ab", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "653f6eff-5107-4e6f-800e-407274a55bc5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de78d81f-66bb-4ab7-8c09-48c09f1fe577");

            migrationBuilder.DropColumn(
                name: "RootIntroductionCode",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb16d812-df12-4005-ad05-6f15415ad06c", "badf4aaa-5ac9-43c0-be3a-8b4003d40e1f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eee619ab-65db-4617-8ba6-c5934824a095", "caa7c8d2-e08c-4284-b4cb-3f114142ac55", "Customer", "CUSTOMER" });
        }
    }
}
