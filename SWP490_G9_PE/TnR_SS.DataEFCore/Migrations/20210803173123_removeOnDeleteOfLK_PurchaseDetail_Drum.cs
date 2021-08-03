using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class removeOnDeleteOfLK_PurchaseDetail_Drum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_LKPurchaseDrum_Drum",
                table: "LK_PurchaseDeatil_Drum");

            migrationBuilder.DropForeignKey(
                name: "FK_LKPurchaseDrum_PurchaseDetail",
                table: "LK_PurchaseDeatil_Drum");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LKPurchaseDrum_Drum",
                table: "LK_PurchaseDeatil_Drum",
                column: "DrumID",
                principalTable: "Drum",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LKPurchaseDrum_PurchaseDetail",
                table: "LK_PurchaseDeatil_Drum",
                column: "PurchaseDetailID",
                principalTable: "PurchaseDetail",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_LKPurchaseDrum_Drum",
                table: "LK_PurchaseDeatil_Drum");

            migrationBuilder.DropForeignKey(
                name: "FK_LKPurchaseDrum_PurchaseDetail",
                table: "LK_PurchaseDeatil_Drum");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "8001de94-66d8-4e9e-b39f-579349930c4f", new DateTime(2021, 8, 4, 0, 5, 18, 143, DateTimeKind.Local).AddTicks(7889) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "75d877c8-dd09-4ce8-9e82-9f6b55a212ad", new DateTime(2021, 8, 4, 0, 5, 18, 143, DateTimeKind.Local).AddTicks(7937) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "dfec2232-5fd1-4788-8a00-375aef4827fb", new DateTime(2021, 8, 4, 0, 5, 18, 141, DateTimeKind.Local).AddTicks(9406) });

            migrationBuilder.AddForeignKey(
                name: "FK_LKPurchaseDrum_Drum",
                table: "LK_PurchaseDeatil_Drum",
                column: "DrumID",
                principalTable: "Drum",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LKPurchaseDrum_PurchaseDetail",
                table: "LK_PurchaseDeatil_Drum",
                column: "PurchaseDetailID",
                principalTable: "PurchaseDetail",
                principalColumn: "ID");
        }
    }
}
