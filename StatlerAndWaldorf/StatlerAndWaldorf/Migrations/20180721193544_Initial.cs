using Microsoft.EntityFrameworkCore.Migrations;

namespace StatlerAndWaldorf.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "movieId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_movieId",
                table: "Reviews",
                column: "movieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_userId",
                table: "Reviews",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_movieId",
                table: "Reviews",
                column: "movieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_userId",
                table: "Reviews",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_movieId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_userId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_movieId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_userId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "movieId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
