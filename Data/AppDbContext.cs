using Microsoft.EntityFrameworkCore;
using AlIzza.API.Models;

namespace AlIzza.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<School> Schools => Set<School>();
        public DbSet<Parent> Parents => Set<Parent>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Staff> StaffMembers => Set<Staff>();
        public DbSet<AcademicClass> AcademicClasses { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enforce Unique constraints on Parent Email and Phone Number for recovery logic
            modelBuilder.Entity<Parent>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Parent>()
                .HasIndex(p => p.PhoneNumber)
                .IsUnique();

            // Enforce Unique constraint on Student School Number when assigned
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.SchoolNumber)
                .IsUnique()
                .HasFilter("\"SchoolNumber\" IS NOT NULL");
        }
    }
}