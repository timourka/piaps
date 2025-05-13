using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Departments_Departmentid",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_Departmentid",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Departmentid",
                table: "Workers");

            migrationBuilder.CreateTable(
                name: "DepartmentWorker",
                columns: table => new
                {
                    departmentsid = table.Column<int>(type: "integer", nullable: false),
                    workersid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentWorker", x => new { x.departmentsid, x.workersid });
                    table.ForeignKey(
                        name: "FK_DepartmentWorker_Departments_departmentsid",
                        column: x => x.departmentsid,
                        principalTable: "Departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentWorker_Workers_workersid",
                        column: x => x.workersid,
                        principalTable: "Workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentWorker_workersid",
                table: "DepartmentWorker",
                column: "workersid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentWorker");

            migrationBuilder.AddColumn<int>(
                name: "Departmentid",
                table: "Workers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Departmentid",
                table: "Workers",
                column: "Departmentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Departments_Departmentid",
                table: "Workers",
                column: "Departmentid",
                principalTable: "Departments",
                principalColumn: "id");
        }
    }
}
