using LogGenerator.Models.Domain;

namespace LogGenerator.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<NewEmployee>> GetAllASync();

        Task<NewEmployee?> GetAsync(int id);

        Task<NewEmployee> AddAsync(NewEmployee newEmployee);

        Task<NewEmployee?> UpdateAsync(NewEmployee newEmployee);

        Task<NewEmployee?> DeleteAsync(int id);

    }
}
