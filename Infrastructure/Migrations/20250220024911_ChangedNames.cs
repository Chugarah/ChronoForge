using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesEntityUsersEntity");

            migrationBuilder.CreateTable(
                name: "ProjectServices",
                columns: table => new
                {
                    ProjectsEntityId = table.Column<int>(type: "int", nullable: false),
                    ServicesEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectServices", x => new { x.ProjectsEntityId, x.ServicesEntityId });
                    table.ForeignKey(
                        name: "FK_ProjectServices_Projects_ProjectsEntityId",
                        column: x => x.ProjectsEntityId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectServices_Services_ServicesEntityId",
                        column: x => x.ServicesEntityId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolesUsers",
                columns: table => new
                {
                    RolesEntityId = table.Column<int>(type: "int", nullable: false),
                    UsersEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsers", x => new { x.RolesEntityId, x.UsersEntityId });
                    table.ForeignKey(
                        name: "FK_RolesUsers_Roles_RolesEntityId",
                        column: x => x.RolesEntityId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolesUsers_Users_UsersEntityId",
                        column: x => x.UsersEntityId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectServices_ServicesEntityId",
                table: "ProjectServices",
                column: "ServicesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsers_UsersEntityId",
                table: "RolesUsers",
                column: "UsersEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectServices");

            migrationBuilder.DropTable(
                name: "RolesUsers");

            migrationBuilder.CreateTable(
                name: "ProjectsEntityServicesEntity",
                columns: table => new
                {
                    ProjectsEntityId = table.Column<int>(type: "int", nullable: false),
                    ServicesEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsEntityServicesEntity", x => new { x.ProjectsEntityId, x.ServicesEntityId });
                    table.ForeignKey(
                        name: "FK_ProjectsEntityServicesEntity_Projects_ProjectsEntityId",
                        column: x => x.ProjectsEntityId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectsEntityServicesEntity_Services_ServicesEntityId",
                        column: x => x.ServicesEntityId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolesEntityUsersEntity",
                columns: table => new
                {
                    RolesEntityId = table.Column<int>(type: "int", nullable: false),
                    UsersEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesEntityUsersEntity", x => new { x.RolesEntityId, x.UsersEntityId });
                    table.ForeignKey(
                        name: "FK_RolesEntityUsersEntity_Roles_RolesEntityId",
                        column: x => x.RolesEntityId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolesEntityUsersEntity_Users_UsersEntityId",
                        column: x => x.UsersEntityId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsEntityServicesEntity_ServicesEntityId",
                table: "ProjectsEntityServicesEntity",
                column: "ServicesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesEntityUsersEntity_UsersEntityId",
                table: "RolesEntityUsersEntity",
                column: "UsersEntityId");
        }
    }
}
