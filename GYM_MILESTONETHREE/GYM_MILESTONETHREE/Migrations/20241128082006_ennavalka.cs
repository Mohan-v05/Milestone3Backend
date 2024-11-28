using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYM_MILESTONETHREE.Migrations
{
    /// <inheritdoc />
    public partial class ennavalka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_users_PayeeId",
                table: "payments");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_users_PayeeId",
                table: "payments",
                column: "PayeeId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_users_PayeeId",
                table: "payments");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_users_PayeeId",
                table: "payments",
                column: "PayeeId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
