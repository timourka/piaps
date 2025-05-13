using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayOff_Vacation_Vacationid",
                table: "DayOff");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacation_Workers_Workerid",
                table: "Vacation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedule4Day_Departments_Departmentid",
                table: "WorkSchedule4Day");

            migrationBuilder.RenameColumn(
                name: "Departmentid",
                table: "WorkSchedule4Day",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedule4Day_Departmentid",
                table: "WorkSchedule4Day",
                newName: "IX_WorkSchedule4Day_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "Workerid",
                table: "Vacation",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_Vacation_Workerid",
                table: "Vacation",
                newName: "IX_Vacation_WorkerId");

            migrationBuilder.RenameColumn(
                name: "Vacationid",
                table: "DayOff",
                newName: "VacationId");

            migrationBuilder.RenameIndex(
                name: "IX_DayOff_Vacationid",
                table: "DayOff",
                newName: "IX_DayOff_VacationId");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "WorkSchedule4Day",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "Vacation",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VacationId",
                table: "DayOff",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DayOff_Vacation_VacationId",
                table: "DayOff",
                column: "VacationId",
                principalTable: "Vacation",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacation_Workers_WorkerId",
                table: "Vacation",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedule4Day_Departments_DepartmentId",
                table: "WorkSchedule4Day",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayOff_Vacation_VacationId",
                table: "DayOff");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacation_Workers_WorkerId",
                table: "Vacation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedule4Day_Departments_DepartmentId",
                table: "WorkSchedule4Day");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "WorkSchedule4Day",
                newName: "Departmentid");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedule4Day_DepartmentId",
                table: "WorkSchedule4Day",
                newName: "IX_WorkSchedule4Day_Departmentid");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "Vacation",
                newName: "Workerid");

            migrationBuilder.RenameIndex(
                name: "IX_Vacation_WorkerId",
                table: "Vacation",
                newName: "IX_Vacation_Workerid");

            migrationBuilder.RenameColumn(
                name: "VacationId",
                table: "DayOff",
                newName: "Vacationid");

            migrationBuilder.RenameIndex(
                name: "IX_DayOff_VacationId",
                table: "DayOff",
                newName: "IX_DayOff_Vacationid");

            migrationBuilder.AlterColumn<int>(
                name: "Departmentid",
                table: "WorkSchedule4Day",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Workerid",
                table: "Vacation",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Vacationid",
                table: "DayOff",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_DayOff_Vacation_Vacationid",
                table: "DayOff",
                column: "Vacationid",
                principalTable: "Vacation",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacation_Workers_Workerid",
                table: "Vacation",
                column: "Workerid",
                principalTable: "Workers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedule4Day_Departments_Departmentid",
                table: "WorkSchedule4Day",
                column: "Departmentid",
                principalTable: "Departments",
                principalColumn: "id");
        }
    }
}
