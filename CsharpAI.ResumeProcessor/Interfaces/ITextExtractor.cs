using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpAI.ResumeProcessor.Interfaces
{
    public interface ITextExtractor
    {
        Task<string> ExtractText(string filePath);
    }
}
