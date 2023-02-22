﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotesApp.Infrastructure.DbConfiguration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NotesApp.Infrastructure.Migrations
{
    [DbContext(typeof(NotesDbContext))]
    [Migration("20230221095620_FixForeignKeys")]
    partial class FixForeignKeys
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NotesApp.Domain.Entities.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_note");

                    b.ToTable("note", (string)null);
                });

            modelBuilder.Entity("NotesApp.Domain.Entities.NoteTag", b =>
                {
                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid")
                        .HasColumnName("note_id");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid")
                        .HasColumnName("tag_id");

                    b.HasKey("NoteId", "TagId")
                        .HasName("pk_note_tag");

                    b.HasIndex("TagId")
                        .HasDatabaseName("ix_note_tag_tag_id");

                    b.HasIndex("NoteId", "TagId")
                        .IsUnique()
                        .HasDatabaseName("ix_note_tag_note_id_tag_id");

                    b.ToTable("note_tag", (string)null);
                });

            modelBuilder.Entity("NotesApp.Domain.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid")
                        .HasColumnName("note_id");

                    b.Property<DateTime?>("TimeBinding")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time_binding");

                    b.HasKey("Id")
                        .HasName("pk_notification");

                    b.HasIndex("NoteId")
                        .HasDatabaseName("ix_notification_note_id");

                    b.ToTable("notification", (string)null);
                });

            modelBuilder.Entity("NotesApp.Domain.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.HasKey("Id")
                        .HasName("pk_tag");

                    b.ToTable("tag", (string)null);
                });

            modelBuilder.Entity("NotesApp.Domain.Entities.NoteTag", b =>
                {
                    b.HasOne("NotesApp.Domain.Entities.Note", "Note")
                        .WithMany("NoteTags")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_note_tag_notes_note_id");

                    b.HasOne("NotesApp.Domain.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_note_tag_tag_tag_id");

                    b.Navigation("Note");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("NotesApp.Domain.Entities.Notification", b =>
                {
                    b.HasOne("NotesApp.Domain.Entities.Note", "Note")
                        .WithMany()
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_notification_note_note_id");

                    b.Navigation("Note");
                });

            modelBuilder.Entity("NotesApp.Domain.Entities.Note", b =>
                {
                    b.Navigation("NoteTags");
                });
#pragma warning restore 612, 618
        }
    }
}
