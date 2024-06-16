using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication11.Data.Migrations
{
    /// <inheritdoc />
    public partial class maxValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "blogs",
                type: "nvarchar(280)",
                maxLength: 280,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "blogs",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(280)",
                oldMaxLength: 280);
        }
    }
}
