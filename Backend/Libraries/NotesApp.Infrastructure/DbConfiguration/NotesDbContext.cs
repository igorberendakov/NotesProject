using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.DbConfiguration
{
    public class NotesDbContext : DbContext, IDbContext
    {
        public DbSet<Tag> Tags { get; } = null!;
        public DbSet<Note> Notes { get; } = null!;
        public DbSet<NoteTag> NoteTags { get; } = null!;
        public DbSet<Notification> Notifications { get; } = null!;

        public NotesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tags = new List<Tag>
            {
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Text = "TestTag"
                }
            };

            var notes = new List<Note>
            {
                new Note
                {
                    Id = Guid.NewGuid(),
                    Text = "TestNote",
                    Title= "TestTitle"
                }
            };

            var noteTags = new List<NoteTag>
            {
                new NoteTag
                {
                    NoteId = notes.First().Id,
                    TagId = tags.First().Id,
                }
            };

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
                entity.Property(x => x.Text)
                    .HasColumnName("text");
                entity.HasData(tags);
            });

            modelBuilder.Entity<NoteTag>(entity =>
            {
                entity.ToTable("note_tag");
                entity.HasNoKey();
                entity.HasIndex(x => new { x.NoteId, x.TagId })
                    .IsUnique();
                entity.HasOne(x => x.Tag)
                    .WithMany();
                entity.HasOne(x => x.Note)
                    .WithMany();
                entity.Property(x => x.NoteId)
                    .HasColumnName("note_id");
                entity.Property(x => x.TagId)
                    .HasColumnName("tag_id");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("note");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
                entity.Property(x => x.Title)
                    .HasColumnName("title");
                entity.Property(x => x.Text)
                    .HasColumnName("text");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
                entity.Property(x => x.NoteId)
                    .HasColumnName("note_id");
                entity.Property(x => x.TimeBinding)
                    .HasColumnName("time_binding");
                entity.HasOne(x => x.Note)
                    .WithMany();
            });
        }
    }
}