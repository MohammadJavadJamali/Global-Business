using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class editUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profits_AspNetUsers_User_Id",
                table: "Profits");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_User_Id",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc1e2650-eb67-4e5f-b5a5-c96fa5bc0ec8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8861eab-0539-4d1f-8a98-58388334edc4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe7a5a38-61db-4ac8-9266-dfe4cb56a411");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Transactions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_User_Id",
                table: "Transactions",
                newName: "IX_Transactions_UserId");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Profits",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Profits_User_Id",
                table: "Profits",
                newName: "IX_Profits_UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "76c7c433-8bec-4ba4-bcef-e886f4f63c49", "9480fece-9ea2-4dc5-ae40-1c1f9d117590", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a042e527-02e6-4c47-9a9c-88d2bd32fa08", "5a4bd9c6-5d2c-4dd4-9a3b-e49ed05bf56b", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "39a64711-d2a2-46cd-8cf5-c38b1d167428", "56609b33-7883-4661-ac08-7a890ee7436f", "Node", "NODE" });

            migrationBuilder.AddForeignKey(
                name: "FK_Profits_AspNetUsers_UserId",
                table: "Profits",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profits_AspNetUsers_UserId",
                table: "Profits");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions");

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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transactions",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                newName: "IX_Transactions_User_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Profits",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Profits_UserId",
                table: "Profits",
                newName: "IX_Profits_User_Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe7a5a38-61db-4ac8-9266-dfe4cb56a411", "eeec81f4-22b4-4bed-b592-68e465e0dc2c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e8861eab-0539-4d1f-8a98-58388334edc4", "b6896ed6-e318-408d-9ea4-9f915ea1dbd6", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc1e2650-eb67-4e5f-b5a5-c96fa5bc0ec8", "ede9ddf0-e11f-4936-aa51-a6a533bc4d9a", "Node", "NODE" });

            migrationBuilder.AddForeignKey(
                name: "FK_Profits_AspNetUsers_User_Id",
                table: "Profits",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_User_Id",
                table: "Transactions",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
