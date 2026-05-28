using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientIdasNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MasterCodeGeneration_ClientId_TableName",
                table: "MasterCodeGeneration");

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "MasterCodeGeneration",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "MasterCodeGeneration",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "MasterCodeGeneration",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCodeGeneration_MasterCodeGenerationId_TableName_Prefix",
                table: "MasterCodeGeneration",
                columns: new[] { "MasterCodeGenerationId", "TableName", "Prefix" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MasterCodeGeneration_MasterCodeGenerationId_TableName_Prefix",
                table: "MasterCodeGeneration");

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "MasterCodeGeneration",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "MasterCodeGeneration",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "MasterCodeGeneration",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterCodeGeneration_ClientId_TableName",
                table: "MasterCodeGeneration",
                columns: new[] { "ClientId", "TableName" },
                unique: true,
                filter: "[TableName] IS NOT NULL");
        }
    }
}
