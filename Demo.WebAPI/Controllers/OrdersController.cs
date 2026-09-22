using Demo.WebAPI.Core.Commands;
using Demo.WebAPI.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly IMediator mediator;

        public OrdersController(ILogger<OrdersController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var query = new GetAllOrdersQuery();
            var result = await mediator.Send(query, token);
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(string orderId, CancellationToken token)
        {
            var query = new GetOrderByIdQuery(orderId);
            var result = await mediator.Send(query, token);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("place")]
        public async Task<IActionResult> Place([FromBody] PlaceOrderCommand command, CancellationToken token)
        {
            var result = await mediator.Send(command, token);
            return CreatedAtAction(nameof(Get), new { orderId = result }, result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] CancelOrderCommand command, CancellationToken token)
        {
            await mediator.Send(command, token);
            return Ok();
        }
    }
}
