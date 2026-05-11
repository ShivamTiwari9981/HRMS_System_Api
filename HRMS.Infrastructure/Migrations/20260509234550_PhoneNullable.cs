using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhoneNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_Phone",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_Phone",
                table: "User",
                columns: new[] { "ClientId", "Phone" },
                unique: true,
                filter: "[ClientId] IS NOT NULL AND [Phone] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_Phone",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_Phone",
                table: "User",
                columns: new[] { "ClientId", "Phone" },
                unique: true,
                filter: "[ClientId] IS NOT NULL");
        }
    }
}
