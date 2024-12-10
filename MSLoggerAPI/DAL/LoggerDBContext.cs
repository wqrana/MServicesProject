using Microsoft.EntityFrameworkCore;
using MSLoggerAPI.Model;

namespace MSLoggerAPI.DAL
{
    public class LoggerDBContext : DbContext
    {
        public DbSet<AppLogger> AppLogger { get; set; }
        public LoggerDBContext(DbContextOptions<LoggerDBContext> options) : base(options)
        {
        }

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
