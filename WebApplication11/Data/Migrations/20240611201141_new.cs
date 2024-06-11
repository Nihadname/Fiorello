using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication11.Data.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SliderContent",
                table: "SliderContent");

            migrationBuilder.RenameTable(
                name: "SliderContent",
                newName: "contents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contents",
                table: "contents",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_contents",
                table: "contents");

            migrationBuilder.RenameTable(
                name: "contents",
                newName: "SliderContent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SliderContent",
                table: "SliderContent",
                column: "Id");
        }
    }
}
