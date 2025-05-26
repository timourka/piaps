using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initital2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Receptions_Receptionid",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_Receptionid",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Receptionid",
                table: "Workers");

            migrationBuilder.CreateTable(
                name: "ReceptionWorker",
                columns: table => new
                {
                    personnelid = table.Column<int>(type: "integer", nullable: false),
                    receptionsid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionWorker", x => new { x.personnelid, x.receptionsid });
                    table.ForeignKey(
                        name: "FK_ReceptionWorker_Receptions_receptionsid",
                        column: x => x.receptionsid,
                        principalTable: "Receptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptionWorker_Workers_personnelid",
                        column: x => x.personnelid,
                        principalTable: "Workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionWorker_receptionsid",
                table: "ReceptionWorker",
                column: "receptionsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceptionWorker");

            migrationBuilder.AddColumn<int>(
                name: "Receptionid",
                table: "Workers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Receptionid",
                table: "Workers",
                column: "Receptionid");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Receptions_Receptionid",
                table: "Workers",
                column: "Receptionid",
                principalTable: "Receptions",
                principalColumn: "id");
        }
    }
}
