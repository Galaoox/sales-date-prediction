using SalesDatePrediction.Application.DTOs;

namespace SalesDatePrediction.Application.Interfaces;

public interface IOrderService
{
    Task<PaginatedResultDto<ClientOrderDto>> GetOrdersByCustomerAsync(
        int customerId,
        string sortColumn = "Orderid",
        string sortOrder = "ASC",
        int pageNumber = 1,
        int pageSize = 10
    );

    Task<PaginatedResultDto<CustomerOrderPredictionDto>> GetOrderPredictionsAsync(
        string search,
        string sortColumn = "CompanyName",
        string sortOrder = "ASC",
        int pageNumber = 1,
        int pageSize = 10
    );

    Task CreateOrderWithDetails(OrderDto order, List<OrderDetailDto> orderDetails);
}