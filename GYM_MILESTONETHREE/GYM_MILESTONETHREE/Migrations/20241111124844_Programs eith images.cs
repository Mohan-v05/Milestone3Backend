﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYM_MILESTONETHREE.Migrations
{
    /// <inheritdoc />
    public partial class Programseithimages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "gymprograms",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "gymprograms");
        }
    }
}