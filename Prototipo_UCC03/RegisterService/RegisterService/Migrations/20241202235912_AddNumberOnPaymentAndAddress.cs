using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegisterService.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOnPaymentAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValidUntilMonth",
                table: "PaymentCard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValidUntilYear",
                table: "PaymentCard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "DeliveryAddress",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidUntilMonth",
                table: "PaymentCard");

            migrationBuilder.DropColumn(
                name: "ValidUntilYear",
                table: "PaymentCard");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "DeliveryAddress");
        }
    }
}
