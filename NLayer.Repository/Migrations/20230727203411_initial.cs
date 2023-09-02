﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NLayer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeatures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(240), "Kalemler", null },
                    { 2, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(252), "Kitaplar", null },
                    { 3, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(253), "Defterler", null },
                    { 4, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(253), "Boyalar", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(373), "Kurşun Kalem", 10m, 20, null },
                    { 2, 1, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(374), "Uçlu Kalem", 50m, 30, null },
                    { 3, 1, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(375), "Pilot Kalem", 70m, 15, null },
                    { 4, 2, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(376), "Oscar Wilde", 100m, 20, null },
                    { 5, 2, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(377), "Cesur Yeni Dünya", 200m, 30, null },
                    { 6, 2, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(378), "Kızıl Veba", 350m, 15, null },
                    { 7, 2, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(379), "Simyacı", 300m, 15, null },
                    { 8, 3, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(379), "Kareli Defter", 50m, 50, null },
                    { 9, 3, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(380), "Çizgili Defter", 40m, 40, null },
                    { 10, 3, new DateTime(2023, 7, 27, 23, 34, 11, 708, DateTimeKind.Local).AddTicks(381), "Çizgisiz Defter", 60m, 30, null }
                });

            migrationBuilder.InsertData(
                table: "ProductFeatures",
                columns: new[] { "Id", "Color", "Height", "ProductId", "Width" },
                values: new object[,]
                {
                    { 1, "Kırmızı", 20, 1, 30 },
                    { 2, "Mavi", 21, 2, 35 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_ProductId",
                table: "ProductFeatures",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFeatures");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
