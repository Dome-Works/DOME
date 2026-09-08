using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dome.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddStacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SocketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ComposeName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProjectName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ComposeYaml = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stacks_Sockets_SocketId",
                        column: x => x.SocketId,
                        principalTable: "Sockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stacks_SocketId_ProjectName",
                table: "Stacks",
                columns: new[] { "SocketId", "ProjectName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stacks");
        }
    }
}
