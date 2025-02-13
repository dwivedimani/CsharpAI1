using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Application.DTOs;
using CsharpAI.Application.Interfaces;
using CsharpAI.Domain.Enums;
using CsharpAI.Domain.Models;
using CsharpAI.Persistence.Interfaces;

namespace CsharpAI.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _repository;

        public JobService(IJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobDto>> GetJobsAsync()
        {
            var jobs = await _repository.GetAllJobsAsync();
            return jobs.Select(j => new JobDto
            {
                Title = j.Title,
                Description = j.Description,
                Location = j.Location,
                Salary = j.Salary,
                Type = (int)j.Type
            });
        }

        public async Task<JobDto?> GetJobByIdAsync(int id)
        {
            var job = await _repository.GetJobByIdAsync(id);
            return job == null ? null : new JobDto
            {
                Title = job.Title,
                Description = job.Description,
                Location = job.Location,
                Salary = job.Salary,
                Type = (int)job.Type
            };
        }

        public async Task AddJobAsync(JobDto job)
        {
            await _repository.AddJobAsync(new Job
            {
                Title = job.Title,
                Description = job.Description,
                Location = job.Location,
                Salary = job.Salary,
                Type = (JobType)job.Type
            });
        }

        public async Task UpdateJobAsync(int id, JobDto job)
        {
            var existingJob = await _repository.GetJobByIdAsync(id);
            if (existingJob != null)
            {
                existingJob.Title = job.Title;
                existingJob.Description = job.Description;
                existingJob.Location = job.Location;
                existingJob.Salary = job.Salary;
                existingJob.Type = (JobType)job.Type;

                await _repository.UpdateJobAsync(existingJob);
            }
        }

        public async Task DeleteJobAsync(int id) => await _repository.DeleteJobAsync(id);
    }
}
