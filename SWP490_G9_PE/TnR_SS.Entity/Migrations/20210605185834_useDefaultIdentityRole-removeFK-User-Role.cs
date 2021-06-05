using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.Entity.Migrations
{
    public partial class useDefaultIdentityRoleremoveFKUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_RoleUser_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_RoleUser_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK__UserInfor__RoleUser",
                table: "UserInfor");

            migrationBuilder.DropIndex(
                name: "IX_UserInfor_RoleID",
                table: "UserInfor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleUser",
                table: "RoleUser");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "RoleUser");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "RoleUser");

            migrationBuilder.RenameTable(
                name: "RoleUser",
                newName: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AspNetRoles",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2bf2693d-64f7-4015-945b-11b1dcca3024", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "811ed2cd-f71e-4366-b558-b392ddb805ae", "Trader", "Thương lái" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "091fbf22-b231-4a76-8c10-576f537e809e", "Weight Recorder", "Chủ bến" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "RoleUser");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RoleUser",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "UserInfor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "RoleUser",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "RoleUser",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleUser",
                table: "RoleUser",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName", "RoleName" },
                values: new object[] { "048b36a8-c473-4716-a9f6-9c6835b4484d", "Admin", null, null, "Admin" });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName", "RoleName" },
                values: new object[] { "833c39fb-371b-472d-88a1-f873807dd300", "Trader", null, null, "Trader" });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "DisplayName", "Name", "NormalizedName", "RoleName" },
                values: new object[] { "bae2bbe4-b6d4-4926-bede-a806457ea819", "Weight Recorder", null, null, "Weight Recorder" });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfor_RoleID",
                table: "UserInfor",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_RoleUser_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "RoleUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_RoleUser_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "RoleUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__UserInfor__RoleUser",
                table: "UserInfor",
                column: "RoleID",
                principalTable: "RoleUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
