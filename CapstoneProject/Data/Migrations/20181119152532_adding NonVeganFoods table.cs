using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneProject.Data.Migrations
{
    public partial class addingNonVeganFoodstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IngredientsPicture = table.Column<byte[]>(nullable: true),
                    ProductPicture = table.Column<byte[]>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsVegan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonVeganFoods",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    IsQuestionable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonVeganFoods", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "NonVeganFoods");
        }
    }
}
