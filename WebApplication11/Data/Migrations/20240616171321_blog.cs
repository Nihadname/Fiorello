using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication11.Data.Migrations
{
    /// <inheritdoc />
    public partial class blog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "sliders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "productsImage",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "experts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "expertItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "contents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "abouts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "aboutDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 6, 16, 21, 13, 20, 780, DateTimeKind.Local).AddTicks(6467))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "sliders");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "productsImage");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "products");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "experts");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "expertItems");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "abouts");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "aboutDetails");
        }
    }
}
