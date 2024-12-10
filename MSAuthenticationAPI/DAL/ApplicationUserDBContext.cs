using Microsoft.EntityFrameworkCore;
using MSAuthenticationAPI.Model;

namespace MSAuthenticationAPI.DAL
{
    public class ApplicationUserDBContext :DbContext
    {
        public ApplicationUserDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
