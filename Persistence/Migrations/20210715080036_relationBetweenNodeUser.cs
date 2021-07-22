using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class relationBetweenNodeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aca8412-1b2e-4abf-a26b-8b8c5f884163");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc6eb8d0-172f-4d08-994a-f479fa04bceb");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Nodes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a116615-9a8a-447d-b772-708d482da3d6", "82d393cc-f4de-4efd-9d3a-47803f274f0e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7af1ad0c-358f-4279-8030-c25332263ef6", "41a88110-214f-4811-ad62-d2c86d4e9798", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_UserId",
                table: "Nodes",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_AspNetUsers_UserId",
                table: "Nodes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_AspNetUsers_UserId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_UserId",
                table: "Nodes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7af1ad0c-358f-4279-8030-c25332263ef6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a116615-9a8a-447d-b772-708d482da3d6");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Nodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0aca8412-1b2e-4abf-a26b-8b8c5f884163", "93572256-b4b5-4e3a-a7b1-d17bd8c1de11", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc6eb8d0-172f-4d08-994a-f479fa04bceb", "90f5151f-be27-47d0-98fb-93f31a7af3ec", "Customer", "CUSTOMER" });
        }
    }
}
