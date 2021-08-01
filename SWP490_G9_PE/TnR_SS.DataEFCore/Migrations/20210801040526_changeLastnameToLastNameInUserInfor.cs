using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class changeLastnameToLastNameInUserInfor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "UserInfor",
                newName: "LastName");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "f063380b-db97-4ac3-9765-0233e625c57a", new DateTime(2021, 8, 1, 11, 5, 25, 940, DateTimeKind.Local).AddTicks(802) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c10b2495-75b5-47c1-a107-f5d790346131", new DateTime(2021, 8, 1, 11, 5, 25, 940, DateTimeKind.Local).AddTicks(851) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "385c230e-4515-438e-8bc4-b4f22080b2e0", new DateTime(2021, 8, 1, 11, 5, 25, 937, DateTimeKind.Local).AddTicks(8551) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "UserInfor",
                newName: "Lastname");

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
        }
    }
}
