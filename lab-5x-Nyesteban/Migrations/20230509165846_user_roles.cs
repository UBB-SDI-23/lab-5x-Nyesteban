using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab_5x_Nyesteban.Migrations
{
    /// <inheritdoc />
    public partial class user_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_Users_UserID",
                table: "Apps");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_DevelopmentDetails_Users_UserID",
                table: "DevelopmentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_UserID",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "DevelopmentDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Apps",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_Users_UserID",
                table: "Apps",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserID",
                table: "Companies",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DevelopmentDetails_Users_UserID",
                table: "DevelopmentDetails",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_UserID",
                table: "Games",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_Users_UserID",
                table: "Apps");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_DevelopmentDetails_Users_UserID",
                table: "DevelopmentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_UserID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Games",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "DevelopmentDetails",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Companies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Apps",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_Users_UserID",
                table: "Apps",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserID",
                table: "Companies",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DevelopmentDetails_Users_UserID",
                table: "DevelopmentDetails",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_UserID",
                table: "Games",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}
