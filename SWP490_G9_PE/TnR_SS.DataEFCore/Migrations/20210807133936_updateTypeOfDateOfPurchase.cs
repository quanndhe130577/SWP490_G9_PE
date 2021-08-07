using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class updateTypeOfDateOfPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Purchase",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "46f44e85-5537-4ce5-b797-0d8cc0047adb", new DateTime(2021, 8, 7, 20, 39, 35, 478, DateTimeKind.Local).AddTicks(4058) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "12cc6f72-9eb8-49e2-b868-9e5c511261b5", new DateTime(2021, 8, 7, 20, 39, 35, 478, DateTimeKind.Local).AddTicks(4095) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "7057ec5f-2372-4243-83d9-8f16cee91ca8", new DateTime(2021, 8, 7, 20, 39, 35, 476, DateTimeKind.Local).AddTicks(8297) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Purchase",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "a3568960-66f6-47bc-abfc-65a47efe4ca8", new DateTime(2021, 8, 7, 16, 22, 39, 20, DateTimeKind.Local).AddTicks(3857) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "6984a8f7-5221-4991-93e5-f0dfc8fa09ad", new DateTime(2021, 8, 7, 16, 22, 39, 20, DateTimeKind.Local).AddTicks(3911) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "66eb3558-0d06-4e9a-aaf7-0975e80fb961", new DateTime(2021, 8, 7, 16, 22, 39, 18, DateTimeKind.Local).AddTicks(6953) });
        }
    }
}
