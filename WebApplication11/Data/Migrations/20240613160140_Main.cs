using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication11.Data.Migrations
{
    /// <inheritdoc />
    public partial class Main : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "productsImage",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "productsImage");
        }
    }
}
