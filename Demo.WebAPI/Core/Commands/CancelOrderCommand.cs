using MediatR;

namespace Demo.WebAPI.Core.Commands
{
    public class CancelOrderCommand : IRequest
    {
        public string OrderId { get; set; }
    }
}
