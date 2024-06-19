using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class setAsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ServingsNumber",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Proteins",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Fats",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Carbohydrates",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Proteins",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Fats",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Carbohydrates",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Ingridients",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProteinsPer100",
                table: "Recipes",
                type: "integer",
                nullable: true,
                computedColumnSql: "\"Proteins\" * 100 /\"Weight\"",
                stored: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComputedColumnSql: "\"Proteins\" * 100 /\"Weight\"",
                oldStored: true);

            migrationBuilder.AlterColumn<int>(
                name: "FatsPer100",
                table: "Recipes",
                type: "integer",
                nullable: true,
                computedColumnSql: "\"Fats\" * 100 /\"Weight\"",
                stored: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComputedColumnSql: "\"Fats\" * 100 /\"Weight\"",
                oldStored: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarbohydratesPer100",
                table: "Recipes",
                type: "integer",
                nullable: true,
                computedColumnSql: "\"Carbohydrates\" * 100 /\"Weight\"",
                stored: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComputedColumnSql: "\"Carbohydrates\" * 100 /\"Weight\"",
                oldStored: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaloriesPer100",
                table: "Recipes",
                type: "integer",
                nullable: true,
                computedColumnSql: "\"Calories\" * 100 /\"Weight\"",
                stored: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComputedColumnSql: "\"Calories\" * 100 /\"Weight\"",
                oldStored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServingsNumber",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Proteins",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Fats",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Carbohydrates",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Proteins",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Fats",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Carbohydrates",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Ingridients",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProteinsPer100",
                table: "Recipes",
                type: "integer",
                nullable: false,
                computedColumnSql: "\"Proteins\" * 100 /\"Weight\"",
                stored: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComputedColumnSql: "\"Proteins\" * 100 /\"Weight\"",
                oldStored: true);

            migrationBuilder.AlterColumn<int>(
                name: "FatsPer100",
                table: "Recipes",
                type: "integer",
                nullable: false,
                computedColumnSql: "\"Fats\" * 100 /\"Weight\"",
                stored: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComputedColumnSql: "\"Fats\" * 100 /\"Weight\"",
                oldStored: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarbohydratesPer100",
                table: "Recipes",
                type: "integer",
                nullable: false,
                computedColumnSql: "\"Carbohydrates\" * 100 /\"Weight\"",
                stored: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComputedColumnSql: "\"Carbohydrates\" * 100 /\"Weight\"",
                oldStored: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaloriesPer100",
                table: "Recipes",
                type: "integer",
                nullable: false,
                computedColumnSql: "\"Calories\" * 100 /\"Weight\"",
                stored: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComputedColumnSql: "\"Calories\" * 100 /\"Weight\"",
                oldStored: true);
        }
    }
}
