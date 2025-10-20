using CsApplication.Domain;

namespace CsApplication.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICustomerRepository<Customer> Customers { get; }

        public UnitOfWork(AppDbContext context, ICustomerRepository<Customer> customerRepository)
        {
            _context = context;
            Customers = customerRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

    }
}
