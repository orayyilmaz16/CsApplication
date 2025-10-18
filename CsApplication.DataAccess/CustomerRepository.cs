using CsApplication.Domain;
using Microsoft.EntityFrameworkCore;


namespace CsApplication.DataAccess
{
    public class CustomerRepository<T> : ICustomerRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(int id) => _dbSet.Find(id);
        public IEnumerable<T> GetAll() => _dbSet.ToList();
        public void Add(T entity) => _dbSet.Add(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);

        public void Save() => _context.SaveChanges();

    }
}