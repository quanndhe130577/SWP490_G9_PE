using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class AddEmployeeDebtPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "EmployeeDebt",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "2c21961b-264a-460a-ae5d-19fc52618850", new DateTime(2021, 7, 16, 20, 39, 3, 64, DateTimeKind.Local).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "f627206a-e86b-4482-bfaa-433b51f4394d", new DateTime(2021, 7, 16, 20, 39, 3, 64, DateTimeKind.Local).AddTicks(7170) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "0091eac6-3f46-4d42-a352-d83e80112505", new DateTime(2021, 7, 16, 20, 39, 3, 50, DateTimeKind.Local).AddTicks(5230) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "EmployeeDebt");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "e58811d1-e19e-421f-af62-10a3c778e6ae", new DateTime(2021, 7, 16, 20, 26, 14, 829, DateTimeKind.Local).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "a0225550-7155-4752-af9e-6c74df481223", new DateTime(2021, 7, 16, 20, 26, 14, 829, DateTimeKind.Local).AddTicks(9650) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "d72b6215-397e-4d6b-bab0-06b81ebfb03b", new DateTime(2021, 7, 16, 20, 26, 14, 816, DateTimeKind.Local).AddTicks(3400) });
        }
    }
}
