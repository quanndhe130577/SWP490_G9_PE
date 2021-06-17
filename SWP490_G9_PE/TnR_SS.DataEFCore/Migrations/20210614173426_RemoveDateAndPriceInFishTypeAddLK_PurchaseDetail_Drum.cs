using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class RemoveDateAndPriceInFishTypeAddLK_PurchaseDetail_Drum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "FishType");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "FishType");

            migrationBuilder.CreateTable(
                name: "LK_PurchaseDeatil_Drum",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDetailID = table.Column<int>(type: "int", nullable: false),
                    DrumID = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_PurchaseDeatil_Drum", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LKPurchaseDrum_Drum",
                        column: x => x.DrumID,
                        principalTable: "Drum",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LKPurchaseDrum_PurchaseDetail",
                        column: x => x.PurchaseDetailID,
                        principalTable: "PurchaseDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c10841db-f0fc-4d0a-b94e-4e53239ecc4f");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2878889c-00ec-4b0d-a47d-e3a5e0eb6480");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d34b5fb1-c9ae-4448-a692-1d1ddbd32f90");

            migrationBuilder.CreateIndex(
                name: "IX_LK_PurchaseDeatil_Drum_DrumID",
                table: "LK_PurchaseDeatil_Drum",
                column: "DrumID");

            migrationBuilder.CreateIndex(
                name: "IX_LK_PurchaseDeatil_Drum_PurchaseDetailID",
                table: "LK_PurchaseDeatil_Drum",
                column: "PurchaseDetailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LK_PurchaseDeatil_Drum");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "FishType",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "FishType",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
        }
    }
}
