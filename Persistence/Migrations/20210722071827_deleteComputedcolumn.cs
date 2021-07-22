using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class deleteComputedcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "DayCount",
                table: "UserFinancialPackages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "DATEDIFF(Day, ChoicePackageDate, EndFinancialPackageDate)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e78955d2-bf04-4409-90ce-ea68de6f86ff", "9eff2d46-2d91-4894-af89-aecd8e01cb65", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4e195af5-4a53-4467-b5fd-ae7da80eff04", "645c59c8-7169-4580-b564-437eee51fef2", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c55f019-6eab-4bbd-935c-c444c0a4629b", "567d3508-35b2-4d92-a904-7a1946514ba4", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c55f019-6eab-4bbd-935c-c444c0a4629b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e195af5-4a53-4467-b5fd-ae7da80eff04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e78955d2-bf04-4409-90ce-ea68de6f86ff");

            migrationBuilder.AlterColumn<int>(
                name: "DayCount",
                table: "UserFinancialPackages",
                type: "int",
                nullable: false,
                computedColumnSql: "DATEDIFF(Day, ChoicePackageDate, EndFinancialPackageDate)",
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
