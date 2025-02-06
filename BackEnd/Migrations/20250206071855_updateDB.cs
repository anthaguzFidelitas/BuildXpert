using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "test",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserGoogleIDUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmails_test_testUserId",
                table: "UserEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhones_test_testUserId",
                table: "UserPhones");

            migrationBuilder.DropTable(
                name: "test");

            migrationBuilder.DropIndex(
                name: "IX_UserPhones_testUserId",
                table: "UserPhones");

            migrationBuilder.DropIndex(
                name: "IX_UserEmails_testUserId",
                table: "UserEmails");

            migrationBuilder.DropColumn(
                name: "testUserId",
                table: "UserPhones");

            migrationBuilder.DropColumn(
                name: "testUserId",
                table: "UserEmails");
        }
    }
}
