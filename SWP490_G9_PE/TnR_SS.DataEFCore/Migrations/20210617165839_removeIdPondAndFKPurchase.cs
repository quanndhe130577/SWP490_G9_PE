using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class removeIdPondAndFKPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PondOwner_UserInfor",
                table: "PondOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_PondOwner",
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

            migrationBuilder.AddColumn<int>(
                name: "TraderID",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Basket_TraderID",
                table: "Basket",
                column: "TraderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_UserInfor",
                table: "Basket",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_UserInfor",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_TraderID",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "TraderID",
                table: "Basket");

            migrationBuilder.AddColumn<Guid>(
                name: "PondOwnerID",
                table: "Purchase",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "PondOwner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                value: "6650a5d5-60f7-4200-bc3e-eeb1b8edf067");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ce53604f-9165-44e6-a5d3-27b6392b9742");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c70cbc1d-22fa-42e5-8c39-ccff3faca573");

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
                name: "FK_Purchase_PondOwner",
                table: "Purchase",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID");
        }
    }
}
