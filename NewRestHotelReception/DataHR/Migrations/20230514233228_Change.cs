using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataHR.Migrations
{
    /// <inheritdoc />
    public partial class Change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartialPayment",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PartialPayment",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
