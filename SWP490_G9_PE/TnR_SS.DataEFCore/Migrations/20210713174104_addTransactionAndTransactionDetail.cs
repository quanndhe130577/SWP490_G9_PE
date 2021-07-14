using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addTransactionAndTransactionDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraderId = table.Column<int>(type: "int", nullable: false),
                    WeightRecorderId = table.Column<int>(type: "int", nullable: false),
                    CommissionUnit = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transaction_UserInfor-Trader",
                        column: x => x.TraderId,
                        principalTable: "UserInfor",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Transaction_UserInfor-WeightRecorder",
                        column: x => x.WeightRecorderId,
                        principalTable: "UserInfor",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishTypeId = table.Column<int>(type: "int", nullable: false),
                    TransId = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    SellPrice = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransactionDetail_Buyer",
                        column: x => x.BuyerId,
                        principalTable: "Buyer",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TransactionDetail_FishType",
                        column: x => x.FishTypeId,
                        principalTable: "FishType",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TransactionDetail_Transaction",
                        column: x => x.TransId,
                        principalTable: "Transaction",
                        principalColumn: "ID");
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "763710bd-0140-474c-aaaf-ef4db5382bca", new DateTime(2021, 7, 14, 0, 41, 3, 221, DateTimeKind.Local).AddTicks(9929) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c1d5d471-a5f4-4f84-b698-5834b933d7d0", new DateTime(2021, 7, 14, 0, 41, 3, 221, DateTimeKind.Local).AddTicks(9987) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c76e2d7b-ef99-4706-8669-14fa8e0274fd", new DateTime(2021, 7, 14, 0, 41, 3, 220, DateTimeKind.Local).AddTicks(1125) });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TraderId",
                table: "Transaction",
                column: "TraderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_WeightRecorderId",
                table: "Transaction",
                column: "WeightRecorderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetail_BuyerId",
                table: "TransactionDetail",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetail_FishTypeId",
                table: "TransactionDetail",
                column: "FishTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetail_TransId",
                table: "TransactionDetail",
                column: "TransId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDetail");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "937c40c0-34cb-4717-9067-b24965827c58", new DateTime(2021, 7, 11, 15, 47, 41, 221, DateTimeKind.Local).AddTicks(6347) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "35921199-c862-4b85-aae8-9a47f3afbd9e", new DateTime(2021, 7, 11, 15, 47, 41, 221, DateTimeKind.Local).AddTicks(6383) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "570e1e8c-c874-42fc-b1ec-5c2068b65711", new DateTime(2021, 7, 11, 15, 47, 41, 219, DateTimeKind.Local).AddTicks(6929) });
        }
    }
}
