using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addColumnCreateAtUpdateAtHistorySalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "HistorySalaryEmp",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "HistorySalaryEmp",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c7491281-0644-4801-a8e9-589f5cd229e0", new DateTime(2021, 7, 17, 21, 15, 8, 554, DateTimeKind.Local).AddTicks(1134) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "a10bb1a2-3b70-49b3-b8a5-4112057e0b20", new DateTime(2021, 7, 17, 21, 15, 8, 554, DateTimeKind.Local).AddTicks(1166) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "88a2c7d0-1bc5-4994-b0a0-03d0d46a6fd0", new DateTime(2021, 7, 17, 21, 15, 8, 553, DateTimeKind.Local).AddTicks(115) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "HistorySalaryEmp",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "HistorySalaryEmp",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "8b0bc746-0bf0-488c-b60a-def40723131e", new DateTime(2021, 7, 15, 16, 10, 38, 828, DateTimeKind.Local).AddTicks(8281) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "134489cd-e9ef-4df8-b921-40853b4f4e80", new DateTime(2021, 7, 15, 16, 10, 38, 828, DateTimeKind.Local).AddTicks(8319) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "74877de3-6b7e-46d5-a940-6d017cda0bcd", new DateTime(2021, 7, 15, 16, 10, 38, 827, DateTimeKind.Local).AddTicks(4448) });
        }
    }
}
