using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatealldb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScaleConfigurations");

            migrationBuilder.DropTable(
                name: "SensorReadings");

            migrationBuilder.CreateTable(
                name: "ScaleEntities",
                columns: table => new
                {
                    ScaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScaleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SingleItemWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScaleEntities", x => x.ScaleId);
                });

            migrationBuilder.CreateTable(
                name: "ReadingEntities",
                columns: table => new
                {
                    ReadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScaleId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingEntities", x => x.ReadId);
                    table.ForeignKey(
                        name: "FK_ReadingEntities_ScaleEntities_ScaleId",
                        column: x => x.ScaleId,
                        principalTable: "ScaleEntities",
                        principalColumn: "ScaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReadingEntities_ScaleId",
                table: "ReadingEntities",
                column: "ScaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadingEntities");

            migrationBuilder.DropTable(
                name: "ScaleEntities");

            migrationBuilder.CreateTable(
                name: "ScaleConfigurations",
                columns: table => new
                {
                    EspId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false),
                    ItemNumber = table.Column<int>(type: "int", nullable: false),
                    SingleItemWeight = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScaleConfigurations", x => x.EspId);
                });

            migrationBuilder.CreateTable(
                name: "SensorReadings",
                columns: table => new
                {
                    SensorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EspId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EspName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorReadings", x => x.SensorId);
                });
        }
    }
}
