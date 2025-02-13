using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Application.Interfaces;
using CsharpAI.Infrastructure.AI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace CsharpAI.Application.Services
{
    public class AIOrchestrationService : IAIOrchestrationService
    {
        private readonly IResumeParser _resumeParser;
        private readonly IJobMatcher _jobMatcher;
        //private readonly INotificationService _notificationService;
        //private readonly IHubContext<SignalHub> _hubContext;

        public AIOrchestrationService(
            IResumeParser resumeParser,
            IJobMatcher jobMatcher
            //INotificationService notificationService,
            //IHubContext<SignalHub> hubContext
            )
        {
            _resumeParser = resumeParser;
            _jobMatcher = jobMatcher;
            
        }

        public async Task<string> ProcessResumeAsync(int userId, IFormFile resumeFile)
        {
            // Step 1: Parse Resume
            var extractedData = await _resumeParser.ParseAsync(resumeFile);

            // Step 2: Find Matching Jobs
            var jobMatches = await _jobMatcher.FindMatchesAsync(extractedData);

            //// Step 3: Store and Notify User
            //await _notificationService.SendNotificationAsync(userId, "Resume processed successfully!");

            //// Real-time update via SignalR
            //await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveJobMatches", jobMatches);

            return "Resume processing completed!";
        }
    }
}

