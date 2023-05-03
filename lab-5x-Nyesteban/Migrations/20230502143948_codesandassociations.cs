using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace lab_5x_Nyesteban.Migrations
{
    /// <inheritdoc />
    public partial class codesandassociations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Games",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "DevelopmentDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Companies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Apps",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegistrationCodes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    generatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationCodes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserID",
                table: "Games",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentDetails_UserID",
                table: "DevelopmentDetails",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserID",
                table: "Companies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_UserID",
                table: "Apps",
                column: "UserID");

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

            migrationBuilder.DropTable(
                name: "RegistrationCodes");

            migrationBuilder.DropIndex(
                name: "IX_Games_UserID",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_DevelopmentDetails_UserID",
                table: "DevelopmentDetails");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserID",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Apps_UserID",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "DevelopmentDetails");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Apps");
        }
    }
}
