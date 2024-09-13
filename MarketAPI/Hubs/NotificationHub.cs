using Microsoft.AspNetCore.SignalR;

namespace MarketAPI.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
            Console.WriteLine("Sent message");
        }
    }
}
