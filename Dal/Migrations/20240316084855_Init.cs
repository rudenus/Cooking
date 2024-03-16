using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<byte[]>(type: "bytea", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                    table.CheckConstraint("CK_Files_Type_Has_Allowed_Values", "\"Type\" in ('Photo','Video')");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Calories = table.Column<int>(type: "integer", nullable: false),
                    IsModerated = table.Column<bool>(type: "boolean", nullable: false),
                    Proteins = table.Column<int>(type: "integer", nullable: false),
                    Fats = table.Column<int>(type: "integer", nullable: false),
                    Carbohydrates = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.CheckConstraint("CK_Products_Calories_Is_Positive", "\"Calories\" >= 0");
                    table.CheckConstraint("CK_Products_Carbohydrates_Is_Positive", "\"Carbohydrates\" >= 0");
                    table.CheckConstraint("CK_Products_Fats_Is_Positive", "\"Fats\" >= 0");
                    table.CheckConstraint("CK_Products_Proteins_Is_Positive", "\"Proteins\" >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    IsBanned = table.Column<bool>(type: "boolean", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.CheckConstraint("CK_Users_Role_Has_Allowed_Values", "\"Role\" in ('Default','Moderator')");
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Calories = table.Column<int>(type: "integer", nullable: false),
                    Carbohydrates = table.Column<int>(type: "integer", nullable: false),
                    Fats = table.Column<int>(type: "integer", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsModerated = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Proteins = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                    table.CheckConstraint("CK_Recipes_Calories_Is_Positive", "\"Calories\" >= 0");
                    table.CheckConstraint("CK_Recipes_Carbohydrates_Is_Positive", "\"Carbohydrates\" >= 0");
                    table.CheckConstraint("CK_Recipes_Fats_Is_Positive", "\"Fats\" >= 0");
                    table.CheckConstraint("CK_Recipes_Proteins_Is_Positive", "\"Proteins\" >= 0");
                    table.ForeignKey(
                        name: "FK_Recipes_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "FileId");
                    table.ForeignKey(
                        name: "FK_Recipes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingridients",
                columns: table => new
                {
                    IngridientId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingridients", x => x.IngridientId);
                    table.CheckConstraint("CK_Ingridients_Weight_Is_Positive", "\"Weight\" >= 0");
                    table.ForeignKey(
                        name: "FK_Ingridients_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingridients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Step = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TimeInSeconds = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => new { x.RecipeId, x.Step });
                    table.CheckConstraint("CK_Operations_Step_Is_Positive", "\"Step\" >= 0");
                    table.CheckConstraint("CK_Operations_TimeInSeconds_Is_Positive", "\"TimeInSeconds\" >= 0");
                    table.ForeignKey(
                        name: "FK_Operations_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    PublicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    CommentsNumber = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    LikesNumber = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.PublicationId);
                    table.CheckConstraint("CK_Publications_CommentsNumber_Is_Positive", "\"CommentsNumber\" >= 0");
                    table.CheckConstraint("CK_Publications_LikesNumber_Is_Positive", "\"LikesNumber\" >= 0");
                    table.ForeignKey(
                        name: "FK_Publications_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "FileId");
                    table.ForeignKey(
                        name: "FK_Publications_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Publications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublicationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "PublicationId");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikeId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublicationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_Likes_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "PublicationId");
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PublicationId",
                table: "Comments",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_ProductId",
                table: "Ingridients",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_RecipeId",
                table: "Ingridients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PublicationId",
                table: "Likes",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_RecipeId",
                table: "Operations",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_FileId",
                table: "Publications",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_RecipeId",
                table: "Publications",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_UserId",
                table: "Publications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FileId",
                table: "Recipes",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "UserId", "PasswordHash" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Ingridients");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
