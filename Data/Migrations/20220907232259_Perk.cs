using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pick_a_Breed.Data.Migrations
{
    public partial class Perk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Breedid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perk_Breed_Breedid",
                        column: x => x.Breedid,
                        principalTable: "Breed",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perk_Breedid",
                table: "Perk",
                column: "Breedid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perk");
        }
    }
}
