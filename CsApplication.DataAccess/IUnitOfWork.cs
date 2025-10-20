
using CsApplication.Domain;

namespace CsApplication.DataAccess
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICustomerRepository<Customer> Customers { get; }
        Task<int> CompleteAsync();
    }

}
