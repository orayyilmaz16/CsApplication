using CsApplication.Business;
using CsApplication.DataAccess;
using System;
using CsApplication.Domain;

namespace CsApplication.UI
{
    public class Program
    {
       static void Main()
        {
            var manager = new CustomerManager();

            manager.AddCustomer(new Customer { Id = 1, Name = "Oray" });
            manager.AddCustomer(new Customer { Id = 2, Name = "Koray" });

            foreach(var c in manager.GetAllCustomers())
            {
                Console.WriteLine($"Customer ID: {c.Id}, Name: {c.Name}");
            }
        }
    }
}
