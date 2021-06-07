using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.Entity.Migrations
{
    public partial class createOTPTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OTP",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpiredDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Code = table.Column<string>(type: "nchar(6)", fixedLength: true, maxLength: 6, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4bbf1329-0948-4a35-b3f3-c6da0ecf1d4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "245fea45-2fbc-453d-a00d-eb4bc2adbc87");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "04903130-104c-44be-aa91-5604d096aa8d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OTP");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6d48bf32-a305-42a5-8b43-3bb891efa164");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "19f7f92f-14bc-4b8b-9105-1e3639d61d8f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d7d218d6-3490-4b1e-96d0-3bbcb14cd90e");
        }
    }
}
