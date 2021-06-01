using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.Entity.Migrations
{
    public partial class addDefaultRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "ID", "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName", "RoleName" },
                values: new object[] { 1, "048b36a8-c473-4716-a9f6-9c6835b4484d", "Admin", null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "ID", "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName", "RoleName" },
                values: new object[] { 2, "833c39fb-371b-472d-88a1-f873807dd300", "Trader", null, null, "Trader" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "ID", "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName", "RoleName" },
                values: new object[] { 3, "bae2bbe4-b6d4-4926-bede-a806457ea819", "Weight Recorder", null, null, "Weight Recorder" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
