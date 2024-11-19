using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYM_MILESTONETHREE.Migrations
{
    /// <inheritdoc />
    public partial class finalnotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notification_users_UsersId",
                table: "notification");

            migrationBuilder.DropIndex(
                name: "IX_notification_UsersId",
                table: "notification");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "notification");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "notification",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_notification_UserId",
                table: "notification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_users_UserId",
                table: "notification",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notification_users_UserId",
                table: "notification");

            migrationBuilder.DropIndex(
                name: "IX_notification_UserId",
                table: "notification");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "notification",
                newName: "userId");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "notification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_notification_UsersId",
                table: "notification",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_users_UsersId",
                table: "notification",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
