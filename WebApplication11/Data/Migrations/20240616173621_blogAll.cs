using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication11.Data.Migrations
{
    /// <inheritdoc />
    public partial class blogAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "blogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "blogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 16, 21, 36, 21, 326, DateTimeKind.Local).AddTicks(3799),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 16, 21, 13, 20, 780, DateTimeKind.Local).AddTicks(6467));

            migrationBuilder.AddPrimaryKey(
                name: "PK_blogs",
                table: "blogs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_blogs",
                table: "blogs");

            migrationBuilder.RenameTable(
                name: "blogs",
                newName: "Blog");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Blog",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 16, 21, 13, 20, 780, DateTimeKind.Local).AddTicks(6467),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 16, 21, 36, 21, 326, DateTimeKind.Local).AddTicks(3799));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");
        }
    }
}
