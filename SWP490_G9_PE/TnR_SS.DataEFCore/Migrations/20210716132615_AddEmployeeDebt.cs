using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class AddEmployeeDebt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeDebt",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDebt", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeDebt_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDebt_EmployeeID",
                table: "EmployeeDebt",
                column: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDebt");

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
