using Demo.WebAPI.Core.Queries;
using Demo.WebAPI.Core.Repositories;
using Demo.WebAPI.Mappers;
using Demo.WebAPI.Models.Dto;
using MediatR;

namespace Demo.WebAPI.Core.Handlers
{
    public class OrderQueryHandler :
        IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>,
        IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrdersRepository _repository;

        public OrderQueryHandler(IOrdersRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetOrdersAsync();
            return orders.Select(x => x.ToDto());
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetOrderAsync(request.OrderId);
            return order?.ToDto();
        }
    }
}
