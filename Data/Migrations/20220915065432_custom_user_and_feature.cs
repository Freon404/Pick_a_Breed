using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pick_a_Breed.Data.Migrations
{
    public partial class custom_user_and_feature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Breed",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Breedid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feature_Breed_Breedid",
                        column: x => x.Breedid,
                        principalTable: "Breed",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Breed_ApplicationUserId",
                table: "Breed",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_Breedid",
                table: "Feature",
                column: "Breedid");

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_AspNetUsers_ApplicationUserId",
                table: "Breed",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breed_AspNetUsers_ApplicationUserId",
                table: "Breed");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Breed_ApplicationUserId",
                table: "Breed");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Breed");
        }
    }
}
