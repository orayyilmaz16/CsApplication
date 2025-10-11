using CsApplication.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CsApplication.DataAccess
{
    public class CustomerRepository
    {
        public void Add(Customer customer)
        {
            using var context = new AppDbContext();
            context.Customers.Add(customer);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
            }

        }

        public List<Customer> GetAll()
        {
            using var context = new AppDbContext();
            return context.Customers.ToList();
        }
    }
}
