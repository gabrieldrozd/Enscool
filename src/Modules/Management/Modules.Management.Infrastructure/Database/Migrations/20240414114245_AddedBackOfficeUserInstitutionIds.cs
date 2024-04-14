using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Modules.Management.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedBackOfficeUserInstitutionIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackOfficeUserInstitutionIds",
                schema: "Management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackOfficeUserInstitutionIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackOfficeUserInstitutionIds_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Management",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackOfficeUserInstitutionIds_UserId",
                schema: "Management",
                table: "BackOfficeUserInstitutionIds",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackOfficeUserInstitutionIds",
                schema: "Management");
        }
    }
}
