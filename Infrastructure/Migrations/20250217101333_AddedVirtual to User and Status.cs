using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedVirtualtoUserandStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesEntityUsersEntity_Roles_RolesId",
                table: "RolesEntityUsersEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesEntityUsersEntity_Users_UsersId",
                table: "RolesEntityUsersEntity");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "RolesEntityUsersEntity",
                newName: "UsersEntityId");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "RolesEntityUsersEntity",
                newName: "RolesEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_RolesEntityUsersEntity_UsersId",
                table: "RolesEntityUsersEntity",
                newName: "IX_RolesEntityUsersEntity_UsersEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesEntityUsersEntity_Roles_RolesEntityId",
                table: "RolesEntityUsersEntity",
                column: "RolesEntityId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesEntityUsersEntity_Users_UsersEntityId",
                table: "RolesEntityUsersEntity",
                column: "UsersEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesEntityUsersEntity_Roles_RolesEntityId",
                table: "RolesEntityUsersEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesEntityUsersEntity_Users_UsersEntityId",
                table: "RolesEntityUsersEntity");

            migrationBuilder.RenameColumn(
                name: "UsersEntityId",
                table: "RolesEntityUsersEntity",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "RolesEntityId",
                table: "RolesEntityUsersEntity",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_RolesEntityUsersEntity_UsersEntityId",
                table: "RolesEntityUsersEntity",
                newName: "IX_RolesEntityUsersEntity_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesEntityUsersEntity_Roles_RolesId",
                table: "RolesEntityUsersEntity",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesEntityUsersEntity_Users_UsersId",
                table: "RolesEntityUsersEntity",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
