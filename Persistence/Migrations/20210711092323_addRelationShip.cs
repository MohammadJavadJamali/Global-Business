using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class addRelationShip : Migration
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
                keyValue: "25299b09-a5e9-46cc-8f91-d248d54c200d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce577e5b-e8af-4665-8c94-14b9724b780e");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserFinancialPackages");

            migrationBuilder.DropColumn(
                name: "HaveFinancialPackage",
                table: "AspNetUsers");

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
                values: new object[] { "1d08c455-d564-4141-9665-b2f1378dd742", "2f5e7666-b5d0-434c-87d2-ad4a9b167d97", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34a46d18-050c-4909-8e0f-313a9804b467", "51a61552-4126-464e-bcec-b0fa96a3a83c", "Customer", "CUSTOMER" });

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
                keyValue: "1d08c455-d564-4141-9665-b2f1378dd742");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34a46d18-050c-4909-8e0f-313a9804b467");

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

            migrationBuilder.AddColumn<bool>(
                name: "HaveFinancialPackage",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFinancialPackages",
                table: "UserFinancialPackages",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "25299b09-a5e9-46cc-8f91-d248d54c200d", "6828c8d1-2e0a-4a23-95b9-7a3d2ae1e434", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce577e5b-e8af-4665-8c94-14b9724b780e", "3e28d6a7-e3c6-4f98-8e91-33154087e666", "Customer", "CUSTOMER" });

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
