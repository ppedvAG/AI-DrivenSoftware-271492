using Demo.WebAPI.Models.Domain;

namespace Demo.WebAPI.Core.Repositories;

public class Seed
{
    public static IEnumerable<Order> SeedOrders()
    {
        yield return new Order
        {
            CustomerId = "ACME-0815",
            OrderId = "1",
            ProductId = "Staubsauger#45",
            DeliveryDate = DateTime.Now
        };
    }
}
