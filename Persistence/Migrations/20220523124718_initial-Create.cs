using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39a64711-d2a2-46cd-8cf5-c38b1d167428");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76c7c433-8bec-4ba4-bcef-e886f4f63c49");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a042e527-02e6-4c47-9a9c-88d2bd32fa08");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "541df233-3108-4e1a-8b35-ef114ad6b3b4", "ae41fb26-73cf-4485-bdc6-24453243f143", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7d167ca0-283e-4c68-9d1e-1f73fcf27c72", "a7a039df-e242-435c-95fe-e17a97d76eb4", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1ce1093-1ec2-4f7e-9c84-f2302ea7a82d", "7d134bd8-f442-47c4-9946-13644e309bd4", "Node", "NODE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "541df233-3108-4e1a-8b35-ef114ad6b3b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d167ca0-283e-4c68-9d1e-1f73fcf27c72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1ce1093-1ec2-4f7e-9c84-f2302ea7a82d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "39a64711-d2a2-46cd-8cf5-c38b1d167428", "56609b33-7883-4661-ac08-7a890ee7436f", "Node", "NODE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "76c7c433-8bec-4ba4-bcef-e886f4f63c49", "9480fece-9ea2-4dc5-ae40-1c1f9d117590", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a042e527-02e6-4c47-9a9c-88d2bd32fa08", "5a4bd9c6-5d2c-4dd4-9a3b-e49ed05bf56b", "Customer", "CUSTOMER" });
        }
    }
}
