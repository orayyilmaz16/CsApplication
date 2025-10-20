using CsApplication.Domain;
using Microsoft.EntityFrameworkCore;


namespace CsApplication.DataAccess
{
    public class CustomerRepository : ICustomerRepository<Customer>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Customer>();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(Customer entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(Customer entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask; // EF Core update senkron ama interface async olduğu için
        }

        public async Task DeleteAsync(Customer entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask; // Remove da senkron
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}