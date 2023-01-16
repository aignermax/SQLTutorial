using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedWalkDifficulty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WalkDificulty_WalkDifficultyId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalkDificulty",
                table: "WalkDificulty");

            migrationBuilder.RenameTable(
                name: "WalkDificulty",
                newName: "WalkDifficulty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalkDifficulty",
                table: "WalkDifficulty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WalkDifficulty_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId",
                principalTable: "WalkDifficulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WalkDifficulty_WalkDifficultyId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalkDifficulty",
                table: "WalkDifficulty");

            migrationBuilder.RenameTable(
                name: "WalkDifficulty",
                newName: "WalkDificulty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalkDificulty",
                table: "WalkDificulty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WalkDificulty_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId",
                principalTable: "WalkDificulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
