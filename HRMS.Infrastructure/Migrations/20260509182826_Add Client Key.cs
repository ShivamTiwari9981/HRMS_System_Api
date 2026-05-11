using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClientKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Client_ClientCode",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ClientCode",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "ClientKey",
                table: "User",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientKey",
                table: "Client",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientKey",
                table: "Client",
                column: "ClientKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Client_ClientKey",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientKey",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ClientKey",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "ClientCode",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientCode",
                table: "Client",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientCode",
                table: "Client",
                column: "ClientCode",
                unique: true);
        }
    }
}
