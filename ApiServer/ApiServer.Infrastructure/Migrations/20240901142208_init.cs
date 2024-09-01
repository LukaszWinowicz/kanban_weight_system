using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scale",
                columns: table => new
                {
                    ScaleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SingleItemWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scale", x => x.ScaleName);
                });

            migrationBuilder.CreateTable(
                name: "Reading",
                columns: table => new
                {
                    ReadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScaleName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reading", x => x.ReadId);
                    table.ForeignKey(
                        name: "FK_Reading_Scale_ScaleName",
                        column: x => x.ScaleName,
                        principalTable: "Scale",
                        principalColumn: "ScaleName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reading_ScaleName",
                table: "Reading",
                column: "ScaleName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reading");

            migrationBuilder.DropTable(
                name: "Scale");
        }
    }
}
