using MediatR;

namespace Demo.WebAPI.Core.Commands
{
    public class PlaceOrderCommand : IRequest<string>
    {
        public string CustomerId { get; set; }

        public string ProductId { get; set; }
    }
}
