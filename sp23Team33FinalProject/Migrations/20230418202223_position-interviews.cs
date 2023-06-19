using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sp23Team33FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class positioninterviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionID",
                table: "Interviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_PositionID",
                table: "Interviews",
                column: "PositionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Positions_PositionID",
                table: "Interviews",
                column: "PositionID",
                principalTable: "Positions",
                principalColumn: "PositionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Positions_PositionID",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_PositionID",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "PositionID",
                table: "Interviews");
        }
    }
}
