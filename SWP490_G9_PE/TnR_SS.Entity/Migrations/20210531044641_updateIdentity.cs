using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.Entity.Migrations
{
    public partial class updateIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "UserInfor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "UserInfor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserInfor",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "UserInfor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "UserInfor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "UserInfor",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "UserInfor",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "UserInfor",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "UserInfor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "UserInfor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "UserInfor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "UserInfor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserInfor",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "RoleUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RoleUser",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "RoleUser",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_RoleUser_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_UserInfor_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_UserInfor_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_RoleUser_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_UserInfor_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_UserInfor_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                column: "ConcurrencyStamp",
                value: "bea59828-77ff-4f40-8e18-e7d9623e7aca");

            migrationBuilder.UpdateData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ed9521ee-a78b-4278-aac1-c3724c0e110a");

            migrationBuilder.UpdateData(
                table: "UserInfor",
                keyColumn: "ID",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f0f5e96b-858e-423e-a33b-0c2300d9e88b");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "UserInfor",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "UserInfor",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "RoleUser",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "UserInfor");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "UserInfor");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "RoleUser");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "RoleUser");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RoleUser");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "RoleUser");
        }
    }
}
