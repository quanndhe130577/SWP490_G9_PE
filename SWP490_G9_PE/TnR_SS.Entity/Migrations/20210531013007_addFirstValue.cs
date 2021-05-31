using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.Entity.Migrations
{
    public partial class addFirstValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserInfor",
                type: "varchar(64)",
                unicode: false,
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldUnicode: false,
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "ID", "DisplayName", "RoleName" },
                values: new object[] { 1, "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "ID", "DisplayName", "RoleName" },
                values: new object[] { 2, "Trader", "Trader" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "ID", "DisplayName", "RoleName" },
                values: new object[] { 3, "Weight Recorder", "Weight Recorder" });

            migrationBuilder.InsertData(
                table: "UserInfor",
                columns: new[] { "ID", "Avatar", "CreatedDate", "DOB", "FirstName", "IdentifyCode", "Lastname", "Password", "PhoneNumber", "RoleID", "SaltPassword" },
                values: new object[] { 3, null, new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "123456789", "Admin", "f781bfeada73d5d4d703dca8c3b1b0eba6aa49151ac0fcfaa5d10510eaecdfd3", "admin", 1, "qwertyuiopasdfghjklz" });

            migrationBuilder.InsertData(
                table: "UserInfor",
                columns: new[] { "ID", "Avatar", "CreatedDate", "DOB", "FirstName", "IdentifyCode", "Lastname", "Password", "PhoneNumber", "RoleID", "SaltPassword" },
                values: new object[] { 1, null, new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen", "123456789", "Quan", "1a56be4be3e34472001aa7e5f5fc5cbe84428edfe902bdf1508fcf33ff517198", "0966848112", 2, "qwertyuiopasdfghjklz" });

            migrationBuilder.InsertData(
                table: "UserInfor",
                columns: new[] { "ID", "Avatar", "CreatedDate", "DOB", "FirstName", "IdentifyCode", "Lastname", "Password", "PhoneNumber", "RoleID", "SaltPassword" },
                values: new object[] { 2, null, new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anh", "123456789", "Duc", "1a56be4be3e34472001aa7e5f5fc5cbe84428edfe902bdf1508fcf33ff517198", "0969360445", 3, "qwertyuiopasdfghjklz" });

            migrationBuilder.CreateIndex(
                name: "UC_PhoneNumber",
                table: "UserInfor",
                columns: new[] { "ID", "PhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PhoneNumber",
                table: "UserInfor",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UC_PhoneNumber",
                table: "UserInfor");

            migrationBuilder.DropIndex(
                name: "UQ_PhoneNumber",
                table: "UserInfor");

            migrationBuilder.DeleteData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserInfor",
                type: "varchar(64)",
                unicode: false,
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldUnicode: false,
                oldMaxLength: 64);
        }
    }
}
