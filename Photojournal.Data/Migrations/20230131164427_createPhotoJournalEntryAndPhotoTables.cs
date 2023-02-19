using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photojournal.Data.Migrations
{
    /// <inheritdoc />
    public partial class createPhotoJournalEntryAndPhotoTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_PhotoblogEntries_PhotoblogEntryId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "PhotoblogEntries");

            migrationBuilder.RenameColumn(
                name: "PhotoblogEntryId",
                table: "Photos",
                newName: "JournalEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_PhotoblogEntryId",
                table: "Photos",
                newName: "IX_Photos_JournalEntryId");

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_JournalEntries_JournalEntryId",
                table: "Photos",
                column: "JournalEntryId",
                principalTable: "JournalEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_JournalEntries_JournalEntryId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "JournalEntries");

            migrationBuilder.RenameColumn(
                name: "JournalEntryId",
                table: "Photos",
                newName: "PhotoblogEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_JournalEntryId",
                table: "Photos",
                newName: "IX_Photos_PhotoblogEntryId");

            migrationBuilder.CreateTable(
                name: "PhotoblogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoblogEntries", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_PhotoblogEntries_PhotoblogEntryId",
                table: "Photos",
                column: "PhotoblogEntryId",
                principalTable: "PhotoblogEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
