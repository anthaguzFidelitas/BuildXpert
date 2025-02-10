using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Userclassupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmails_Users_UserId",
                table: "UserEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEmails_test_testUserId",
                table: "UserEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhones_test_testUserId",
                table: "UserPhones");

            migrationBuilder.DropTable(
                name: "test");

            migrationBuilder.DropTable(
                name: "UserGoogleIDs");

            migrationBuilder.DropIndex(
                name: "IX_UserPhones_testUserId",
                table: "UserPhones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEmails",
                table: "UserEmails");

            migrationBuilder.DropIndex(
                name: "IX_UserEmails_testUserId",
                table: "UserEmails");

            migrationBuilder.DropColumn(
                name: "testUserId",
                table: "UserPhones");

            migrationBuilder.DropColumn(
                name: "testUserId",
                table: "UserEmails");

            migrationBuilder.RenameTable(
                name: "UserEmails",
                newName: "UserEmail");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GoogleID",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEmail",
                table: "UserEmail",
                columns: new[] { "UserId", "Email" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmail_Users_UserId",
                table: "UserEmail",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmail_Users_UserId",
                table: "UserEmail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEmail",
                table: "UserEmail");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GoogleID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "UserEmail",
                newName: "UserEmails");

            migrationBuilder.AddColumn<string>(
                name: "testUserId",
                table: "UserPhones",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "testUserId",
                table: "UserEmails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEmails",
                table: "UserEmails",
                columns: new[] { "UserId", "Email" });

            migrationBuilder.CreateTable(
                name: "UserGoogleIDs",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GoogleID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGoogleIDs", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserGoogleIDs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "test",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserGoogleIDUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_test_UserGoogleIDs_UserGoogleIDUserId",
                        column: x => x.UserGoogleIDUserId,
                        principalTable: "UserGoogleIDs",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPhones_testUserId",
                table: "UserPhones",
                column: "testUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmails_testUserId",
                table: "UserEmails",
                column: "testUserId");

            migrationBuilder.CreateIndex(
                name: "IX_test_UserGoogleIDUserId",
                table: "test",
                column: "UserGoogleIDUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmails_Users_UserId",
                table: "UserEmails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmails_test_testUserId",
                table: "UserEmails",
                column: "testUserId",
                principalTable: "test",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhones_test_testUserId",
                table: "UserPhones",
                column: "testUserId",
                principalTable: "test",
                principalColumn: "UserId");
        }
    }
}
