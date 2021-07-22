using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class userfinancialpackagesomchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitAmountPerDay",
                table: "UserFinancialPackages",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DayCount",
                table: "UserFinancialPackages",
                type: "int",
                nullable: false,
                computedColumnSql: "DATEDIFF(Day, ChoicePackageDate, EndFinancialPackageDate)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "398fe38c-05b0-4b39-9d1e-fc367e6399bf", "e507f6ad-24be-4e52-8629-0600bcc7c59c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2fbac6c-a1ca-4e20-9db7-590be3d7fb2e", "e98a9e1b-7871-4920-9302-5fe52bbf87fc", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e832441b-927f-4dec-a5d4-02e7926abb39", "7554aa0c-7d4d-4583-894e-92d0293b2595", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "398fe38c-05b0-4b39-9d1e-fc367e6399bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2fbac6c-a1ca-4e20-9db7-590be3d7fb2e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e832441b-927f-4dec-a5d4-02e7926abb39");

            migrationBuilder.DropColumn(
                name: "DayCount",
                table: "UserFinancialPackages");

            migrationBuilder.DropColumn(
                name: "ProfitAmountPerDay",
                table: "UserFinancialPackages");

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
    }
}
