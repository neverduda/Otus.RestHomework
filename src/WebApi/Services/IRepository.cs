using System;

namespace WebApi.Services
{
    public interface IRepository<T> : IDisposable where T : class
    {
        long AddCustomer(T item);

        T GetCustomerById(long id);
    }
}
