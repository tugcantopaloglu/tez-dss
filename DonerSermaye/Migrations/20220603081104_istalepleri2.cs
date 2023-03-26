using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class istalepleri2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KararSayisi",
                table: "istalepleri",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "KararTarihi",
                table: "istalepleri",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ToplantiSayisi",
                table: "istalepleri",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KararSayisi",
                table: "istalepleri");

            migrationBuilder.DropColumn(
                name: "KararTarihi",
                table: "istalepleri");

            migrationBuilder.DropColumn(
                name: "ToplantiSayisi",
                table: "istalepleri");
        }
    }
}
