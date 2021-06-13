using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class updateTraderIDinPondOwnerAndFishTypeButNotReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Createdat",
                table: "Purchase",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "TraderID",
                table: "PondOwner",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TraderID",
                table: "FishType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "82a1876d-3aff-4c51-9f7f-8547b893bbf9");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a7d06bdd-3787-4eca-a8a6-0765d793b6c4");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "32f5bd91-6eaa-4e0e-9df8-6ffa0f64d393");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "TraderID",
                table: "PondOwner");

            migrationBuilder.DropColumn(
                name: "TraderID",
                table: "FishType");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Purchase",
                newName: "Createdat");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "de073fa8-ba40-42b1-9163-77e51d6ee96f");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e3a39bf5-b1a2-48f4-9a18-52a9a26b2cc3");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3a310127-d604-4aec-8a58-eac58d2ac329");
        }
    }
}
