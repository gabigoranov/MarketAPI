using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAPI.Migrations
{
    public partial class AddingReviewsToOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Offer",
                table: "Reviews",
                newName: "OfferId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Reviews",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OfferId",
                table: "Reviews",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Offers_OfferId",
                table: "Reviews",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Offers_OfferId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_OfferId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "Reviews",
                newName: "Offer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
