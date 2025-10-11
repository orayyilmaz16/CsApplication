
using CsApplication.Domain;
using Microsoft.EntityFrameworkCore;

namespace CsApplication.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                   "Server=ORAY\\SQLEXPRESS;Database=OrayDb;Trusted_Connection=True;TrustServerCertificate=True;"
                );
            }
        }
    }
}
