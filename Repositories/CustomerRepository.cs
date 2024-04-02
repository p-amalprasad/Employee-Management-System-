using LogGenerator.Data;
using LogGenerator.Models.Domain;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net;

namespace LogGenerator.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SubmissionDbContext submissionDbContext;

        public CustomerRepository(SubmissionDbContext submissionDbContext)
        {
            this.submissionDbContext = submissionDbContext;
        }

        public async Task<NewCustomer> AddAsync(NewCustomer newCustomer)
        {
            await submissionDbContext.AddAsync(newCustomer);
            await submissionDbContext.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<NewCustomer?> DeleteAsync(int id)
        {
            var existingCustomer = await submissionDbContext.Customers.FindAsync(id);

            if (existingCustomer != null)
            {
                submissionDbContext.Customers.Remove(existingCustomer);
                await submissionDbContext.SaveChangesAsync();
                return existingCustomer;
            }

            return null;
        }

        public async Task<IEnumerable<NewCustomer>> GetAllASync()
        {
            return await submissionDbContext.Customers.ToListAsync();
        }

        public async Task<NewCustomer?> GetAsync(int id)
        {
            return await submissionDbContext.Customers.FirstOrDefaultAsync(x => x.CustId == id);
        }

        public async Task<NewCustomer?> UpdateAsync(NewCustomer newCustomer)
        {
            var existingCustomer = await submissionDbContext.Customers
                .FirstOrDefaultAsync(x => x.CustId == newCustomer.CustId);

            if (existingCustomer != null)
            {
                existingCustomer.CustId = newCustomer.CustId;
                existingCustomer.Company = newCustomer.Company;
                existingCustomer.MobileNo = newCustomer.MobileNo;
                existingCustomer.Email = newCustomer.Email;
                existingCustomer.CustType = newCustomer.CustType;
                existingCustomer.Address = newCustomer.Address;
                existingCustomer.CustSince = newCustomer.CustSince;

                await submissionDbContext.SaveChangesAsync();
                return existingCustomer;
            }

            return null;
        }
    }
}
