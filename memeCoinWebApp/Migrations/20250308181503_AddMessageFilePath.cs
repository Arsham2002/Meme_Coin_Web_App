using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace memeCoinWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageFilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Message",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Message");
        }
    }
}
