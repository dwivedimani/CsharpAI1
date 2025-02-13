using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Domain.Models;

namespace CsharpAI.Infrastructure.AI.Interfaces
{
    public interface IJobMatcher
    {
        Task<List<Job>> FindMatchesAsync(Resume resumeData);
    }
}
