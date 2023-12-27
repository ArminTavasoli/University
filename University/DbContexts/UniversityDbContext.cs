using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.DbContexts
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<JunctionThSt> JunctionThSts { get; set; }


    }
}
