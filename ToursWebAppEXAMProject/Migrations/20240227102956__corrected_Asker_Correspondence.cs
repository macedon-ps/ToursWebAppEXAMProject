using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _corrected_Asker_Correspondence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Askers_Customer_CustomerId",
                table: "Askers");

            migrationBuilder.DropForeignKey(
                name: "FK_Correspondences_Customer_CustomerId",
                table: "Correspondences");

            migrationBuilder.DropForeignKey(
                name: "FK_Correspondences_Offer_OfferId",
                table: "Correspondences");

            migrationBuilder.DropIndex(
                name: "IX_Correspondences_CustomerId",
                table: "Correspondences");

            migrationBuilder.DropIndex(
                name: "IX_Correspondences_OfferId",
                table: "Correspondences");

            migrationBuilder.DropIndex(
                name: "IX_Askers_CustomerId",
                table: "Askers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Correspondences");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Correspondences");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Askers");

            migrationBuilder.RenameColumn(
                name: "IsExCustomerOfCompany",
                table: "Correspondences",
                newName: "IsCustomer");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomer",
                table: "Askers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomer",
                table: "Askers");

            migrationBuilder.RenameColumn(
                name: "IsCustomer",
                table: "Correspondences",
                newName: "IsExCustomerOfCompany");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Correspondences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Correspondences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Askers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Correspondences_CustomerId",
                table: "Correspondences",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Correspondences_OfferId",
                table: "Correspondences",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Askers_CustomerId",
                table: "Askers",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Askers_Customer_CustomerId",
                table: "Askers",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Correspondences_Customer_CustomerId",
                table: "Correspondences",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Correspondences_Offer_OfferId",
                table: "Correspondences",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "Id");
        }
    }
}
