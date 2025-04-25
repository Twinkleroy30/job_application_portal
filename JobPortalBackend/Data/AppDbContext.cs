using Microsoft.EntityFrameworkCore;
using JobPortalBackend.Models;

namespace JobPortalBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
    }
}
