using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class removeNodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trees");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5877c21-65f5-4610-9aa9-bcd4cfbbea01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3916127-133c-493f-b81f-1e83f16afac9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "716d2d4c-84b2-4dfd-9608-825046ab23bc", "22ce891e-08f1-4e5d-beda-bf238244f4d5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb42867d-2bf7-4688-9efd-94c26b6804be", "14a693dd-5a60-465e-b6bc-244e391ce808", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trees",
                columns: table => new
                {
                    LeftId = table.Column<int>(type: "int", nullable: true),
                    RightId = table.Column<int>(type: "int", nullable: true),
                    RootId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Trees_Nodes_LeftId",
                        column: x => x.LeftId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trees_Nodes_RightId",
                        column: x => x.RightId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trees_Nodes_RootId",
                        column: x => x.RootId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3916127-133c-493f-b81f-1e83f16afac9", "399cefbb-f91b-497d-bbdb-93dd421a4cde", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c5877c21-65f5-4610-9aa9-bcd4cfbbea01", "d94425c2-dd37-4b28-89bf-a34d102cc063", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_UserId",
                table: "Nodes",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_LeftId",
                table: "Trees",
                column: "LeftId");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_RightId",
                table: "Trees",
                column: "RightId");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_RootId",
                table: "Trees",
                column: "RootId");
        }
    }
}
