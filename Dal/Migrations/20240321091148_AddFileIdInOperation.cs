using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddFileIdInOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "Operations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FileId",
                table: "Operations",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Files_FileId",
                table: "Operations",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Files_FileId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_FileId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Operations");
        }
    }
}
