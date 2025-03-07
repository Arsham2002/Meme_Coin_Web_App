using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace memeCoinWebApp.Migrations
{
    /// <inheritdoc />
    public partial class FixTransferSrcDst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_User_UserPhoneNumber",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_UserPhoneNumber",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "UserPhoneNumber",
                table: "Transfer");

            migrationBuilder.AddColumn<string>(
                name: "SourceUserPhoneNumber",
                table: "Transfer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_Destination",
                table: "Transfer",
                column: "Destination");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_SourceUserPhoneNumber",
                table: "Transfer",
                column: "SourceUserPhoneNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_User_Destination",
                table: "Transfer",
                column: "Destination",
                principalTable: "User",
                principalColumn: "PhoneNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_User_SourceUserPhoneNumber",
                table: "Transfer",
                column: "SourceUserPhoneNumber",
                principalTable: "User",
                principalColumn: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_User_Destination",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_User_SourceUserPhoneNumber",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_Destination",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Transfer_SourceUserPhoneNumber",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "SourceUserPhoneNumber",
                table: "Transfer");

            migrationBuilder.AddColumn<string>(
                name: "UserPhoneNumber",
                table: "Transfer",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_UserPhoneNumber",
                table: "Transfer",
                column: "UserPhoneNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_User_UserPhoneNumber",
                table: "Transfer",
                column: "UserPhoneNumber",
                principalTable: "User",
                principalColumn: "PhoneNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
