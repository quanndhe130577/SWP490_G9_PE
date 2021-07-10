using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class removeFishTypePriceAndAddTransactionPriceToFishType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FishTypePrice");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "FishType",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TransactionPrice",
                table: "FishType",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "178e7222-2af1-46b0-bf54-57ae3887f082");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2dfd4946-ce93-47f1-8ec8-1f1d80cdfb86");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d5491626-0928-437b-914a-f883974c5e4e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "FishType");

            migrationBuilder.DropColumn(
                name: "TransactionPrice",
                table: "FishType");

            migrationBuilder.CreateTable(
                name: "FishTypePrice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    FishTypeId = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    TransactionPrice = table.Column<double>(type: "float", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishTypePrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FishTypePrice_FishType",
                        column: x => x.FishTypeId,
                        principalTable: "FishType",
                        principalColumn: "ID");
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cad3ca25-0c53-4ffb-b503-dd84c9780d87");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "21e2405e-de08-43ac-81e3-110cf03eef27");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c574ffc7-12e6-4d9f-8eec-1218084caa4b");

            migrationBuilder.CreateIndex(
                name: "IX_FishTypePrice_FishTypeId",
                table: "FishTypePrice",
                column: "FishTypeId");
        }
    }
}
