using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class ekmektipleriduzeltme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EkmekTipi",
                table: "ekmekSiparis",
                newName: "EkmekTipleriId");

            migrationBuilder.RenameIndex(
                name: "IX_ekmekSiparis_EkmekTipi",
                table: "ekmekSiparis",
                newName: "IX_ekmekSiparis_EkmekTipleriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EkmekTipleriId",
                table: "ekmekSiparis",
                newName: "EkmekTipi");

            migrationBuilder.RenameIndex(
                name: "IX_ekmekSiparis_EkmekTipleriId",
                table: "ekmekSiparis",
                newName: "IX_ekmekSiparis_EkmekTipi");
        }
    }
}
