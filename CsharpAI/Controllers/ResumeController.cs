using CsharpAI.Application.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAI.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IAIOrchestrationService _aiOrchestrationService;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public ResumeController(IAIOrchestrationService aiOrchestrationService, IBackgroundJobClient backgroundJobClient)
        {
            _aiOrchestrationService = aiOrchestrationService;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost]
        public IActionResult UploadResume(int userId, IFormFile resumeFile)
        {
            _backgroundJobClient.Enqueue(() => _aiOrchestrationService.ProcessResumeAsync(userId, resumeFile));
            return Ok("Resume processing started!");
        }
    }
}
