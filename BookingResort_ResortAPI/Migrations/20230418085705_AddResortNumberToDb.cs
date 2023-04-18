﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingResort_ResortAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddResortNumberToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResortNumbers",
                columns: table => new
                {
                    ResortNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Createddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updateddate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResortNumbers", x => x.ResortNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResortNumbers");
        }
    }
}
