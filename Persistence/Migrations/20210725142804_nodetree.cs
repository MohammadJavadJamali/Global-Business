using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class nodetree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99f313ab-a5b4-44fb-aad7-808a91f3f11c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfb2837a-d59f-4e51-9c9a-d223d6461018");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7a2b9a6-ca66-4f69-8913-4113a3f1a2b0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "594c37a0-5016-4fc3-9c5d-0ca357f7d666", "57b304f6-0bfa-408f-8465-c845c5bd6eaa", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9c904fd6-1493-4040-96c9-f0e474ea2815", "9481fc6f-0cd5-4f6a-a3c4-3f41126dad39", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "517a49eb-091b-4a5b-99f9-5bed227b6381", "003bae17-4256-4e07-be20-22898920ec03", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "517a49eb-091b-4a5b-99f9-5bed227b6381");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "594c37a0-5016-4fc3-9c5d-0ca357f7d666");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c904fd6-1493-4040-96c9-f0e474ea2815");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7a2b9a6-ca66-4f69-8913-4113a3f1a2b0", "14b75501-7665-42ce-924a-665394f783e1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99f313ab-a5b4-44fb-aad7-808a91f3f11c", "b7609780-6cfd-4b06-b534-27a42f77a19e", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bfb2837a-d59f-4e51-9c9a-d223d6461018", "b4564177-3c0a-4d48-82a7-641c2eb1944e", "Node", "NODE" });
        }
    }
}
