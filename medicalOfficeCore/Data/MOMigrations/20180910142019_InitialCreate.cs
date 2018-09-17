using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace medicalOfficeCore.Data.MOMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MO");

            migrationBuilder.CreateTable(
                name: "Doctors",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DOB = table.Column<DateTime>(nullable: true),
                    DoctorID = table.Column<int>(nullable: false),
                    ExpYrVisits = table.Column<byte>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    OHIP = table.Column<string>(maxLength: 10, nullable: false),
                    Phone = table.Column<long>(nullable: false),
                    eMail = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalSchema: "MO",
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorID",
                schema: "MO",
                table: "Patients",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_OHIP",
                schema: "MO",
                table: "Patients",
                column: "OHIP",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Doctors",
                schema: "MO");
        }
    }
}
