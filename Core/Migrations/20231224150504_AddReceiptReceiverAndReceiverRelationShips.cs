using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddReceiptReceiverAndReceiverRelationShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptSenders");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ReceiverId",
                table: "Receipts",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SenderId",
                table: "Receipts",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Locations_ReceiverId",
                table: "Receipts",
                column: "ReceiverId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Locations_SenderId",
                table: "Receipts",
                column: "SenderId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Locations_ReceiverId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Locations_SenderId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_ReceiverId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_SenderId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Receipts");

            migrationBuilder.CreateTable(
                name: "ReceiptSenders",
                columns: table => new
                {
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptSenders", x => new { x.SenderId, x.ReceiptId });
                    table.ForeignKey(
                        name: "FK_ReceiptSenders_Locations_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Locations",
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
                name: "IX_ReceiptSenders_ReceiptId",
                table: "ReceiptSenders",
                column: "ReceiptId");
        }
    }
}
