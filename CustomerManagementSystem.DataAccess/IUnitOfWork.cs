using CustomerManagementSystem.Domain;

namespace CustomerManagementSystem.DataAccess
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICustomerRepository<Customer> Customers { get; }
        Task<int> CompleteAsync();
    }

}
