using Microsoft.AspNetCore.SignalR;

namespace SignalR.Web.Hubs;

// 23. SignalR Server Hub (Asp.Net Core Mvc)
public class MyHub : Hub
{
    public async Task SendName(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
