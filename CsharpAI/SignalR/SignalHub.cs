using CsharpAI.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace CsharpAI.SignalR
{
    public class SignalHub : Hub
    {
        public async Task SendJobMatches(int userId, List<Job> jobMatches)
        {
            await Clients.User(userId.ToString()).SendAsync("ReceiveJobMatches", jobMatches);
        }
    }
}
