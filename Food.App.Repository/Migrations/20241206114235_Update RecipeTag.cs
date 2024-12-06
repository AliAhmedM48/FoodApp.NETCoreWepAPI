using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.App.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecipeTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Recipes_RecipeId",
                table: "RecipeTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Tags_TagId",
                table: "RecipeTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag");

            migrationBuilder.RenameTable(
                name: "RecipeTag",
                newName: "RecipeTags");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTag_TagId",
                table: "RecipeTags",
                newName: "IX_RecipeTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTag_RecipeId",
                table: "RecipeTags",
                newName: "IX_RecipeTags_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTags",
                table: "RecipeTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Recipes_RecipeId",
                table: "RecipeTags",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTags_Tags_TagId",
                table: "RecipeTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Recipes_RecipeId",
                table: "RecipeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTags_Tags_TagId",
                table: "RecipeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTags",
                table: "RecipeTags");

            migrationBuilder.RenameTable(
                name: "RecipeTags",
                newName: "RecipeTag");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTags_TagId",
                table: "RecipeTag",
                newName: "IX_RecipeTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTags_RecipeId",
                table: "RecipeTag",
                newName: "IX_RecipeTag_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Recipes_RecipeId",
                table: "RecipeTag",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Tags_TagId",
                table: "RecipeTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
