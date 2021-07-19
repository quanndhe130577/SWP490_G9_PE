using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class createTableHistorySalaryEmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorySalaryEmp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
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
                values: new object[] { "62c0c25f-f55a-4c92-b85d-d1d1269cf582", new DateTime(2021, 7, 19, 22, 41, 19, 23, DateTimeKind.Local).AddTicks(2118) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "35ae58a7-3b1b-4d9a-bc3d-710e07afa7e1", new DateTime(2021, 7, 19, 22, 41, 19, 23, DateTimeKind.Local).AddTicks(2156) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "488323d1-316d-440d-9fde-c1f803e356d3", new DateTime(2021, 7, 19, 22, 41, 19, 22, DateTimeKind.Local).AddTicks(275) });

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
        }
    }
}
