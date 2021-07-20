using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class AddAdvanceSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvanceSalary",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceSalary", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AdvanceSalary_Employee",
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
                values: new object[] { "70d44482-92b9-469e-a863-7639efdcfa37", new DateTime(2021, 7, 20, 20, 53, 6, 763, DateTimeKind.Local).AddTicks(9080) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "2ec55572-e878-4f10-972a-8bcc42b16fc9", new DateTime(2021, 7, 20, 20, 53, 6, 763, DateTimeKind.Local).AddTicks(9130) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "5ec647b6-934a-4723-9c9d-71504c60425a", new DateTime(2021, 7, 20, 20, 53, 6, 746, DateTimeKind.Local).AddTicks(9110) });

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceSalary_EmpId",
                table: "AdvanceSalary",
                column: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvanceSalary");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "fb929971-1c25-443f-aa22-ac47c6c5fd74", new DateTime(2021, 7, 20, 20, 44, 44, 874, DateTimeKind.Local).AddTicks(1010) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c1c704a3-7712-4047-97af-d596418d56e2", new DateTime(2021, 7, 20, 20, 44, 44, 874, DateTimeKind.Local).AddTicks(1120) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "cabc4f0a-d86e-4cab-bdf0-82c0cfb98dbf", new DateTime(2021, 7, 20, 20, 44, 44, 827, DateTimeKind.Local).AddTicks(3330) });
        }
    }
}
