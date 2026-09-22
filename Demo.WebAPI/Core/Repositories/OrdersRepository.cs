using Demo.WebAPI.Models.Domain;

namespace Demo.WebAPI.Core.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly Dictionary<string, Order> _orders = Seed.SeedOrders().ToDictionary(x => x.OrderId);

    public Task<string> PlaceOrderAsync(string customerId, string productId)
    {
        var orderId = Guid.NewGuid().ToString();
        _orders.Add(orderId, new Order
        {
            CustomerId = customerId,
            ProductId = productId,
            OrderId = orderId
        });
        return Task.FromResult(orderId);
    }

    public async Task CancelOrderAsync(string orderId)
    {
        var order = await GetOrderAsync(orderId);
        if (order is not null)
        {
            order.IsCancelled = true;
        }
    }

    public Task<Order?> GetOrderAsync(string orderId) => Task.FromResult(_orders.GetValueOrDefault(orderId));

    public Task<List<Order>> GetOrdersAsync() => Task.FromResult(_orders.Values.ToList());

}
