using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Persistence.Entities;

namespace CsharpAI.ResumeProcessor.Interfaces
{
    public interface IResumeProcessor
    {
        Task<ResumeResult> ProcessResume(string filePath);
    }
}
