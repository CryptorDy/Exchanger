using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchanger.Migrations
{
    /// <inheritdoc />
    public partial class RenameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelatedId",
                table: "Transactions",
                newName: "ExchangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExchangeId",
                table: "Transactions",
                newName: "RelatedId");
        }
    }
}
