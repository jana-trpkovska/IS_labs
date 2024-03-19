using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheatreShows.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TheatreShow_TheatreShowId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_TheatreShowId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TheatreShowId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TheatreShowName",
                table: "Ticket");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TheatreShowId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TheatreShowName",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TheatreShowId",
                table: "Ticket",
                column: "TheatreShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TheatreShow_TheatreShowId",
                table: "Ticket",
                column: "TheatreShowId",
                principalTable: "TheatreShow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
