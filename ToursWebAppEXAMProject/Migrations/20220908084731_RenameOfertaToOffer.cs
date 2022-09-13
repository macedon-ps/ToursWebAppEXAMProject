using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    public partial class RenameOfertaToOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Offer");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_TourId",
                table: "Offer",
                newName: "IX_Offer_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_SallerId",
                table: "Offer",
                newName: "IX_Offer_SallerId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_CustomerId",
                table: "Offer",
                newName: "IX_Offer_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "Offers");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_TourId",
                table: "Offers",
                newName: "IX_Offers_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_SallerId",
                table: "Offers",
                newName: "IX_Offers_SallerId");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_CustomerId",
                table: "Offers",
                newName: "IX_Offers_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");
        }
    }
}
