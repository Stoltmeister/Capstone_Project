using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneProject.Data.Migrations
{
    public partial class vegansubtitutestableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeganSubstitutes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    NonVeganIngredient = table.Column<string>(nullable: true),
                    VeganSubsDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeganSubstitutes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeganSubstitutes");
        }
    }
}
