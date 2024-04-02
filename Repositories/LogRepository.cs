using LogGenerator.Data;
using LogGenerator.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LogGenerator.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly SubmissionDbContext submissionDbContext;

        public LogRepository(SubmissionDbContext submissionDbContext)
        {
            this.submissionDbContext = submissionDbContext;
        }

        public async Task<Submission> AddAsync(Submission submission)
        {
            await submissionDbContext.AddAsync(submission);
            await submissionDbContext.SaveChangesAsync();      
            return submission;
        }

        public async Task<IEnumerable<Submission>> GetAllASync()
        {
            return await submissionDbContext.Logs.ToListAsync();
        }

    }
}
