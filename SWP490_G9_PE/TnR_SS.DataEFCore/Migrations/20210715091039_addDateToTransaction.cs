using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addDateToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Transaction",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Transaction");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "d8531425-55e8-4720-9bfc-ca8bf452f986", new DateTime(2021, 7, 14, 22, 33, 6, 654, DateTimeKind.Local).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "967b3ece-1e70-4036-9831-a5010910cd42", new DateTime(2021, 7, 14, 22, 33, 6, 654, DateTimeKind.Local).AddTicks(7546) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "e5659458-419b-4a74-8bfc-bfcb911dbd0b", new DateTime(2021, 7, 14, 22, 33, 6, 653, DateTimeKind.Local).AddTicks(451) });
        }
    }
}
