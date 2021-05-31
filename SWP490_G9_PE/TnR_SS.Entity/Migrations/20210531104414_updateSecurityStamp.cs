using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.Entity.Migrations
{
    public partial class updateSecurityStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e8baefa1-b256-49f8-9095-d1b12f8e10d9");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d415db0d-a436-45b0-86d4-bf742970375b");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "372a5c0f-f4a7-4c06-bf2a-6e4bc14993c2");

            migrationBuilder.UpdateData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4850c617-ee6d-429b-8a2b-33dc176bd376", "4a4f1efe-7534-44e0-908f-f80adefdde76" });

            migrationBuilder.UpdateData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0c55d4ce-bf57-43eb-aa7a-bf0268399e9e", "88208305-7eb2-4d8f-a90d-0ca4b6d5299e" });

            migrationBuilder.UpdateData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "45e4cd1f-31e9-466a-b722-f2bc9931106f", "2c840f3c-a913-4de4-8911-61c73d5878b7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a78dca77-d4a8-4d26-bdf7-9b95773f77bf");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2c1e8314-79b3-46fa-8185-45cbf3a80cbe");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c8499857-01c8-4a2c-9888-a0777d6890bd");

            migrationBuilder.UpdateData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bea59828-77ff-4f40-8e18-e7d9623e7aca", null });

            migrationBuilder.UpdateData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ed9521ee-a78b-4278-aac1-c3724c0e110a", null });

            migrationBuilder.UpdateData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f0f5e96b-858e-423e-a33b-0c2300d9e88b", null });
        }
    }
}
