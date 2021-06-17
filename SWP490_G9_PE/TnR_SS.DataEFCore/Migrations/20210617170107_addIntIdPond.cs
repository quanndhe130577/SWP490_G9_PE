using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addIntIdPond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PondOwnerID",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "PondOwner",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TraderID",
                table: "PondOwner",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PondOwner",
                table: "PondOwner",
                column: "ID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_PondOwnerID",
                table: "Purchase",
                column: "PondOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_PondOwner_TraderID",
                table: "PondOwner",
                column: "TraderID");

            migrationBuilder.AddForeignKey(
                name: "FK_PondOwner_UserInfor",
                table: "PondOwner",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_PondOwner_PondOwnerID",
                table: "Purchase",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PondOwner_UserInfor",
                table: "PondOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_PondOwner_PondOwnerID",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_PondOwnerID",
                table: "Purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PondOwner",
                table: "PondOwner");

            migrationBuilder.DropIndex(
                name: "IX_PondOwner_TraderID",
                table: "PondOwner");

            migrationBuilder.DropColumn(
                name: "PondOwnerID",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "PondOwner");

            migrationBuilder.DropColumn(
                name: "TraderID",
                table: "PondOwner");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8dd88f93-6a3f-4986-b6a1-3ebc2e3c3027");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "44627eef-f021-4fc8-aa52-b91ee3e64042");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "bf895ea5-0303-42ae-9b43-90671acedaf9");
        }
    }
}
