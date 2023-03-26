using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class genelayarlar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dekan",
                table: "ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Raportor",
                table: "ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uye1",
                table: "ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uye2",
                table: "ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uye3",
                table: "ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uye4",
                table: "ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uye5",
                table: "ayarlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uye6",
                table: "ayarlar",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dekan",
                table: "ayarlar");

            migrationBuilder.DropColumn(
                name: "Raportor",
                table: "ayarlar");

            migrationBuilder.DropColumn(
                name: "Uye1",
                table: "ayarlar");

            migrationBuilder.DropColumn(
                name: "Uye2",
                table: "ayarlar");

            migrationBuilder.DropColumn(
                name: "Uye3",
                table: "ayarlar");

            migrationBuilder.DropColumn(
                name: "Uye4",
                table: "ayarlar");

            migrationBuilder.DropColumn(
                name: "Uye5",
                table: "ayarlar");

            migrationBuilder.DropColumn(
                name: "Uye6",
                table: "ayarlar");
        }
    }
}
