using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlayerCQRS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_CreatedAt",
                table: "GamePlayers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_Email",
                table: "GamePlayers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_Role",
                table: "GamePlayers",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_Status",
                table: "GamePlayers",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayers");
        }
    }
}
