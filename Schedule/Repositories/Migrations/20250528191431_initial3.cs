using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_JobTitles_jobTitleid",
                table: "Workers");

            migrationBuilder.RenameColumn(
                name: "jobTitleid",
                table: "Workers",
                newName: "JobTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_jobTitleid",
                table: "Workers",
                newName: "IX_Workers_JobTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_JobTitles_JobTitleId",
                table: "Workers",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_JobTitles_JobTitleId",
                table: "Workers");

            migrationBuilder.RenameColumn(
                name: "JobTitleId",
                table: "Workers",
                newName: "jobTitleid");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_JobTitleId",
                table: "Workers",
                newName: "IX_Workers_jobTitleid");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_JobTitles_jobTitleid",
                table: "Workers",
                column: "jobTitleid",
                principalTable: "JobTitles",
                principalColumn: "id");
        }
    }
}
