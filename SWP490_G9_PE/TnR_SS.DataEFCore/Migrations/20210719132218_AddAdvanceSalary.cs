using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class AddAdvanceSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDebt");

            migrationBuilder.CreateTable(
                name: "AdvanceSalary",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceSalary", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AdvanceSalary_Employee_EmployeeID",
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
                values: new object[] { "70672d4d-6818-42b5-afe7-11cf8a9baf48", new DateTime(2021, 7, 19, 20, 22, 18, 31, DateTimeKind.Local).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "efc40775-6697-4904-96ad-7ceba51dd8dc", new DateTime(2021, 7, 19, 20, 22, 18, 31, DateTimeKind.Local).AddTicks(6690) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "d877cf18-be09-4bf5-ade9-196578c1275e", new DateTime(2021, 7, 19, 20, 22, 18, 17, DateTimeKind.Local).AddTicks(5710) });

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceSalary_EmployeeID",
                table: "AdvanceSalary",
                column: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvanceSalary");

            migrationBuilder.CreateTable(
                name: "EmployeeDebt",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
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
                name: "IX_EmployeeDebt_EmployeeID",
                table: "EmployeeDebt",
                column: "EmployeeID");
        }
    }
}
