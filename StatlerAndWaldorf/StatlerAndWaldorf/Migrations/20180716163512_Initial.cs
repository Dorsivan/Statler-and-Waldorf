using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StatlerAndWaldorf.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(nullable: false),
                    review = table.Column<string>(nullable: true),
                    isBlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Movies",
                nullable: true);
        }
    }
}
