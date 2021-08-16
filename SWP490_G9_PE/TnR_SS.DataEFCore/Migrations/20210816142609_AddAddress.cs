using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class AddAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserInfor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "e6933cab-3ca0-4edd-a9f9-49e1ecc20608", new DateTime(2021, 8, 16, 21, 26, 8, 943, DateTimeKind.Local).AddTicks(2500) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4c163e4d-80fc-4fa1-932b-fc11667a3774", new DateTime(2021, 8, 16, 21, 26, 8, 943, DateTimeKind.Local).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "3a650bc2-2e65-464a-82f0-6de9f69afe2c", new DateTime(2021, 8, 16, 21, 26, 8, 929, DateTimeKind.Local).AddTicks(6210) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserInfor");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "801c3b00-8dc2-4324-b38a-33a5ed429011", new DateTime(2021, 8, 15, 23, 48, 53, 984, DateTimeKind.Local).AddTicks(1537) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "b833ba9a-dca5-478d-8c30-cbbfa8754a10", new DateTime(2021, 8, 15, 23, 48, 53, 984, DateTimeKind.Local).AddTicks(1602) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "894395e2-0233-4558-9ce5-f3ee6a3ed1c0", new DateTime(2021, 8, 15, 23, 48, 53, 981, DateTimeKind.Local).AddTicks(9235) });
        }
    }
}
