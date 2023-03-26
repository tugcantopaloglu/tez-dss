using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class ispersonel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonelId",
                table: "isler",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_isler_PersonelId",
                table: "isler",
                column: "PersonelId");

            migrationBuilder.AddForeignKey(
                name: "FK_isler_personeller",
                table: "isler",
                column: "PersonelId",
                principalTable: "personeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_isler_personeller",
                table: "isler");

            migrationBuilder.DropIndex(
                name: "IX_isler_PersonelId",
                table: "isler");

            migrationBuilder.DropColumn(
                name: "PersonelId",
                table: "isler");
        }
    }
}
