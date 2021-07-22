using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class addIntrodictionCodeToNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12517683-f0b3-48ce-aa7e-b4256ddc0649");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38c45ef0-3ecf-4e97-b5f1-f2077d1ae68b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5646dc21-71f1-4ae5-9b90-f24b79baf477");

            migrationBuilder.AddColumn<string>(
                name: "IntroductionCode",
                table: "Nodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "123b8695-b43c-49b8-842e-a7f0eab8d657", "d794c723-fabb-4672-9a74-a316dca819a9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba354d1e-52cd-4b02-a747-2d54bf9638a7", "7cd5562e-f1ed-421a-80d0-57f95ac507b2", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13393521-4647-4f2c-b13d-f4cd7d0bf7af", "f9c93b79-c0bd-4af8-a327-372f3a7a50c1", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "123b8695-b43c-49b8-842e-a7f0eab8d657");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13393521-4647-4f2c-b13d-f4cd7d0bf7af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba354d1e-52cd-4b02-a747-2d54bf9638a7");

            migrationBuilder.DropColumn(
                name: "IntroductionCode",
                table: "Nodes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "38c45ef0-3ecf-4e97-b5f1-f2077d1ae68b", "b9ccf855-a233-4277-ad1a-04f9f4c51e6e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5646dc21-71f1-4ae5-9b90-f24b79baf477", "e79923b9-ab0c-4074-b68f-add84b5e9394", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12517683-f0b3-48ce-aa7e-b4256ddc0649", "e76fcc1b-dab1-4640-83ce-5eb239751962", "Node", "NODE" });
        }
    }
}
