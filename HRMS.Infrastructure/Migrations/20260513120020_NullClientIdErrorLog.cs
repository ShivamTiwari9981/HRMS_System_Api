using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NullClientIdErrorLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLog_Client_ClientId",
                table: "ErrorLog");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLog_ClientId",
                table: "ErrorLog");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "ErrorLog",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLog_ClientId",
                table: "ErrorLog",
                column: "ClientId",
                unique: true,
                filter: "[ClientId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ErrorLog_ClientId",
                table: "ErrorLog");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "ErrorLog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLog_ClientId",
                table: "ErrorLog",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLog_Client_ClientId",
                table: "ErrorLog",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
