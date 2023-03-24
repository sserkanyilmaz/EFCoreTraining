using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tracking.Migrations
{
    /// <inheritdoc />
    public partial class ders20_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Roller_RollerId",
                table: "Kullanicilar");

            migrationBuilder.RenameColumn(
                name: "RollerId",
                table: "Kullanicilar",
                newName: "RolId");

            migrationBuilder.RenameIndex(
                name: "IX_Kullanicilar_RollerId",
                table: "Kullanicilar",
                newName: "IX_Kullanicilar_RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar",
                column: "RolId",
                principalTable: "Roller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "Kullanicilar",
                newName: "RollerId");

            migrationBuilder.RenameIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar",
                newName: "IX_Kullanicilar_RollerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Roller_RollerId",
                table: "Kullanicilar",
                column: "RollerId",
                principalTable: "Roller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
