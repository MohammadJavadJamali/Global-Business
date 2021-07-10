using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class IdUserFinancial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: "476e7b79-fd84-4447-9d5c-f6fb8598759b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8d760eb-545a-4a8c-9bdd-20622c7eea2e");

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
                values: new object[] { "d3146779-ae63-4bc9-9efe-254a984b7d06", "5b54572d-105f-40dd-a0e0-6ec5a78e6131", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc4b9810-58de-4d1b-b8b0-88edee4fa9e9", "4436db51-99ae-4393-ae9b-16c795a9ae1a", "Customer", "CUSTOMER" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: "bc4b9810-58de-4d1b-b8b0-88edee4fa9e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3146779-ae63-4bc9-9efe-254a984b7d06");

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
                values: new object[] { "476e7b79-fd84-4447-9d5c-f6fb8598759b", "16d3a608-d781-4d04-9d37-ac492861f8cb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b8d760eb-545a-4a8c-9bdd-20622c7eea2e", "c4675cdb-a2d0-4a52-9aff-141cc96acf83", "Customer", "CUSTOMER" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFinancialPackages_AspNetUsers_UserId",
                table: "UserFinancialPackages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
