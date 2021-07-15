using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class addNodeTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "716d2d4c-84b2-4dfd-9608-825046ab23bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb42867d-2bf7-4688-9efd-94c26b6804be");

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeftUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0aca8412-1b2e-4abf-a26b-8b8c5f884163", "93572256-b4b5-4e3a-a7b1-d17bd8c1de11", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc6eb8d0-172f-4d08-994a-f479fa04bceb", "90f5151f-be27-47d0-98fb-93f31a7af3ec", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aca8412-1b2e-4abf-a26b-8b8c5f884163");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc6eb8d0-172f-4d08-994a-f479fa04bceb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "716d2d4c-84b2-4dfd-9608-825046ab23bc", "22ce891e-08f1-4e5d-beda-bf238244f4d5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb42867d-2bf7-4688-9efd-94c26b6804be", "14a693dd-5a60-465e-b6bc-244e391ce808", "Customer", "CUSTOMER" });
        }
    }
}
