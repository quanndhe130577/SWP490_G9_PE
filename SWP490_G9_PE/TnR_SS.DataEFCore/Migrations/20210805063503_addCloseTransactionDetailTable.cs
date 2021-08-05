using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addCloseTransactionDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CloseTransactionDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishTypeId = table.Column<int>(type: "int", nullable: false),
                    FishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FishTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FishTypeMinWeight = table.Column<float>(type: "real", nullable: false),
                    FishTypeMaxWeight = table.Column<float>(type: "real", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: true),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellPrice = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    TransactionDetailId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloseTransactionDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CloseTransactionDetail_TransactionDetail",
                        column: x => x.TransactionDetailId,
                        principalTable: "TransactionDetail",
                        principalColumn: "ID");
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "bf9edf7d-17f0-4e9e-a816-2a0b4c48d335", new DateTime(2021, 8, 5, 13, 35, 3, 30, DateTimeKind.Local).AddTicks(1446) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "e1148d50-c1b1-4b4e-833b-1c748cf24bf6", new DateTime(2021, 8, 5, 13, 35, 3, 30, DateTimeKind.Local).AddTicks(1629) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "14bd2fc3-9d88-4d38-81ea-eedf5efb366e", new DateTime(2021, 8, 5, 13, 35, 3, 27, DateTimeKind.Local).AddTicks(8452) });

            migrationBuilder.CreateIndex(
                name: "IX_CloseTransactionDetails_TransactionDetailId",
                table: "CloseTransactionDetails",
                column: "TransactionDetailId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CloseTransactionDetails");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "e3a5bedc-91d9-4b00-86ef-aa5f42ade350", new DateTime(2021, 8, 4, 0, 31, 22, 565, DateTimeKind.Local).AddTicks(2333) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "ec3ae8b5-f6ee-4902-81d4-0457a0c85343", new DateTime(2021, 8, 4, 0, 31, 22, 565, DateTimeKind.Local).AddTicks(2407) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4199701e-3afe-4beb-abd7-c19a8401e874", new DateTime(2021, 8, 4, 0, 31, 22, 562, DateTimeKind.Local).AddTicks(8363) });
        }
    }
}
