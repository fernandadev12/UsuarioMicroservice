using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserMicroservice.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastAccess = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "LastAccess", "Role", "Username", "Email", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("248a2ef7-eda1-4a60-987a-32a228a0cdcb"), new DateTime(2025, 12, 29, 0, 16, 45, 160, DateTimeKind.Utc).AddTicks(216), "Administrador", "admin", "admin@system.com", "Admin@123" },
                    { new Guid("f3dbde56-d8c0-427a-8a54-a22d08577cda"), new DateTime(2025, 12, 29, 0, 16, 45, 160, DateTimeKind.Utc).AddTicks(219), "Usuario", "usuarioGamer", "gamer@system.com", "gamer@123" }
                });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("40a3f868-b6b8-4980-857f-486d407997f6"), "Pode visualizar relatórios", "Usuario", new Guid("248a2ef7-eda1-4a60-987a-32a228a0cdcb") },
                    { new Guid("8bf467b1-61f2-4c5d-828a-b9f1140c0b3d"), "Pode gerenciar usuários", "Administrador", new Guid("248a2ef7-eda1-4a60-987a-32a228a0cdcb") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
