using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniBankApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    AccountName = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    CurrentAccountBalance = table.Column<decimal>(type: "TEXT", nullable: false),
                    AccountType = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PinHash = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PinSalt = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransactionUniqueReference = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransactionStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionSourceAccount = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionDestinationAccount = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionParticulars = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionType = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
