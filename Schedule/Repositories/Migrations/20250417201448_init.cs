using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Receptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    departmentid = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    startTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Receptions_Departments_departmentid",
                        column: x => x.departmentid,
                        principalTable: "Departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkSchedule4Day",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    startOfWork = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    endOfWork = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    isWorking = table.Column<bool>(type: "boolean", nullable: false),
                    Departmentid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedule4Day", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkSchedule4Day_Departments_Departmentid",
                        column: x => x.Departmentid,
                        principalTable: "Departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "JobTitleReception",
                columns: table => new
                {
                    receptionsid = table.Column<int>(type: "integer", nullable: false),
                    requiredPersonnelid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitleReception", x => new { x.receptionsid, x.requiredPersonnelid });
                    table.ForeignKey(
                        name: "FK_JobTitleReception_JobTitles_requiredPersonnelid",
                        column: x => x.requiredPersonnelid,
                        principalTable: "JobTitles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobTitleReception_Receptions_receptionsid",
                        column: x => x.receptionsid,
                        principalTable: "Receptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    jobTitleid = table.Column<int>(type: "integer", nullable: false),
                    Departmentid = table.Column<int>(type: "integer", nullable: true),
                    Receptionid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Workers_Departments_Departmentid",
                        column: x => x.Departmentid,
                        principalTable: "Departments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Workers_JobTitles_jobTitleid",
                        column: x => x.jobTitleid,
                        principalTable: "JobTitles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Receptions_Receptionid",
                        column: x => x.Receptionid,
                        principalTable: "Receptions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Vacation",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Workerid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacation", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vacation_Workers_Workerid",
                        column: x => x.Workerid,
                        principalTable: "Workers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DayOff",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    Vacationid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOff", x => x.id);
                    table.ForeignKey(
                        name: "FK_DayOff_Vacation_Vacationid",
                        column: x => x.Vacationid,
                        principalTable: "Vacation",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayOff_Vacationid",
                table: "DayOff",
                column: "Vacationid");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitleReception_requiredPersonnelid",
                table: "JobTitleReception",
                column: "requiredPersonnelid");

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_departmentid",
                table: "Receptions",
                column: "departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Vacation_Workerid",
                table: "Vacation",
                column: "Workerid");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedule4Day_Departmentid",
                table: "WorkSchedule4Day",
                column: "Departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Departmentid",
                table: "Workers",
                column: "Departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Receptionid",
                table: "Workers",
                column: "Receptionid");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_jobTitleid",
                table: "Workers",
                column: "jobTitleid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOff");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "JobTitleReception");

            migrationBuilder.DropTable(
                name: "WorkSchedule4Day");

            migrationBuilder.DropTable(
                name: "Vacation");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Receptions");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
