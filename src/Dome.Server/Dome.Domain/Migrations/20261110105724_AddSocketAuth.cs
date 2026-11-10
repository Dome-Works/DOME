using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dome.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddSocketAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificateFingerprint",
                table: "Sockets",
                type: "TEXT",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProtectedApiKey",
                table: "Sockets",
                type: "TEXT",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE Sockets SET Address = 'https://' || substr(Address, 8) WHERE Address LIKE 'http://%';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateFingerprint",
                table: "Sockets");

            migrationBuilder.DropColumn(
                name: "ProtectedApiKey",
                table: "Sockets");
        }
    }
}
