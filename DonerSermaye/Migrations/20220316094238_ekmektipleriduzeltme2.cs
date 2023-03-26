using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class ekmektipleriduzeltme2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ekmekSiparis_ekmekTipleri",
                table: "ekmekSiparis");

            migrationBuilder.AddForeignKey(
                name: "FK_ekmekSiparis_ekmektipleri",
                table: "ekmekSiparis",
                column: "EkmekTipleriId",
                principalTable: "ekmekTipleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ekmekSiparis_ekmektipleri",
                table: "ekmekSiparis");

            migrationBuilder.AddForeignKey(
                name: "FK_ekmekSiparis_ekmekTipleri",
                table: "ekmekSiparis",
                column: "EkmekTipleriId",
                principalTable: "ekmekTipleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
