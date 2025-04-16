using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortalBackend.Migrations
{
    /// <inheritdoc />
    public partial class jobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Jobs",
                newName: "SalaryRange");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Jobs",
                newName: "JobType");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDeadline",
                table: "Jobs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Jobs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Jobs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationDeadline",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "SalaryRange",
                table: "Jobs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "Jobs",
                newName: "Company");
        }
    }
}
