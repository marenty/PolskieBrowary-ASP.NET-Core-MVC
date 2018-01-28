using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PolskieBrowary.Migrations
{
    public partial class Druga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerGenre_BeerGenreID1",
                table: "Beer");

            migrationBuilder.DropForeignKey(
                name: "FK_Beer_Brevery_BreveryID1",
                table: "Beer");

            migrationBuilder.DropIndex(
                name: "IX_Beer_BeerGenreID1",
                table: "Beer");

            migrationBuilder.DropIndex(
                name: "IX_Beer_BreveryID1",
                table: "Beer");

            migrationBuilder.DropColumn(
                name: "BeerGenreID1",
                table: "Beer");

            migrationBuilder.DropColumn(
                name: "BreveryID1",
                table: "Beer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Brevery",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Brevery",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BreveryName",
                table: "Brevery",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenreName",
                table: "BeerGenre",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenreDescription",
                table: "BeerGenre",
                maxLength: 1500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BreveryName",
                table: "Beer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BreveryID",
                table: "Beer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BeerGenreID",
                table: "Beer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BeerGenreID",
                table: "Beer",
                column: "BeerGenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BreveryID",
                table: "Beer",
                column: "BreveryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerGenre_BeerGenreID",
                table: "Beer",
                column: "BeerGenreID",
                principalTable: "BeerGenre",
                principalColumn: "BeerGenreID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_Brevery_BreveryID",
                table: "Beer",
                column: "BreveryID",
                principalTable: "Brevery",
                principalColumn: "BreveryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerGenre_BeerGenreID",
                table: "Beer");

            migrationBuilder.DropForeignKey(
                name: "FK_Beer_Brevery_BreveryID",
                table: "Beer");

            migrationBuilder.DropIndex(
                name: "IX_Beer_BeerGenreID",
                table: "Beer");

            migrationBuilder.DropIndex(
                name: "IX_Beer_BreveryID",
                table: "Beer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Brevery",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Brevery",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BreveryName",
                table: "Brevery",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenreName",
                table: "BeerGenre",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenreDescription",
                table: "BeerGenre",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BreveryName",
                table: "Beer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "BreveryID",
                table: "Beer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "BeerGenreID",
                table: "Beer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BeerGenreID1",
                table: "Beer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BreveryID1",
                table: "Beer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BeerGenreID1",
                table: "Beer",
                column: "BeerGenreID1");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BreveryID1",
                table: "Beer",
                column: "BreveryID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerGenre_BeerGenreID1",
                table: "Beer",
                column: "BeerGenreID1",
                principalTable: "BeerGenre",
                principalColumn: "BeerGenreID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_Brevery_BreveryID1",
                table: "Beer",
                column: "BreveryID1",
                principalTable: "Brevery",
                principalColumn: "BreveryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
