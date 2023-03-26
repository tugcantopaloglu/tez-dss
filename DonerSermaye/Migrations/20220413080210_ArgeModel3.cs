using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class ArgeModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonelId",
                table: "arge",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_arge_PersonelId",
                table: "arge",
                column: "PersonelId");

            migrationBuilder.AddForeignKey(
                name: "FK_arge_personel",
                table: "arge",
                column: "PersonelId",
                principalTable: "personeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_arge_personel",
                table: "arge");

            migrationBuilder.DropIndex(
                name: "IX_arge_PersonelId",
                table: "arge");

            migrationBuilder.DropColumn(
                name: "PersonelId",
                table: "arge");
        }
    }
}
