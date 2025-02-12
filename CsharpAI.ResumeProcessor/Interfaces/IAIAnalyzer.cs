﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Domain.Models;
using CsharpAI.Persistence.Entities;

namespace CsharpAI.ResumeProcessor.Interfaces
{
    public interface IAIAnalyzer
    {
        Task<Resume> AnalyzeResume(string extractedText);
    }
}
