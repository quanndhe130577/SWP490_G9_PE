using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class DeleteMoneyTimeKeeping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "TimeKeeping");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "914520a1-9974-4c31-92d0-3d56c2a7260c", new DateTime(2021, 7, 21, 23, 15, 40, 112, DateTimeKind.Local).AddTicks(100) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c8b02a36-4c86-4fba-987b-afdf0cca5a33", new DateTime(2021, 7, 21, 23, 15, 40, 112, DateTimeKind.Local).AddTicks(150) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "d93ced62-d720-4894-b1af-e9923812e1b5", new DateTime(2021, 7, 21, 23, 15, 40, 95, DateTimeKind.Local).AddTicks(1320) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Money",
                table: "TimeKeeping",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "371e5193-a7f3-4d98-b665-9b915cdfd986", new DateTime(2021, 7, 21, 20, 26, 36, 926, DateTimeKind.Local).AddTicks(9780) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "91431f4f-d06b-4d38-b26d-3e1133cf1d4f", new DateTime(2021, 7, 21, 20, 26, 36, 926, DateTimeKind.Local).AddTicks(9830) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c1e6bb96-9878-4646-a88d-87d0dbc0d639", new DateTime(2021, 7, 21, 20, 26, 36, 913, DateTimeKind.Local).AddTicks(2250) });
        }
    }
}
