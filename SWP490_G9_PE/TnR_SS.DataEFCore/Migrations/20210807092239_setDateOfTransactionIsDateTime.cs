using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class setDateOfTransactionIsDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transaction",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transaction",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "100f65de-7926-4c8a-9154-f30ee10d4ec6", new DateTime(2021, 8, 5, 14, 44, 58, 666, DateTimeKind.Local).AddTicks(9906) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4ac87ecf-f202-4f2f-be3a-f699529c8df7", new DateTime(2021, 8, 5, 14, 44, 58, 666, DateTimeKind.Local).AddTicks(9954) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "da6b1313-ba57-44da-9f21-503a8a65895f", new DateTime(2021, 8, 5, 14, 44, 58, 664, DateTimeKind.Local).AddTicks(5886) });
        }
    }
}
