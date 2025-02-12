using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Application.DTOs;

namespace CsharpAI.Application.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<JobDto>> GetJobsAsync();
        Task<JobDto?> GetJobByIdAsync(int id);
        Task AddJobAsync(JobDto job);
        Task UpdateJobAsync(int id, JobDto job);
        Task DeleteJobAsync(int id);
    }
}
