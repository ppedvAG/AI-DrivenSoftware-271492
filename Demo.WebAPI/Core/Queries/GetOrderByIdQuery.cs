using Demo.WebAPI.Models.Dto;
using MediatR;

namespace Demo.WebAPI.Core.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public GetOrderByIdQuery(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; }
    }
}
