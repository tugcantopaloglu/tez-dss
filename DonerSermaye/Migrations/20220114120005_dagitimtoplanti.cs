using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class dagitimtoplanti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KararSayisi",
                table: "hesaplar",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToplantiSayisi",
                table: "hesaplar",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToplantiTarihi",
                table: "hesaplar",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KararSayisi",
                table: "hesaplar");

            migrationBuilder.DropColumn(
                name: "ToplantiSayisi",
                table: "hesaplar");

            migrationBuilder.DropColumn(
                name: "ToplantiTarihi",
                table: "hesaplar");
        }
    }
}
