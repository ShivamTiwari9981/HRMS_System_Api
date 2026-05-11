using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_client_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Client_ClientName",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_Phone",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Client",
                newName: "CompanyEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Client_Email",
                table: "Client",
                newName: "IX_Client_CompanyEmail");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Client",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Client",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "Client",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileCompleted",
                table: "Client",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Client_CompanyName",
                table: "Client",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Phone",
                table: "Client",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Client_CompanyName",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_Phone",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "IsProfileCompleted",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "CompanyEmail",
                table: "Client",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_Client_CompanyEmail",
                table: "Client",
                newName: "IX_Client_Email");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Client",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Client",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
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
                name: "IX_Client_ClientName",
                table: "Client",
                column: "ClientName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Phone",
                table: "Client",
                column: "Phone",
                unique: true);
        }
    }
}
