using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheOrganizer.Model
{
    public partial class TheOrganizerDBContext : DbContext
    {
        public TheOrganizerDBContext() : base() { }
        public TheOrganizerDBContext(DbContextOptions<TheOrganizerDBContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);

                                                // **** Description of fluent api foreign key creation ****
                entity.HasOne(e => e.User)      // one user per event
                .WithMany(u => u.Events)        // many events per user
                .HasForeignKey(e => e.OwnerId)  // owner id is event's foreign key
                .HasPrincipalKey(u => u.Id);    // id is user's primary key
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.OwnerId)
                .HasPrincipalKey(u => u.Id);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasOne(c => c.User)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.OwnerId)
                .HasPrincipalKey(u => u.Id);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(n => n.Id);

                entity.HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.OwnerId)
                .HasPrincipalKey(u => u.Id);
            });
        }
    }
}
