using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_context : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadingEntities_ScaleEntities_ScaleId",
                table: "ReadingEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScaleEntities",
                table: "ScaleEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingEntities",
                table: "ReadingEntities");

            migrationBuilder.RenameTable(
                name: "ScaleEntities",
                newName: "Scale");

            migrationBuilder.RenameTable(
                name: "ReadingEntities",
                newName: "Reading");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingEntities_ScaleId",
                table: "Reading",
                newName: "IX_Reading_ScaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scale",
                table: "Scale",
                column: "ScaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reading",
                table: "Reading",
                column: "ReadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reading_Scale_ScaleId",
                table: "Reading",
                column: "ScaleId",
                principalTable: "Scale",
                principalColumn: "ScaleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reading_Scale_ScaleId",
                table: "Reading");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scale",
                table: "Scale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reading",
                table: "Reading");

            migrationBuilder.RenameTable(
                name: "Scale",
                newName: "ScaleEntities");

            migrationBuilder.RenameTable(
                name: "Reading",
                newName: "ReadingEntities");

            migrationBuilder.RenameIndex(
                name: "IX_Reading_ScaleId",
                table: "ReadingEntities",
                newName: "IX_ReadingEntities_ScaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScaleEntities",
                table: "ScaleEntities",
                column: "ScaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingEntities",
                table: "ReadingEntities",
                column: "ReadId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingEntities_ScaleEntities_ScaleId",
                table: "ReadingEntities",
                column: "ScaleId",
                principalTable: "ScaleEntities",
                principalColumn: "ScaleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
