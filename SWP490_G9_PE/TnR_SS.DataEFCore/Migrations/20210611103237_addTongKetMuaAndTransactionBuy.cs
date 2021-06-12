using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addTongKetMuaAndTransactionBuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "TongKetMua",
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
                    table.PrimaryKey("PK_TongKetMua", x => x.ID);
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

            migrationBuilder.CreateTable(
                name: "TransactionBuy",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishTypeID = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<double>(type: "float", nullable: false),
                    RoId = table.Column<int>(type: "int", nullable: false),
                    TongKetMuaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionBuy", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransactionBuy_FishType",
                        column: x => x.FishTypeID,
                        principalTable: "FishType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionBuy_Ro",
                        column: x => x.RoId,
                        principalTable: "Ro",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionBuy_TongKetMua",
                        column: x => x.TongKetMuaId,
                        principalTable: "TongKetMua",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5e6218e6-64b4-4610-b4ae-b5ccaff785b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "867fe0f6-dfe5-44aa-8e21-f6bb676dc8a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f5b4bd94-d7a3-4728-bc3a-3f7d2bee8413");

            migrationBuilder.CreateIndex(
                name: "IX_TongKetMua_PondOwnerID",
                table: "TongKetMua",
                column: "PondOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_TongKetMua_TraderID",
                table: "TongKetMua",
                column: "TraderID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBuy_FishTypeID",
                table: "TransactionBuy",
                column: "FishTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBuy_RoId",
                table: "TransactionBuy",
                column: "RoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBuy_TongKetMuaId",
                table: "TransactionBuy",
                column: "TongKetMuaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionBuy");

            migrationBuilder.DropTable(
                name: "TongKetMua");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e14368cf-41e1-4a59-bcb8-9dedaf2543ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d2920100-585d-4569-b9bb-001bebb06b7f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c5833868-76b4-47a2-b6d2-6a5dc4768a2e");
        }
    }
}
