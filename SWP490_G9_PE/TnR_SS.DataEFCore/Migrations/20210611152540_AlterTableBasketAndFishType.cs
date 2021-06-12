using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class AlterTableBasketAndFishType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionBuy");

            migrationBuilder.DropTable(
                name: "TongKetMua");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FishType",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FishType",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Basket",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Basket",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Purchase",
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
                    table.PrimaryKey("PK_Purchase", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Purchase_PondOwner",
                        column: x => x.PondOwnerID,
                        principalTable: "PondOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchase_UserInfor",
                        column: x => x.TraderID,
                        principalTable: "UserInfor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishTypeID = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<double>(type: "float", nullable: false),
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    TongKetMuaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchaseDetail_Basket",
                        column: x => x.BasketId,
                        principalTable: "Basket",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDetail_FishType",
                        column: x => x.FishTypeID,
                        principalTable: "FishType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDetail_TongKetMua",
                        column: x => x.TongKetMuaId,
                        principalTable: "Purchase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d53a6346-d771-403e-8af1-b355aa87d518");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "813afca5-a812-472e-91c7-1cfdf1a6b6dc");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "91245791-e09e-46cb-b985-5a898e305984");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_PondOwnerID",
                table: "Purchase",
                column: "PondOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_TraderID",
                table: "Purchase",
                column: "TraderID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_BasketId",
                table: "PurchaseDetail",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_FishTypeID",
                table: "PurchaseDetail",
                column: "FishTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_TongKetMuaId",
                table: "PurchaseDetail",
                column: "TongKetMuaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseDetail");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FishType");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FishType");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Basket");

            migrationBuilder.CreateTable(
                name: "TongKetMua",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayForPondOwner = table.Column<double>(type: "float", nullable: false),
                    PondBackMoney = table.Column<double>(type: "float", nullable: false),
                    PondOwnerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TienGioiThieu = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    TotalWeight = table.Column<double>(type: "float", nullable: false),
                    TraderID = table.Column<int>(type: "int", nullable: false),
                    isPaid = table.Column<bool>(type: "bit", nullable: false)
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
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<double>(type: "float", nullable: false),
                    FishTypeID = table.Column<int>(type: "int", nullable: false),
                    TongKetMuaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionBuy", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransactionBuy_Basket",
                        column: x => x.BasketId,
                        principalTable: "Basket",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionBuy_FishType",
                        column: x => x.FishTypeID,
                        principalTable: "FishType",
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
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "bd8b431c-a757-4bc4-84b0-c95410633c4b");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "35ad9bde-412c-40a6-9708-2f45e88af580");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ec73f9eb-3a8f-49a1-aab5-d6341d2a1300");

            migrationBuilder.CreateIndex(
                name: "IX_TongKetMua_PondOwnerID",
                table: "TongKetMua",
                column: "PondOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_TongKetMua_TraderID",
                table: "TongKetMua",
                column: "TraderID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBuy_BasketId",
                table: "TransactionBuy",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBuy_FishTypeID",
                table: "TransactionBuy",
                column: "FishTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionBuy_TongKetMuaId",
                table: "TransactionBuy",
                column: "TongKetMuaId");
        }
    }
}
