using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.Entity.Migrations
{
    public partial class removePasswordAndSaltPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "SaltPassword",
                table: "UserInfor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserInfor",
                type: "varchar(64)",
                unicode: false,
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaltPassword",
                table: "UserInfor",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "ID", "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName", "RoleName" },
                values: new object[] { 1, "e8baefa1-b256-49f8-9095-d1b12f8e10d9", "Admin", null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "ID", "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName", "RoleName" },
                values: new object[] { 2, "d415db0d-a436-45b0-86d4-bf742970375b", "Trader", null, null, "Trader" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "ID", "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName", "RoleName" },
                values: new object[] { 3, "372a5c0f-f4a7-4c06-bf2a-6e4bc14993c2", "Weight Recorder", null, null, "Weight Recorder" });

            migrationBuilder.InsertData(
                table: "UserInfor",
                columns: new[] { "ID", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "CreatedDate", "DOB", "Email", "EmailConfirmed", "FirstName", "IdentifyCode", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleID", "SaltPassword", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 3, 0, null, "45e4cd1f-31e9-466a-b722-f2bc9931106f", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Admin", "123456789", "Admin", false, null, null, null, "f781bfeada73d5d4d703dca8c3b1b0eba6aa49151ac0fcfaa5d10510eaecdfd3", null, "admin", false, 1, "qwertyuiopasdfghjklz", "2c840f3c-a913-4de4-8911-61c73d5878b7", false, null });

            migrationBuilder.InsertData(
                table: "UserInfor",
                columns: new[] { "ID", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "CreatedDate", "DOB", "Email", "EmailConfirmed", "FirstName", "IdentifyCode", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleID", "SaltPassword", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, null, "4850c617-ee6d-429b-8a2b-33dc176bd376", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Nguyen", "123456789", "Quan", false, null, null, null, "1a56be4be3e34472001aa7e5f5fc5cbe84428edfe902bdf1508fcf33ff517198", null, "0966848112", false, 2, "qwertyuiopasdfghjklz", "4a4f1efe-7534-44e0-908f-f80adefdde76", false, null });

            migrationBuilder.InsertData(
                table: "UserInfor",
                columns: new[] { "ID", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "CreatedDate", "DOB", "Email", "EmailConfirmed", "FirstName", "IdentifyCode", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleID", "SaltPassword", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 2, 0, null, "0c55d4ce-bf57-43eb-aa7a-bf0268399e9e", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Anh", "123456789", "Duc", false, null, null, null, "1a56be4be3e34472001aa7e5f5fc5cbe84428edfe902bdf1508fcf33ff517198", null, "0969360445", false, 3, "qwertyuiopasdfghjklz", "88208305-7eb2-4d8f-a90d-0ca4b6d5299e", false, null });
        }
    }
}
