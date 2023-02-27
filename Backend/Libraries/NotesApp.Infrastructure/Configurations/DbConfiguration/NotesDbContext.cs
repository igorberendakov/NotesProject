using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.Configurations.DbConfiguration
{
    public class NotesDbContext : DbContext, IDbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Tag> Tags { get; } = null!;
        public DbSet<Note> Notes { get; } = null!;
        public DbSet<NoteTag> NoteTags { get; } = null!;
        public DbSet<Notification> Notifications { get; } = null!;

        public NotesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
                entity.Property(x => x.Login)
                    .HasColumnName("login");
                entity.Property(x => x.PasswordHash)
                    .HasColumnName("password");
                entity.HasIndex(x => x.Login)
                    .IsUnique();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
                entity.Property(x => x.UserId)
                    .HasColumnName("user_id");
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId);
                entity.Property(x => x.Text)
                    .HasColumnName("text");
            });

            modelBuilder.Entity<NoteTag>(entity =>
            {
                entity.ToTable("note_tag");
                entity.HasKey(x => new { x.NoteId, x.TagId });
                entity.HasIndex(x => new { x.NoteId, x.TagId })
                    .IsUnique();
                entity.HasOne(x => x.Tag)
                    .WithMany()
                    .HasForeignKey(x => x.TagId);
                entity.HasOne(x => x.Note)
                    .WithMany(x => x.NoteTags)
                    .HasForeignKey(x => x.NoteId);
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
                entity.Property(x => x.UserId)
                    .HasColumnName("user_id");
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId);
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
                entity.Property(x => x.UserId)
                    .HasColumnName("user_id");
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId);
                entity.Property(x => x.NoteId)
                    .HasColumnName("note_id");
                entity.Property(x => x.TimeBinding)
                    .HasColumnName("time_binding");
                entity.HasOne(x => x.Note)
                    .WithMany()
                    .HasForeignKey(x => x.NoteId);
            });
        }
    }
}