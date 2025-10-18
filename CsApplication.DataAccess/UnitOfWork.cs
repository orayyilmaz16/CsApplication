using CsApplication.Domain;

namespace CsApplication.DataAccess
{
  

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICustomerRepository<Customer> Customers { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository<Customer>(_context);
        }

        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
