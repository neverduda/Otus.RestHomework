using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("{id:long}")]
        public Task<Customer> GetCustomerAsync([FromRoute] long id)
        {
            try
            {
                var customer = customerService.GetCustomerById(id);
                return Task.FromResult(customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        [HttpPost("")]
        public Task<long> CreateCustomerAsync([FromBody] Customer customer)
        {
            try
            {
                var id = customerService.AddCustomer(customer);
                return Task.FromResult(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}