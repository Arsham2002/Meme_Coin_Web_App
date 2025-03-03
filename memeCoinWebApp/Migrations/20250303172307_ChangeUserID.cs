using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace memeCoinWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Fund");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Transfer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Message",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Fund",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
