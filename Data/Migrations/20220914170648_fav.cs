using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pick_a_Breed.Data.Migrations
{
    public partial class fav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favourite",
                table: "Breed",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favourite",
                table: "Breed");
        }
    }
}
