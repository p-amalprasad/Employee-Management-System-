using LogGenerator.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LogGenerator.Data
{
    public class SubmissionDbContext : DbContext
    {
        public SubmissionDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Submission> Logs { get; set; }
        public DbSet<NewEmployee> Employees { get; set; }
        public DbSet<NewCustomer> Customers { get; set; }
    }
}
