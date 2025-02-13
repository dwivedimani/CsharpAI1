using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Domain.Models;
using CsharpAI.Infrastructure.AI.Interfaces;
using CsharpAI.Persistence.Interfaces;

namespace CsharpAI.Infrastructure.AI
{
    public class JobMatcher : IJobMatcher
    {
        private readonly IJobRepository _jobRepository;

        public JobMatcher(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<Job>> FindMatchesAsync(Resume resumeData)
        {
            var jobs = await _jobRepository.GetAllJobsAsync();
            return jobs.Where(job => job.RequiredSkills.Any(skill => resumeData.Skills.Contains(skill))).ToList();
        }
    }
}
