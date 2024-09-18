using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolWebApi.Model;
using SchoolWebApi.Models;

namespace SchoolWebApi.Data
{
    public class SchoolsDataContext : IdentityDbContext<AppUser>
    {
        public SchoolsDataContext(DbContextOptions<SchoolsDataContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SchoolTeacher> SchoolTeachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // many-to-many
            modelBuilder.Entity<SchoolTeacher>().HasKey(st => new { st.SchoolId, st.TeacherId });
            modelBuilder.Entity<SchoolTeacher>().HasOne(s => s.School).WithMany(st => st.SchoolTeachers).HasForeignKey(t => t.SchoolId);
            modelBuilder.Entity<SchoolTeacher>().HasOne(s => s.Teacher).WithMany(st => st.SchoolTeachers).HasForeignKey(t => t.TeacherId);
        }
    }
}
