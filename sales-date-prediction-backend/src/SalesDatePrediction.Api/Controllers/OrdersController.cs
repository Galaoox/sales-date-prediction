using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Application.DTOs;

namespace SalesDatePrediction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("{customerId}")]
        public async Task<ActionResult<PaginatedResultDto<ClientOrderDto>>> GetOrders(
            int customerId,
            [FromBody] OrderParametersDto parameters)
        {
            parameters ??= new OrderParametersDto();

            var orders = await _orderService.GetOrdersByCustomerAsync(
                customerId,
                parameters.GetSortColumnAsString(),
                parameters.GetSortOrderAsString(),
                parameters.PageNumber,
                parameters.PageSize
            );

            return Ok(orders);
        }

        [HttpPost("predictions")]
        public async Task<ActionResult<PaginatedResultDto<CustomerOrderPredictionDto>>> GetPredictions(
            [FromBody] OrderPredictionParametersDto parameters)
        {
            parameters ??= new OrderPredictionParametersDto();
            var predictions = await _orderService.GetOrderPredictionsAsync(
                parameters.Search,
                parameters.GetSortColumnAsString(),
                parameters.GetSortOrderAsString(),
                parameters.PageNumber,
                parameters.PageSize
            );
            return Ok(predictions);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] CreateOrderParametersDto parameters)
        {
            await _orderService.CreateOrderWithDetails(parameters.Order, parameters.OrderDetailDtos);
            return Ok();
        }
    }
}