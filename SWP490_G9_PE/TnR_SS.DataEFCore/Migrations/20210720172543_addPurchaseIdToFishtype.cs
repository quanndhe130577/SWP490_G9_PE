using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addPurchaseIdToFishtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseID",
                table: "FishType",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "ecf7f7f9-6956-4048-831c-21cc7c278f9f", new DateTime(2021, 7, 21, 0, 25, 42, 970, DateTimeKind.Local).AddTicks(210) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "8cd43b6b-9185-4df1-959f-6565565b63a9", new DateTime(2021, 7, 21, 0, 25, 42, 970, DateTimeKind.Local).AddTicks(245) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "3a9819b1-902c-4506-bafe-442eed9d84d8", new DateTime(2021, 7, 21, 0, 25, 42, 968, DateTimeKind.Local).AddTicks(4898) });

            migrationBuilder.CreateIndex(
                name: "IX_FishType_PurchaseID",
                table: "FishType",
                column: "PurchaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_FishType_Purchase",
                table: "FishType",
                column: "PurchaseID",
                principalTable: "Purchase",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishType_Purchase",
                table: "FishType");


            migrationBuilder.DropIndex(
                name: "IX_FishType_PurchaseID",
                table: "FishType");

            migrationBuilder.DropColumn(
                name: "PurchaseID",
                table: "FishType");

           

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "62c0c25f-f55a-4c92-b85d-d1d1269cf582", new DateTime(2021, 7, 19, 22, 41, 19, 23, DateTimeKind.Local).AddTicks(2118) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "35ae58a7-3b1b-4d9a-bc3d-710e07afa7e1", new DateTime(2021, 7, 19, 22, 41, 19, 23, DateTimeKind.Local).AddTicks(2156) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "488323d1-316d-440d-9fde-c1f803e356d3", new DateTime(2021, 7, 19, 22, 41, 19, 22, DateTimeKind.Local).AddTicks(275) });

        }
    }
}
