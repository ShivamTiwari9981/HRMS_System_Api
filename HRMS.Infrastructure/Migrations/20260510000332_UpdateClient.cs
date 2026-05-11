using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClient : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_Client_CompanyEmail",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_CompanyName",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientKey",
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

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Client",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyEmail",
                table: "Client",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Client",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

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

            migrationBuilder.CreateIndex(
                name: "IX_Client_CompanyEmail",
                table: "Client",
                column: "CompanyEmail",
                unique: true,
                filter: "[CompanyEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Client_CompanyName",
                table: "Client",
                column: "CompanyName",
                unique: true,
                filter: "[CompanyName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Client_ClientId",
                table: "User",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Client_CompanyEmail",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_CompanyName",
                table: "Client");

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

            migrationBuilder.AddColumn<string>(
                name: "ClientKey",
                table: "User",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompanyProfileCreated",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Client",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyEmail",
                table: "Client",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Client",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Client_CompanyEmail",
                table: "Client",
                column: "CompanyEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_CompanyName",
                table: "Client",
                column: "CompanyName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Client_ClientId",
                table: "User",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId");
        }
    }
}
