using Microsoft.EntityFrameworkCore;
using MSSalesAPI.Model;


namespace MSSalesAPI.DAL
{
    public class POSalesDBContext : DbContext
    {
        public POSalesDBContext(DbContextOptions<POSalesDBContext> options) : base(options)
        {
        }

        public DbSet<POSales> POSaless { get; set; }
        public DbSet<POSalesDetail> POSalesDetails { get; set; }
    
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
