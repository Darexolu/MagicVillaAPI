using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class villathumbnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ThumbnailUrl" },
                values: new object[] { new DateTime(2023, 11, 20, 12, 3, 38, 745, DateTimeKind.Local).AddTicks(4904), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ThumbnailUrl" },
                values: new object[] { new DateTime(2023, 11, 20, 12, 3, 38, 745, DateTimeKind.Local).AddTicks(4915), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ThumbnailUrl" },
                values: new object[] { new DateTime(2023, 11, 20, 12, 3, 38, 745, DateTimeKind.Local).AddTicks(4917), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ThumbnailUrl" },
                values: new object[] { new DateTime(2023, 11, 20, 12, 3, 38, 745, DateTimeKind.Local).AddTicks(4918), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ThumbnailUrl" },
                values: new object[] { new DateTime(2023, 11, 20, 12, 3, 38, 745, DateTimeKind.Local).AddTicks(4920), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Villas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 20, 11, 15, 5, 576, DateTimeKind.Local).AddTicks(3448));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 20, 11, 15, 5, 576, DateTimeKind.Local).AddTicks(3459));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 20, 11, 15, 5, 576, DateTimeKind.Local).AddTicks(3461));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 20, 11, 15, 5, 576, DateTimeKind.Local).AddTicks(3463));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 20, 11, 15, 5, 576, DateTimeKind.Local).AddTicks(3464));
        }
    }
}
