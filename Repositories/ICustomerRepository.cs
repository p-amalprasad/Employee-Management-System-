using LogGenerator.Models.Domain;

namespace LogGenerator.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<NewCustomer>> GetAllASync();

        Task<NewCustomer?> GetAsync(int id);

        Task<NewCustomer> AddAsync(NewCustomer newCustomer);

        Task<NewCustomer?> UpdateAsync(NewCustomer newCustomer);

        Task<NewCustomer?> DeleteAsync(int id);

    }
}
