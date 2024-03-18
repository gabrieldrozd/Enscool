using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Management.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InstitutionModifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstitutionAdministratorIds_Institution_InstitutionId",
                schema: "Management",
                table: "InstitutionAdministratorIds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institution",
                schema: "Management",
                table: "Institution");

            migrationBuilder.RenameTable(
                name: "Institution",
                schema: "Management",
                newName: "Institutions",
                newSchema: "Management");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institutions",
                schema: "Management",
                table: "Institutions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutionAdministratorIds_Institutions_InstitutionId",
                schema: "Management",
                table: "InstitutionAdministratorIds",
                column: "InstitutionId",
                principalSchema: "Management",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstitutionAdministratorIds_Institutions_InstitutionId",
                schema: "Management",
                table: "InstitutionAdministratorIds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institutions",
                schema: "Management",
                table: "Institutions");

            migrationBuilder.RenameTable(
                name: "Institutions",
                schema: "Management",
                newName: "Institution",
                newSchema: "Management");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institution",
                schema: "Management",
                table: "Institution",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstitutionAdministratorIds_Institution_InstitutionId",
                schema: "Management",
                table: "InstitutionAdministratorIds",
                column: "InstitutionId",
                principalSchema: "Management",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
