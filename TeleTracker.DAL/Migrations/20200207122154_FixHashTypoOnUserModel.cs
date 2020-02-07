using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeleTracker.DAL.Migrations
{
    public partial class FixHashTypoOnUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHast",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHast",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
