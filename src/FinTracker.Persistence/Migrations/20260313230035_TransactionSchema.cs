using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TransactionSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "presentation");

            migrationBuilder.EnsureSchema(
                name: "transaction");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "presentation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentation_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "transaction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "transaction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<string>(type: "text", nullable: false),
                    MoneyAmount = table.Column<double>(type: "double precision", nullable: false),
                    MoneyCurrency = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "transaction",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                schema: "transaction",
                table: "Transactions",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories",
                schema: "presentation");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "transaction");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "transaction");
        }
    }
}
