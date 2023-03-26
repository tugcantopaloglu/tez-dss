using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class y23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "vergiDairesi",
                table: "firmalar",
                maxLength: 25,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_firmalar_vergiNo",
                table: "firmalar",
                column: "vergiNo",
                unique: true,
                filter: "[vergiNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_firmalar_vergiNo",
                table: "firmalar");

            migrationBuilder.DropColumn(
                name: "vergiDairesi",
                table: "firmalar");
        }
    }
}
