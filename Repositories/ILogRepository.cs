using LogGenerator.Models.Domain;

namespace LogGenerator.Repositories
{
    public interface ILogRepository
    {
        Task<Submission> AddAsync(Submission submission);

        Task<IEnumerable<Submission>> GetAllASync();
  
    }
}
