
using CsApplication.Domain;

namespace CsApplication.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository<Customer> Customers { get; }
        int Complete();
    }

}
