using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sp23Team33FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class GradYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "GraduationDate",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "GraduationYear",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraduationYear",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "InterviewerName",
                table: "Interviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GraduationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
