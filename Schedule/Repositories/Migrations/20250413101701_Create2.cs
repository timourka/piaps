using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Create2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Departments_departmentid",
                table: "Workers");

            migrationBuilder.DropTable(
                name: "ReceptionWorker");

            migrationBuilder.RenameColumn(
                name: "departmentid",
                table: "Workers",
                newName: "Departmentid");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_departmentid",
                table: "Workers",
                newName: "IX_Workers_Departmentid");

            migrationBuilder.AlterColumn<int>(
                name: "Departmentid",
                table: "Workers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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
                name: "FK_Workers_Departments_Departmentid",
                table: "Workers",
                column: "Departmentid",
                principalTable: "Departments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Receptions_Receptionid",
                table: "Workers",
                column: "Receptionid",
                principalTable: "Receptions",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Departments_Departmentid",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Receptions_Receptionid",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_Receptionid",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Receptionid",
                table: "Workers");

            migrationBuilder.RenameColumn(
                name: "Departmentid",
                table: "Workers",
                newName: "departmentid");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_Departmentid",
                table: "Workers",
                newName: "IX_Workers_departmentid");

            migrationBuilder.AlterColumn<int>(
                name: "departmentid",
                table: "Workers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ReceptionWorker",
                columns: table => new
                {
                    personnelid = table.Column<int>(type: "integer", nullable: false),
                    upcomingReceptionsid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionWorker", x => new { x.personnelid, x.upcomingReceptionsid });
                    table.ForeignKey(
                        name: "FK_ReceptionWorker_Receptions_upcomingReceptionsid",
                        column: x => x.upcomingReceptionsid,
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
                name: "IX_ReceptionWorker_upcomingReceptionsid",
                table: "ReceptionWorker",
                column: "upcomingReceptionsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Departments_departmentid",
                table: "Workers",
                column: "departmentid",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
