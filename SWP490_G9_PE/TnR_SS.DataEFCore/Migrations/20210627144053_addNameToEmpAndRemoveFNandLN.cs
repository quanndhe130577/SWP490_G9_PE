using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addNameToEmpAndRemoveFNandLN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employee",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ac546bdc-4c64-4290-8cd0-3bd25d50870f");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1b0f12bb-48b5-470c-bcab-4552e67f870a");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3c28512d-49e9-46c2-a9cc-52c13271fbfe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employee",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employee",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0de7ad5b-fd35-4680-b4d8-08b4419544c3");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "09391ab1-fab2-431e-b9e6-297b966bdb65");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ad7c1449-ca10-4feb-bd5f-d64f0e8c4c3c");
        }
    }
}
