using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab_5x_Nyesteban.Migrations
{
    /// <inheritdoc />
    public partial class show_count : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowCount",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowCount",
                table: "Users");
        }
    }
}
