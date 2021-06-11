using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addTongKetMuaEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TongKetMuas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    TotalWeight = table.Column<double>(type: "float", nullable: false),
                    PayForPondOwner = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TienGioiThieu = table.Column<double>(type: "float", nullable: false),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    PondBackMoney = table.Column<double>(type: "float", nullable: false),
                    PondOwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TraderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TongKetMuas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TongKetMua_PondOwner",
                        column: x => x.PondOwnerID,
                        principalTable: "PondOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TongKetMua_UserInfor",
                        column: x => x.TraderID,
                        principalTable: "UserInfor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9663d868-b09b-4f02-b8d1-208a76afc952");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "385b5624-8962-45ab-bed6-45d483f99820");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "09cd8d3c-7928-4d41-9714-37231cdc6be3");

            migrationBuilder.CreateIndex(
                name: "IX_TongKetMuas_PondOwnerID",
                table: "TongKetMuas",
                column: "PondOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_TongKetMuas_TraderID",
                table: "TongKetMuas",
                column: "TraderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TongKetMuas");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2d4f4585-c432-4b00-a848-61a50c6f1f8f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f2b345de-5668-4abd-8f03-e73a59adaaf0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "557cc5a5-3b0d-48d1-8bc9-fb6cc6bd2e1f");
        }
    }
}
