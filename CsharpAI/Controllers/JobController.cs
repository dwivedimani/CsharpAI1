using CsharpAI.Application.DTOs;
using CsharpAI.Application.Interfaces;
using CsharpAI.Infrastructure.Caching;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAI.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;

        private readonly IJobService _jobService;
        private readonly RedisCacheService _cacheService;

        public JobController(IJobService jobService, ILogger<JobController> logger, RedisCacheService cacheService)
        {
            _jobService = jobService;
            _logger = logger;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetJobs()
        {
            string cacheKey = "job_listings";

            // Try to fetch from Redis cache first
            var cachedJobs = await _cacheService.GetAsync<List<string>>(cacheKey);
            if (cachedJobs is not null)
            {
                _logger.LogInformation("Retrieved jobs from Redis cache.");
                return Ok(cachedJobs);
            }

            // Simulate fetching from a database (slow operation)
            var jobs = await _jobService.GetJobsAsync();
            _logger.LogInformation("Fetched jobs from database.");

            // 🔹 Store in Redis with an expiration of 10 minutes
            await _cacheService.SetAsync(cacheKey, jobs, TimeSpan.FromMinutes(10));
            return Ok(jobs);           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobDto>> GetJob(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null) return NotFound();
            return Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult> CreateJob(JobDto jobDto)
        {
            try
            {
                await _jobService.AddJobAsync(jobDto);
                return CreatedAtAction(nameof(GetJob), new { id = jobDto.Title }, jobDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating job listing");
                // Manually send exception to Sentry
                SentrySdk.CaptureException(ex);
                return BadRequest();
            }
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateJob(int id, JobDto jobDto)
        {
            await _jobService.UpdateJobAsync(id, jobDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJob(int id)
        {
            await _jobService.DeleteJobAsync(id);
            return NoContent();
        }
    }
}
