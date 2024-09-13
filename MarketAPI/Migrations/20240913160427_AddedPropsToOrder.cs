using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketAPI.Migrations
{
    public partial class AddedPropsToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Orders");
        }
    }
}
