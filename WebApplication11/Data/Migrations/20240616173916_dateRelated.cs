using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication11.Data.Migrations
{
    /// <inheritdoc />
    public partial class dateRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "blogs",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 16, 21, 36, 21, 326, DateTimeKind.Local).AddTicks(3799));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "blogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 16, 21, 36, 21, 326, DateTimeKind.Local).AddTicks(3799),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
