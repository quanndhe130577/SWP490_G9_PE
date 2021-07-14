using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addTableHistorySalaryEmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorySalaryEmp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorySalaryEmp", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HistorySalaryEmp_Employee",
                        column: x => x.EmpId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "6d6dc15f-bdc6-47ea-b36d-3ea6bd252597", new DateTime(2021, 7, 14, 0, 26, 15, 281, DateTimeKind.Local).AddTicks(6041) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c7de3235-8353-4303-8d0c-c0fa760991d6", new DateTime(2021, 7, 14, 0, 26, 15, 281, DateTimeKind.Local).AddTicks(6074) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "22c19dbf-2dd9-4ef3-a001-753338fa6243", new DateTime(2021, 7, 14, 0, 26, 15, 280, DateTimeKind.Local).AddTicks(5113) });

            migrationBuilder.CreateIndex(
                name: "IX_HistorySalaryEmp_EmpId",
                table: "HistorySalaryEmp",
                column: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorySalaryEmp");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "937c40c0-34cb-4717-9067-b24965827c58", new DateTime(2021, 7, 11, 15, 47, 41, 221, DateTimeKind.Local).AddTicks(6347) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "35921199-c862-4b85-aae8-9a47f3afbd9e", new DateTime(2021, 7, 11, 15, 47, 41, 221, DateTimeKind.Local).AddTicks(6383) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "570e1e8c-c874-42fc-b1ec-5c2068b65711", new DateTime(2021, 7, 11, 15, 47, 41, 219, DateTimeKind.Local).AddTicks(6929) });
        }
    }
}
