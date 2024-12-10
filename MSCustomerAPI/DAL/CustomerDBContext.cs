using Microsoft.EntityFrameworkCore;
using MSCustomerAPI.Model;

namespace MSCustomerAPI.DAL
{
    public class CustomerDBContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
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
