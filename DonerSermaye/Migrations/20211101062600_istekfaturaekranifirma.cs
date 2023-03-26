using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class istekfaturaekranifirma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmaId",
                table: "istekler",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_istekler_FirmaId",
                table: "istekler",
                column: "FirmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_istekler_firmalar",
                table: "istekler",
                column: "FirmaId",
                principalTable: "firmalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_istekler_firmalar",
                table: "istekler");

            migrationBuilder.DropIndex(
                name: "IX_istekler_FirmaId",
                table: "istekler");

            migrationBuilder.DropColumn(
                name: "FirmaId",
                table: "istekler");
        }
    }
}
