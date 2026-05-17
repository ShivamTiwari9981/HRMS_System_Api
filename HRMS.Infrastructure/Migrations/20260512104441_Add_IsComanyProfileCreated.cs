using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsComanyProfileCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Client_ClientId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_Phone",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_UserCode",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_UserEmail",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_UserName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsCompanyProfileCreated",
                table: "Client");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompanyProfileCreated",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_Phone",
                table: "User",
                columns: new[] { "ClientId", "Phone" },
                unique: true,
                filter: "[ClientId] IS NOT NULL AND [Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_UserCode",
                table: "User",
                columns: new[] { "ClientId", "UserCode" },
                unique: true,
                filter: "[ClientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_UserEmail",
                table: "User",
                columns: new[] { "ClientId", "UserEmail" },
                unique: true,
                filter: "[ClientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_UserName",
                table: "User",
                columns: new[] { "ClientId", "UserName" },
                unique: true,
                filter: "[ClientId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_Phone",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_UserCode",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_UserEmail",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClientId_UserName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsCompanyProfileCreated",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompanyProfileCreated",
                table: "Client",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_Phone",
                table: "User",
                columns: new[] { "ClientId", "Phone" },
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_UserCode",
                table: "User",
                columns: new[] { "ClientId", "UserCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_UserEmail",
                table: "User",
                columns: new[] { "ClientId", "UserEmail" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ClientId_UserName",
                table: "User",
                columns: new[] { "ClientId", "UserName" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Client_ClientId",
                table: "User",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
