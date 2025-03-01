using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace memeCoinWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSeen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Message",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Message");
        }
    }
}
