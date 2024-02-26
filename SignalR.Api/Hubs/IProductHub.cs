using SignalR.Api.Models;

namespace SignalR.Api.Hubs
{
    public interface IProductHub
    {
        Task ReceiveProduct(Product product);
    }
}
