﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hospital_api.Migrations
{
    /// <inheritdoc />
    public partial class icd10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Icd",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    parentId = table.Column<Guid>(type: "uuid", nullable: false),
                    actual = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icd", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Icd");
        }
    }
}
