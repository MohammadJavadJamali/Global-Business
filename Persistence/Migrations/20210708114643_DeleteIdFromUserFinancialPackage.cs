using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class DeleteIdFromUserFinancialPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFinancialPackages_AspNetUsers_UserId",
                table: "UserFinancialPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFinancialPackages",
                table: "UserFinancialPackages");

            migrationBuilder.DropIndex(
                name: "IX_UserFinancialPackages_UserId",
                table: "UserFinancialPackages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46a2eeb4-5f10-4184-b211-e806be79a7ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d453d54e-79b2-4294-847a-cff61a375e11");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserFinancialPackages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFinancialPackages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFinancialPackages",
                table: "UserFinancialPackages",
                columns: new[] { "UserId", "FinancialPackageId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5e17001c-33c3-41c3-bb53-30c1f4a582c1", "b1c4653b-bb2b-4fe3-a20a-478137450a76", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19578e70-06a0-4c5b-9197-7852cea37d98", "b5b3c058-3587-429e-aa8e-fe566a211602", "Customer", "CUSTOMER" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFinancialPackages_AspNetUsers_UserId",
                table: "UserFinancialPackages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFinancialPackages_AspNetUsers_UserId",
                table: "UserFinancialPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFinancialPackages",
                table: "UserFinancialPackages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19578e70-06a0-4c5b-9197-7852cea37d98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e17001c-33c3-41c3-bb53-30c1f4a582c1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFinancialPackages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserFinancialPackages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFinancialPackages",
                table: "UserFinancialPackages",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "46a2eeb4-5f10-4184-b211-e806be79a7ed", "a3d04069-a048-47c0-8ec5-50e3a78259f2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d453d54e-79b2-4294-847a-cff61a375e11", "263fa1e9-84e3-4555-aaa2-8a9a29c3a3b3", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFinancialPackages_UserId",
                table: "UserFinancialPackages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFinancialPackages_AspNetUsers_UserId",
                table: "UserFinancialPackages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
