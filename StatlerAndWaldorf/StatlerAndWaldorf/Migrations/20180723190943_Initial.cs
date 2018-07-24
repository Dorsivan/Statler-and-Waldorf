using Microsoft.EntityFrameworkCore.Migrations;

namespace StatlerAndWaldorf.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "movieId",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MoviesId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MoviesId",
                table: "Reviews",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_MoviesId",
                table: "Reviews",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_MoviesId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_MoviesId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MoviesId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "movieId",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
