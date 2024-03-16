using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class RecipeAddColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ServingsNumber",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Recipes_ServingsNumber_Is_Positive",
                table: "Recipes",
                sql: "\"ServingsNumber\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Recipes_Weight_Is_Positive",
                table: "Recipes",
                sql: "\"Weight\" >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Recipes_ServingsNumber_Is_Positive",
                table: "Recipes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Recipes_Weight_Is_Positive",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ServingsNumber",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Recipes");
        }
    }
}
