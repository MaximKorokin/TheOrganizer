using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheOrganizer.Model
{
    public partial class TheOrganizerDBContext : DbContext
    {
        public TheOrganizerDBContext()
        {
        }

        public TheOrganizerDBContext(DbContextOptions<TheOrganizerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Model> Models { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => e.Int);
            });
        }
    }
}
