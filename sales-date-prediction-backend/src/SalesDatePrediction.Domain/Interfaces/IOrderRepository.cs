using SalesDatePrediction.Domain.Models;


namespace SalesDatePrediction.Domain.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<OrderSummary>> GetByCustomerIdAsync(
        int customerId,
        string sortColumn = "OrderId",
        string sortOrder = "ASC",
        int pageNumber = 1,
        int pageSize = 10
    );
    Task<int> CountByCustomerIdAsync(int customerId);

    Task<(IEnumerable<CustomerOrderPrediction> data, int totalCount)> GetOrderPredictionsAsync(
    string search,
    string sortColumn,
    string sortOrder,
    int pageNumber,
    int pageSize);
    Task CreateOrderWithDetails(Order order, List<OrderDetail> orderDetails);
}