using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYM_MILESTONETHREE.Migrations
{
    /// <inheritdoc />
    public partial class Satiyassothani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "payments");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "payments",
                type: "datetime2",
                nullable: true);
        }
    }
}
