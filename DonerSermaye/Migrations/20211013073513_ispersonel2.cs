using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class ispersonel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_isler_personeller",
                table: "isler");

            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "isler",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_isler_personeller",
                table: "isler",
                column: "PersonelId",
                principalTable: "personeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_isler_personeller",
                table: "isler");

            migrationBuilder.AlterColumn<int>(
                name: "PersonelId",
                table: "isler",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_isler_personeller",
                table: "isler",
                column: "PersonelId",
                principalTable: "personeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
