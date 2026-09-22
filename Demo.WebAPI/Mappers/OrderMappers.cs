using Demo.WebAPI.Models.Domain;
using Demo.WebAPI.Models.Dto;

namespace Demo.WebAPI.Mappers
{
    public static class OrderMappers
    {
        public static OrderDto? ToDto(this Order order)
        {
            return order is null
                ? null
                : new OrderDto
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    ProductId = order.ProductId,
                    DeliveryDate = order.DeliveryDate,
                    IsCancelled = order.IsCancelled,
                    // Business Logik, weil hier eine Transformation des Datums statt findet
                    IsDelivered = !order.DeliveryDate.Equals(default)
                };
        }
    }
}
