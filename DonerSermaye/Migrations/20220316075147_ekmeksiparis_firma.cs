using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class ekmeksiparis_firma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ekmekSiparis_personeller",
                table: "ekmekSiparis");

            migrationBuilder.AddForeignKey(
                name: "FK_ekmekSiparis_personeller",
                table: "ekmekSiparis",
                column: "FirmaId",
                principalTable: "firmalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ekmekSiparis_personeller",
                table: "ekmekSiparis");

            migrationBuilder.AddForeignKey(
                name: "FK_ekmekSiparis_personeller",
                table: "ekmekSiparis",
                column: "FirmaId",
                principalTable: "personeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
