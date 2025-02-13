using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace CsharpAI.Infrastructure.AI.Interfaces
{
    public interface IResumeParser
    {
        Task<Resume> ParseAsync(IFormFile resumeFile);
    }
}
