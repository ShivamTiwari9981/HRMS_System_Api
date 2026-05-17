using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NullableUserCodeInUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_UserCode",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "UserCode",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_UserCode",
                table: "User",
                columns: new[] { "ClientId", "UserCode" },
                unique: true,
                filter: "[ClientId] IS NOT NULL AND [UserCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_UserCode",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "UserCode",
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
                name: "IX_User_ClientId_UserCode",
                table: "User",
                columns: new[] { "ClientId", "UserCode" },
                unique: true,
                filter: "[ClientId] IS NOT NULL");
        }
    }
}
