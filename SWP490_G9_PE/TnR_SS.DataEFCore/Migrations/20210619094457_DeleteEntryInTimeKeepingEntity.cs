using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class DeleteEntryInTimeKeepingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "TimeKeeping");

            migrationBuilder.DropColumn(
                name: "WorkDay",
                table: "TimeKeeping");

            migrationBuilder.AlterColumn<double>(
                name: "Status",
                table: "TimeKeeping",
                type: "float",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c4c03a9c-de30-4dd1-aefd-aa951b398fa6");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0edd2432-1a48-47e5-9af6-33dc8d4d1ee3");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "16bdcc65-9a27-4a0e-92e6-08f132d26f8f");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TimeKeeping",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TimeKeeping",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkDay",
                table: "TimeKeeping",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "31724f3c-413e-46ca-8cf4-21a7ccf2f284");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c2ebc497-2266-4316-b903-2cded5655070");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "076637eb-deb2-4f39-8301-e349521a4ffe");
        }
    }
}
