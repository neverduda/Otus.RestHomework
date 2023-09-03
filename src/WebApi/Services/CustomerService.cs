using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Services
{
    public class CustomerService : ICustomerService
    {
        IRepository<Customer> customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public Customer GetCustomerById(long id)
        {
            return customerRepository.GetCustomerById(id);
        }

        public long AddCustomer(Customer item)
        {
            return customerRepository.AddCustomer(item);
        }
    }
}
