using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class ChangeFKOfClosePurchaseDetailToPurchaseAndAddFKFromCpdToLK_Drum_Pur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [ClosePurchaseDetails]", true);

            migrationBuilder.DropForeignKey(
                name: "FK_ClosePurchaseDetail_PurchaseDetail",
                table: "ClosePurchaseDetails");

            migrationBuilder.DropIndex(
                name: "IX_ClosePurchaseDetails_PurchaseDetailId",
                table: "ClosePurchaseDetails");

            migrationBuilder.RenameColumn(
                name: "PurchaseDetailId",
                table: "ClosePurchaseDetails",
                newName: "PurchaseId");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseDetailID",
                table: "LK_PurchaseDeatil_Drum",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClosePurchaseDetailID",
                table: "LK_PurchaseDeatil_Drum",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "9ae2345c-ce21-430e-a9e3-d521782de89e", new DateTime(2021, 8, 15, 20, 43, 16, 898, DateTimeKind.Local).AddTicks(150) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4679db98-1363-4ace-b31f-b746b8755b0f", new DateTime(2021, 8, 15, 20, 43, 16, 898, DateTimeKind.Local).AddTicks(212) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "2712ba23-49c8-40f9-9852-3973c304352d", new DateTime(2021, 8, 15, 20, 43, 16, 895, DateTimeKind.Local).AddTicks(9861) });

            migrationBuilder.CreateIndex(
                name: "IX_LK_PurchaseDeatil_Drum_ClosePurchaseDetailID",
                table: "LK_PurchaseDeatil_Drum",
                column: "ClosePurchaseDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_ClosePurchaseDetails_PurchaseId",
                table: "ClosePurchaseDetails",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClosePurchaseDetail_Purchase",
                table: "ClosePurchaseDetails",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LKPurchaseDrum_ClosePurchaseDetail",
                table: "LK_PurchaseDeatil_Drum",
                column: "ClosePurchaseDetailID",
                principalTable: "ClosePurchaseDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClosePurchaseDetail_Purchase",
                table: "ClosePurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_LKPurchaseDrum_ClosePurchaseDetail",
                table: "LK_PurchaseDeatil_Drum");

            migrationBuilder.DropIndex(
                name: "IX_LK_PurchaseDeatil_Drum_ClosePurchaseDetailID",
                table: "LK_PurchaseDeatil_Drum");

            migrationBuilder.DropIndex(
                name: "IX_ClosePurchaseDetails_PurchaseId",
                table: "ClosePurchaseDetails");

            migrationBuilder.DropColumn(
                name: "ClosePurchaseDetailID",
                table: "LK_PurchaseDeatil_Drum");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "ClosePurchaseDetails",
                newName: "PurchaseDetailId");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseDetailID",
                table: "LK_PurchaseDeatil_Drum",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "1b27f43d-8e28-43be-a0c2-e2648237fd0d", new DateTime(2021, 8, 12, 15, 19, 41, 372, DateTimeKind.Local).AddTicks(9382) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "f203f6ca-9a62-49cf-b9e8-0c1b3b60c0d9", new DateTime(2021, 8, 12, 15, 19, 41, 372, DateTimeKind.Local).AddTicks(9423) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "de869db4-d0d2-493c-a666-01a85abe550e", new DateTime(2021, 8, 12, 15, 19, 41, 370, DateTimeKind.Local).AddTicks(6146) });

            migrationBuilder.CreateIndex(
                name: "IX_ClosePurchaseDetails_PurchaseDetailId",
                table: "ClosePurchaseDetails",
                column: "PurchaseDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClosePurchaseDetail_PurchaseDetail",
                table: "ClosePurchaseDetails",
                column: "PurchaseDetailId",
                principalTable: "PurchaseDetail",
                principalColumn: "ID");
        }
    }
}
