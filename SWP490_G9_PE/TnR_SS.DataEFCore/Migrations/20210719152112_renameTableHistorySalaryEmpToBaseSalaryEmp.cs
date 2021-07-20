using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class renameTableHistorySalaryEmpToBaseSalaryEmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorySalaryEmp");

            migrationBuilder.CreateTable(
                name: "BaseSalaryEmp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseSalaryEmp", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BaseSalaryEmp_Employee",
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
                values: new object[] { "7fafe50c-2b11-4451-9255-fa3e4755fdb9", new DateTime(2021, 7, 19, 22, 21, 11, 627, DateTimeKind.Local).AddTicks(500) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "f52083e6-aec2-41cb-a1c9-cf7a9f77fb0a", new DateTime(2021, 7, 19, 22, 21, 11, 627, DateTimeKind.Local).AddTicks(564) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "35c6bd64-4078-4fd4-befc-8b380c58511f", new DateTime(2021, 7, 19, 22, 21, 11, 624, DateTimeKind.Local).AddTicks(8660) });

            migrationBuilder.CreateIndex(
                name: "IX_BaseSalaryEmp_EmpId",
                table: "BaseSalaryEmp",
                column: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseSalaryEmp");

            migrationBuilder.CreateTable(
                name: "HistorySalaryEmp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                values: new object[] { "27d28b87-0166-4c05-97f1-d3a3556a3a54", new DateTime(2021, 7, 19, 11, 31, 14, 372, DateTimeKind.Local).AddTicks(3409) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "2dc61e86-d2c2-4296-a29e-188eeb1f3043", new DateTime(2021, 7, 19, 11, 31, 14, 372, DateTimeKind.Local).AddTicks(3518) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "9456ffb8-f306-490e-ba70-8c0203c52f0a", new DateTime(2021, 7, 19, 11, 31, 14, 370, DateTimeKind.Local).AddTicks(7777) });

            migrationBuilder.CreateIndex(
                name: "IX_HistorySalaryEmp_EmpId",
                table: "HistorySalaryEmp",
                column: "EmpId");
        }
    }
}
