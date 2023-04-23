using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab_5x_Nyesteban.Migrations
{
    /// <inheritdoc />
    public partial class AppRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppRating",
                table: "Apps",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppRating",
                table: "Apps");
        }
    }
}
