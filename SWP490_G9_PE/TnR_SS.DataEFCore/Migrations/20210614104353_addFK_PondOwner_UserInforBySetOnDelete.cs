using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addFK_PondOwner_UserInforBySetOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_UserInfor",
                table: "Purchase");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2bc3bf1a-ca0d-4d82-a382-824720816f94");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "59de8a88-cc47-4b5b-b7fb-643d6cb644ac");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6ec51015-d001-4331-908d-dcae2ee39c8e");

            migrationBuilder.CreateIndex(
                name: "IX_PondOwner_TraderID",
                table: "PondOwner",
                column: "TraderID");

            migrationBuilder.AddForeignKey(
                name: "FK_PondOwner_UserInfor",
                table: "PondOwner",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_UserInfor",
                table: "Purchase",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PondOwner_UserInfor",
                table: "PondOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_UserInfor",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_PondOwner_TraderID",
                table: "PondOwner");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "68b729b6-2282-49c8-99d9-637f84c7f585");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c213bda2-d979-481d-91f2-a5913df1fa05");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "706ca6ff-688a-4c58-badb-27d8404ed9aa");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_UserInfor",
                table: "Purchase",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
