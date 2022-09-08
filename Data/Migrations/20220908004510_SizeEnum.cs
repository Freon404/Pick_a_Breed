using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pick_a_Breed.Data.Migrations
{
    public partial class SizeEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perk");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Breed",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Breed",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Perk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Breedid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
