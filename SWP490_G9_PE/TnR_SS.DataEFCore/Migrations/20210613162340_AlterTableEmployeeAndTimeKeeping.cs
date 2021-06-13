using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class AlterTableEmployeeAndTimeKeeping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f3667bf8-18db-40be-b5c1-14c5025e002a");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c698ae5c-eaf3-4b8f-b4e3-c1c3b1c8cc5d");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6c10d3a9-7751-4c14-a984-e113c7a9140a");

            migrationBuilder.CreateIndex(
                name: "IX_TimeKeeping_EmpId",
                table: "TimeKeeping",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_TraderId",
                table: "Employee",
                column: "TraderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_UserInfor",
                table: "Employee",
                column: "TraderId",
                principalTable: "UserInfor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeKeeping_Employee",
                table: "TimeKeeping",
                column: "EmpId",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_UserInfor",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeKeeping_Employee",
                table: "TimeKeeping");

            migrationBuilder.DropIndex(
                name: "IX_TimeKeeping_EmpId",
                table: "TimeKeeping");

            migrationBuilder.DropIndex(
                name: "IX_Employee_TraderId",
                table: "Employee");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dc9f9be7-f79f-4db6-9896-3486f9182cab");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3236d282-1d71-4df3-bada-83ff05b18c28");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4a8682a0-d260-4c33-b26a-8a421db4bf9c");
        }
    }
}
