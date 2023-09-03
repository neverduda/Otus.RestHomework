using WebApi.Models;

namespace WebApi.Services
{
    public interface ICustomerService
    {
        Customer GetCustomerById(long id);

        long AddCustomer(Customer item);
    }
}
