using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class RemoveReceiptSenderAndReceiverRelationShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptReceiver");

            migrationBuilder.DropTable(
                name: "ReceiptSenders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceiptReceiver",
                columns: table => new
                {
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptReceiver", x => new { x.ReceiverId, x.ReceiptId });
                    table.ForeignKey(
                        name: "FK_ReceiptReceiver_Inventories_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptReceiver_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptSenders",
                columns: table => new
                {
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptSenders", x => new { x.SenderId, x.ReceiptId });
                    table.ForeignKey(
                        name: "FK_ReceiptSenders_Inventories_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptSenders_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptReceiver_ReceiptId",
                table: "ReceiptReceiver",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSenders_ReceiptId",
                table: "ReceiptSenders",
                column: "ReceiptId");
        }
    }
}
