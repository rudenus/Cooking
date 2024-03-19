using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class CaloriesEtcPer100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaloriesPer100",
                table: "Recipes",
                type: "integer",
                nullable: false,
                computedColumnSql: "\"Calories\" * 100 /\"Weight\"",
                stored: true);

            migrationBuilder.AddColumn<int>(
                name: "CarbohydratesPer100",
                table: "Recipes",
                type: "integer",
                nullable: false,
                computedColumnSql: "\"Carbohydrates\" * 100 /\"Weight\"",
                stored: true);

            migrationBuilder.AddColumn<int>(
                name: "FatsPer100",
                table: "Recipes",
                type: "integer",
                nullable: false,
                computedColumnSql: "\"Fats\" * 100 /\"Weight\"",
                stored: true);

            migrationBuilder.AddColumn<int>(
                name: "ProteinsPer100",
                table: "Recipes",
                type: "integer",
                nullable: false,
                computedColumnSql: "\"Proteins\" * 100 /\"Weight\"",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesPer100",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CarbohydratesPer100",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "FatsPer100",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProteinsPer100",
                table: "Recipes");
        }
    }
}
