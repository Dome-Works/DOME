using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dome.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddImageVersionCache : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageVersionCaches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ImageReference = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    RemoteDigest = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    RemoteTags = table.Column<string>(type: "TEXT", nullable: false),
                    Unsupported = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastError = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true),
                    CheckedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageVersionCaches", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageVersionCaches_ImageReference",
                table: "ImageVersionCaches",
                column: "ImageReference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageVersionCaches");
        }
    }
}
