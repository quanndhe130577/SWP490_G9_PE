using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addFishTypePriceAndChangeFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_FishType",
                table: "PurchaseDetail");

            migrationBuilder.RenameColumn(
                name: "FishTypeID",
                table: "PurchaseDetail",
                newName: "FishTypePriceID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_FishTypeID",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_FishTypePriceID");

            migrationBuilder.CreateTable(
                name: "FishTypePrice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishTypeID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishTypePrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FishTypePrice_FishType",
                        column: x => x.FishTypeID,
                        principalTable: "FishType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e5c60964-0814-40c3-b9d6-d427c49ef66f");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1bdb62ae-3973-4071-8851-4c2aa1c8d0b5");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "29586451-8ab8-4fa3-9013-16c248dcf34c");

            migrationBuilder.CreateIndex(
                name: "IX_FishTypePrice_FishTypeID",
                table: "FishTypePrice",
                column: "FishTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_FishTypePrice",
                table: "PurchaseDetail",
                column: "FishTypePriceID",
                principalTable: "FishTypePrice",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_FishTypePrice",
                table: "PurchaseDetail");

            migrationBuilder.DropTable(
                name: "FishTypePrice");

            migrationBuilder.RenameColumn(
                name: "FishTypePriceID",
                table: "PurchaseDetail",
                newName: "FishTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_FishTypePriceID",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_FishTypeID");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "da10ffa9-229e-4cd6-a08d-6ed404599a9e");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "390599a2-71f8-4d2b-aed8-9b4504ef3e47");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "32586707-fdaa-43d6-aed5-f7aba67c0a1b");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_FishType",
                table: "PurchaseDetail",
                column: "FishTypeID",
                principalTable: "FishType",
                principalColumn: "ID");
        }
    }
}
