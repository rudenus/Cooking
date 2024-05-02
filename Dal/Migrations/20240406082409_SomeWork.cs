using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class SomeWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Publications_PublicationId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Users_UserId",
                table: "Publications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_PublicationId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Publications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicationId",
                table: "Likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicationId",
                table: "Comments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "PublicationId", "UserId" });

            migrationBuilder.CreateTable(
                name: "ReplacementProducts",
                columns: table => new
                {
                    ReplacementId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReplacingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReplacementLevel = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplacementProducts", x => new { x.ReplacingId, x.ReplacementId });
                    table.CheckConstraint("CK_ReplacementProducts_ReplacementLevel_Has_Allowed_Values", "\"ReplacementLevel\" in ('Low','Medium','Hard')");
                    table.ForeignKey(
                        name: "FK_ReplacementProducts_Products_ReplacementId",
                        column: x => x.ReplacementId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReplacementProducts_Products_ReplacingId",
                        column: x => x.ReplacingId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplacementProducts_ReplacementId",
                table: "ReplacementProducts",
                column: "ReplacementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Publications_PublicationId",
                table: "Likes",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Users_UserId",
                table: "Publications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Publications_PublicationId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Users_UserId",
                table: "Publications");

            migrationBuilder.DropTable(
                name: "ReplacementProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Publications",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicationId",
                table: "Likes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "LikeId",
                table: "Likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PublicationId",
                table: "Comments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "LikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PublicationId",
                table: "Likes",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Publications_PublicationId",
                table: "Likes",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Users_UserId",
                table: "Publications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
