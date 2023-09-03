using System;
using System.Linq;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Infrastructure
{
    public class SQLRepository : IRepository<Customer>
    {
        private bool disposed = false;

        private DBContext dbContext;

        public SQLRepository(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public long AddCustomer(Customer item)
        {
            dbContext.Customers.Add(item);
            dbContext.SaveChanges();
            return item.Id;
        }

        public Customer GetCustomerById(long id)
        {
            return dbContext.Customers.First(c => c.Id == id);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
