using CsApplication.Domain;

namespace CsApplication.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ICustomerRepository<Customer> _customers;
       

        public UnitOfWork(AppDbContext context, ICustomerRepository<Customer> customers)
        {
            _context = context;
            _customers = customers;
        }

        public ICustomerRepository<Customer> Customers => _customers;

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
