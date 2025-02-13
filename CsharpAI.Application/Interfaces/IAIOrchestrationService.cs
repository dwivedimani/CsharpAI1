using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CsharpAI.Application.Interfaces
{
    public interface IAIOrchestrationService
    {
        Task<string> ProcessResumeAsync(int userId, IFormFile resumeFile);
    }
}
