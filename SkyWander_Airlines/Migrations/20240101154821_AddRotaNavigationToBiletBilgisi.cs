using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyWander_Airlines.Migrations
{
    /// <inheritdoc />
    public partial class AddRotaNavigationToBiletBilgisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Durum",
                table: "Uyeler");

            migrationBuilder.CreateIndex(
                name: "IX_BiletBilgisi_RotaId",
                table: "BiletBilgisi",
                column: "RotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_BiletBilgisi_Rota_RotaId",
                table: "BiletBilgisi",
                column: "RotaId",
                principalTable: "Rota",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BiletBilgisi_Rota_RotaId",
                table: "BiletBilgisi");

            migrationBuilder.DropIndex(
                name: "IX_BiletBilgisi_RotaId",
                table: "BiletBilgisi");

            migrationBuilder.AddColumn<bool>(
                name: "Durum",
                table: "Uyeler",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
