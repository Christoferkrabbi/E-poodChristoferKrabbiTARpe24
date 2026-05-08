using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userAccounts",
                table: "userAccounts");

            migrationBuilder.RenameTable(
                name: "userAccounts",
                newName: "UserAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_userAccounts_UserName",
                table: "UserAccounts",
                newName: "IX_UserAccounts_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_userAccounts_Email",
                table: "UserAccounts",
                newName: "IX_UserAccounts_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts");

            migrationBuilder.RenameTable(
                name: "UserAccounts",
                newName: "userAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_UserAccounts_UserName",
                table: "userAccounts",
                newName: "IX_userAccounts_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_UserAccounts_Email",
                table: "userAccounts",
                newName: "IX_userAccounts_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userAccounts",
                table: "userAccounts",
                column: "Id");
        }
    }
}
