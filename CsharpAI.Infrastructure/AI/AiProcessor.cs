using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI;

namespace CsharpAI.Infrastructure.AI
{
    public class AiProcessor
    {
        private readonly OpenAIClient _client;

        public AiProcessor(string apiKey)
        {
            _client = new OpenAIClient(apiKey);
        }

        public async Task<string> GetJobRecommendationsAsync(string resumeText)
        {
            //var response = await _client.Completions.CreateCompletionAsync(new()
            //{
            //    Model = "gpt-4",
            //    Prompt = $"Based on this resume, suggest 5 job titles: {resumeText}",
            //    MaxTokens = 100
            //});

            //return response.Completions[0].Text;
            return "";
        }
    }
}
