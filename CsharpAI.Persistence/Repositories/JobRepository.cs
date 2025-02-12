using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Domain.Models;
using CsharpAI.Persistence.Interfaces;

namespace CsharpAI.Persistence.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly DapperDBContext _context;

        public JobRepository(DapperDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetJobsAsync() => null;

        public async Task<Job?> GetJobByIdAsync(int id) => null;

        public async Task AddJobAsync(Job job)
        {
           
        }

        public async Task UpdateJobAsync(Job job)
        {
           
        }

        public async Task DeleteJobAsync(int id)
        {
            var job = await GetJobByIdAsync(id);
            if (job != null)
            {
               
            }
        }
    }
}
