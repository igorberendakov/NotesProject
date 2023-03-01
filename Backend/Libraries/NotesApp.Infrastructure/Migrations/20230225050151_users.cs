using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "tag",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "notification",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "note",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_tag_user_id",
                table: "tag",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_notification_user_id",
                table: "notification",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_note_user_id",
                table: "note",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_note_user_user_id",
                table: "note",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_user_user_id",
                table: "notification",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tag_user_user_id",
                table: "tag",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_note_user_user_id",
                table: "note");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_user_user_id",
                table: "notification");

            migrationBuilder.DropForeignKey(
                name: "fk_tag_user_user_id",
                table: "tag");

            migrationBuilder.DropIndex(
                name: "ix_tag_user_id",
                table: "tag");

            migrationBuilder.DropIndex(
                name: "ix_notification_user_id",
                table: "notification");

            migrationBuilder.DropIndex(
                name: "ix_note_user_id",
                table: "note");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "tag");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "notification");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "note");
        }
    }
}
