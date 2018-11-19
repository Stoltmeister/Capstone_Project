using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneProject.Data.Migrations
{
    public partial class removingisQuestionablefromnonveganfoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsQuestionable",
                table: "NonVeganFoods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsQuestionable",
                table: "NonVeganFoods",
                nullable: false,
                defaultValue: false);
        }
    }
}
