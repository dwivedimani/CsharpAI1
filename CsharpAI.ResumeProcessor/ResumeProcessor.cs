using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpAI.ResumeProcessor
{
    public class ResumeProcessor : IResumeProcessor
    {
        private readonly ITextExtractor _textExtractor;
        private readonly IAIAnalyzer _aiAnalyzer;
        private readonly AppDbContext _dbContext;
        private readonly IHubContext<ResumeHub> _hubContext;

        public ResumeProcessor(ITextExtractor textExtractor, IAIAnalyzer aiAnalyzer, AppDbContext dbContext, IHubContext<ResumeHub> hubContext)
        {
            _textExtractor = textExtractor;
            _aiAnalyzer = aiAnalyzer;
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        public async Task<ResumeResult> ProcessResume(string filePath)
        {
            string extractedText = await _textExtractor.ExtractText(filePath);
            var analysisResult = await _aiAnalyzer.AnalyzeResume(extractedText);

            var resume = new Resume
            {
                Name = analysisResult.Name,
                Skills = string.Join(",", analysisResult.Skills),
                Experience = analysisResult.Experience
            };

            _dbContext.Resumes.Add(resume);
            await _dbContext.SaveChangesAsync();

            // Send real-time update
            await _hubContext.Clients.All.SendAsync("ReceiveResumeUpdate", resume);

            return analysisResult;
        }
    }

}
