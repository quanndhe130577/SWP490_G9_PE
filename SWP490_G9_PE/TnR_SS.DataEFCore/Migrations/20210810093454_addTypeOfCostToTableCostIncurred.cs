using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addTypeOfCostToTableCostIncurred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeOfCost",
                table: "CostIncurred",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "10035ac6-3f22-4ffe-9b4b-0d71246bf11d", new DateTime(2021, 8, 10, 16, 34, 54, 9, DateTimeKind.Local).AddTicks(3467) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "32dc83bc-5503-4dca-9cb1-76b6537ef0f8", new DateTime(2021, 8, 10, 16, 34, 54, 9, DateTimeKind.Local).AddTicks(3509) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c9a027f3-f573-4ca5-8a36-e084943d337a", new DateTime(2021, 8, 10, 16, 34, 54, 6, DateTimeKind.Local).AddTicks(9079) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfCost",
                table: "CostIncurred");

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
    }
}
