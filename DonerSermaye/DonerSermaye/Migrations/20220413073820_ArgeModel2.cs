using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class ArgeModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmaId",
                table: "arge",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ProjeTutari",
                table: "arge",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_arge_FirmaId",
                table: "arge",
                column: "FirmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_arge_firma",
                table: "arge",
                column: "FirmaId",
                principalTable: "firmalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_arge_firma",
                table: "arge");

            migrationBuilder.DropIndex(
                name: "IX_arge_FirmaId",
                table: "arge");

            migrationBuilder.DropColumn(
                name: "FirmaId",
                table: "arge");

            migrationBuilder.DropColumn(
                name: "ProjeTutari",
                table: "arge");
        }
    }
}
