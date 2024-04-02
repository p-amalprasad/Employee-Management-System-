using LogGenerator.Data;
using LogGenerator.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LogGenerator.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SubmissionDbContext submissionDbContext;

        public EmployeeRepository(SubmissionDbContext submissionDbContext)
        {
            this.submissionDbContext = submissionDbContext;
        }

        public async Task<NewEmployee> AddAsync(NewEmployee newEmployee)
        {
            await submissionDbContext.AddAsync(newEmployee);
            await submissionDbContext.SaveChangesAsync();
            return newEmployee;
        }

        public async Task<NewEmployee?> DeleteAsync(int id)
        {
            var existingEmployee = await submissionDbContext.Employees.FindAsync(id);

            if (existingEmployee != null)
            {
                submissionDbContext.Employees.Remove(existingEmployee);
                await submissionDbContext.SaveChangesAsync();
                return existingEmployee;
            }

            return null;
        }

        public async Task<IEnumerable<NewEmployee>> GetAllASync()
        {
            return await submissionDbContext.Employees.ToListAsync();
        }

        public async Task<NewEmployee?> GetAsync(int id)
        {
            return await submissionDbContext.Employees.FirstOrDefaultAsync(x => x.EmpId == id);
        }

        public async Task<NewEmployee?> UpdateAsync(NewEmployee newEmployee)
        {
            var existingEmployee = await submissionDbContext.Employees
                .FirstOrDefaultAsync(x => x.EmpId == newEmployee.EmpId);

            if (existingEmployee != null)
            {
                existingEmployee.EmpId = newEmployee.EmpId;
                existingEmployee.Name = newEmployee.Name;
                existingEmployee.MobileNo = newEmployee.MobileNo;
                existingEmployee.About = newEmployee.About;
                existingEmployee.JobDesc = newEmployee.JobDesc;
                existingEmployee.Address = newEmployee.Address;
                existingEmployee.Role = newEmployee.Role;
                existingEmployee.DoB = newEmployee.DoB;
                    
                await submissionDbContext.SaveChangesAsync();
                return existingEmployee;
            }

            return null;
        }
    }
}
