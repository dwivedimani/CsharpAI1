using Microsoft.AspNetCore.SignalR;

namespace CsharpAI.SignalR
{
    public class AiHub : Hub
    {
        public async Task SendJobRecommendation(string resumeText)
        {
            await Clients.Caller.SendAsync("ReceiveJobRecommendation", $"Processing AI for: {resumeText}");
        }
    }
}
