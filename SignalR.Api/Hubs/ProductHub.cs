using Microsoft.AspNetCore.SignalR;
using SignalR.Api.Models;

namespace SignalR.Api.Hubs
{
    public class ProductHub : Hub<IProductHub>
    {
        public async Task SendProduct(Product product)
        {
            await Clients.All.ReceiveProduct(product);
        }
    }
}
