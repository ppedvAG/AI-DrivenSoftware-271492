using Demo.WebAPI.Models.Dto;
using MediatR;

namespace Demo.WebAPI.Core.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>> { }
}
