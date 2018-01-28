using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PolskieBrowary.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeerGenre",
                columns: table => new
                {
                    BeerGenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenreDescription = table.Column<string>(nullable: true),
                    GenreName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerGenre", x => x.BeerGenreID);
                });

            migrationBuilder.CreateTable(
                name: "Brevery",
                columns: table => new
                {
                    BreveryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BreveryName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Established = table.Column<int>(nullable: false),
                    ProductionCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brevery", x => x.BreveryID);
                });

            migrationBuilder.CreateTable(
                name: "Beer",
                columns: table => new
                {
                    BeerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlcoholContent = table.Column<decimal>(nullable: false),
                    BeerGenreID = table.Column<string>(nullable: true),
                    BeerGenreID1 = table.Column<int>(nullable: true),
                    BreveryID = table.Column<string>(nullable: true),
                    BreveryID1 = table.Column<int>(nullable: true),
                    BreveryName = table.Column<string>(nullable: true),
                    ExtractContent = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beer", x => x.BeerID);
                    table.ForeignKey(
                        name: "FK_Beer_BeerGenre_BeerGenreID1",
                        column: x => x.BeerGenreID1,
                        principalTable: "BeerGenre",
                        principalColumn: "BeerGenreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beer_Brevery_BreveryID1",
                        column: x => x.BreveryID1,
                        principalTable: "Brevery",
                        principalColumn: "BreveryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BeerGenreID1",
                table: "Beer",
                column: "BeerGenreID1");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BreveryID1",
                table: "Beer",
                column: "BreveryID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropTable(
                name: "BeerGenre");

            migrationBuilder.DropTable(
                name: "Brevery");
        }
    }
}
