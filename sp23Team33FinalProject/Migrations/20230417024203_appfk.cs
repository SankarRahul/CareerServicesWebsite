using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sp23Team33FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class appfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Applications_AppForeignKey",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_AppForeignKey",
                table: "Interviews");

            migrationBuilder.AlterColumn<int>(
                name: "AppForeignKey",
                table: "Interviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_AppForeignKey",
                table: "Interviews",
                column: "AppForeignKey",
                unique: true,
                filter: "[AppForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Applications_AppForeignKey",
                table: "Interviews",
                column: "AppForeignKey",
                principalTable: "Applications",
                principalColumn: "ApplicationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Applications_AppForeignKey",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_AppForeignKey",
                table: "Interviews");

            migrationBuilder.AlterColumn<int>(
                name: "AppForeignKey",
                table: "Interviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_AppForeignKey",
                table: "Interviews",
                column: "AppForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Applications_AppForeignKey",
                table: "Interviews",
                column: "AppForeignKey",
                principalTable: "Applications",
                principalColumn: "ApplicationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
