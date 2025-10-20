
using CustomerManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

       
    }
}
