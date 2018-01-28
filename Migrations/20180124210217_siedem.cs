using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PolskieBrowary.Migrations
{
    public partial class siedem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Beer_BeerId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "BeerId",
                table: "Comment",
                newName: "CommentedBeerId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_BeerId",
                table: "Comment",
                newName: "IX_Comment_CommentedBeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Beer_CommentedBeerId",
                table: "Comment",
                column: "CommentedBeerId",
                principalTable: "Beer",
                principalColumn: "BeerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Beer_CommentedBeerId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "CommentedBeerId",
                table: "Comment",
                newName: "BeerId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CommentedBeerId",
                table: "Comment",
                newName: "IX_Comment_BeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Beer_BeerId",
                table: "Comment",
                column: "BeerId",
                principalTable: "Beer",
                principalColumn: "BeerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
