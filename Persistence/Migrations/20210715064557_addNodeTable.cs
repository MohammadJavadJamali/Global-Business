using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class addNodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c31a703-2411-458d-87d3-035fc8a89699");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc1a450f-e585-415f-8c2c-e45ae1692365");

            migrationBuilder.AddColumn<string>(
                name: "IntroductionCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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
                    RootId = table.Column<int>(type: "int", nullable: true),
                    LeftId = table.Column<int>(type: "int", nullable: true),
                    RightId = table.Column<int>(type: "int", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IntroductionCode",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc1a450f-e585-415f-8c2c-e45ae1692365", "6ddbb9f8-9377-4789-a076-9f27612f950c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5c31a703-2411-458d-87d3-035fc8a89699", "de31ca77-659e-4e2f-bbaf-c0d4e94a897d", "Customer", "CUSTOMER" });
        }
    }
}
