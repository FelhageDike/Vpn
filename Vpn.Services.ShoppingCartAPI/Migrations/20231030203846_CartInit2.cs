using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vpn.Services.ShoppingCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class CartInit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartHeaders",
                newName: "CartHeaderId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartDetails",
                newName: "CartDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CartHeaderId",
                table: "CartHeaders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CartDetailsId",
                table: "CartDetails",
                newName: "Id");
        }
    }
}
