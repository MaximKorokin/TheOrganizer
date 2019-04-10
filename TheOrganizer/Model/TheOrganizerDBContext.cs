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
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<TodoList> TodoLists { get; set; }
        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Notebook> Notebooks { get; set; }
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

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasOne(c => c.User)
                .WithMany(u => u.Calendars)
                .HasForeignKey(c => c.OwnerId)
                .HasPrincipalKey(u => u.Id);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);

                                                // **** Description of fluent api foreign key creation ****
                entity.HasOne(e => e.Calendar)      // one calendar per event
                .WithMany(c => c.Events)        // many events per calendar
                .HasForeignKey(e => e.CalendarId)  // calendar id is event's foreign key
                .HasPrincipalKey(c => c.Id);    // id is calendar's primary key
            });

            modelBuilder.Entity<TodoList>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasOne(c => c.User)
                .WithMany(u => u.TodoLists)
                .HasForeignKey(c => c.OwnerId)
                .HasPrincipalKey(u => u.Id);
            });

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.HasOne(t => t.TodoList)
                .WithMany(tl => tl.Todos)
                .HasForeignKey(t => t.TodoListId)
                .HasPrincipalKey(tl => tl.Id);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasOne(c => c.User)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.OwnerId)
                .HasPrincipalKey(u => u.Id);
            });

            modelBuilder.Entity<Notebook>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasOne(c => c.User)
                .WithMany(u => u.Notebooks)
                .HasForeignKey(c => c.OwnerId)
                .HasPrincipalKey(u => u.Id);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(n => n.Id);

                entity.HasOne(n => n.Notebook)
                .WithMany(nb => nb.Notes)
                .HasForeignKey(n => n.NotebookId)
                .HasPrincipalKey(nb => nb.Id);
            });
        }
    }
}
