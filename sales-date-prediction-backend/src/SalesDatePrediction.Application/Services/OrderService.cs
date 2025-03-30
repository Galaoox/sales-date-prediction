using AutoMapper;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Application.Services;
public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

    public async Task<PaginatedResultDto<ClientOrderDto>> GetOrdersByCustomerAsync(
            int customerId,
            string sortColumn = "Orderid",
            string sortOrder = "ASC",
            int pageNumber = 1,
            int pageSize = 10)
        {
            sortColumn = string.IsNullOrWhiteSpace(sortColumn) ? "Orderid" : sortColumn;
            sortOrder = (sortOrder?.ToUpper() == "DESC") ? "DESC" : "ASC";
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var orders = await _orderRepository.GetByCustomerIdAsync(
                customerId,
                sortColumn,
                sortOrder,
                pageNumber,
                pageSize
            );

            var mappedOrders = _mapper.Map<IEnumerable<ClientOrderDto>>(orders);

            var totalCount = await _orderRepository.CountByCustomerIdAsync(customerId);

            return new PaginatedResultDto<ClientOrderDto>
            {
                Items = mappedOrders,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

    public async Task<PaginatedResultDto<CustomerOrderPredictionDto>> GetOrderPredictionsAsync(string search, string sortColumn = "CompanyName", string sortOrder = "ASC", int pageNumber = 1, int pageSize = 10)
    {
        sortColumn = string.IsNullOrWhiteSpace(sortColumn) ? "CompanyName" : sortColumn;
        sortOrder = (sortOrder?.ToUpper() == "DESC") ? "DESC" : "ASC";
        pageNumber = Math.Max(1, pageNumber);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var results = await _orderRepository.GetOrderPredictionsAsync(search, sortColumn, sortOrder, pageNumber, pageSize);

        return new PaginatedResultDto<CustomerOrderPredictionDto>
        {
            Items = _mapper.Map<IEnumerable<CustomerOrderPredictionDto>>(results.data),
            TotalCount = results.totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task CreateOrderWithDetails(OrderDto order, List<OrderDetailDto> orderDetails)
    {
        await _orderRepository.CreateOrderWithDetails(
            _mapper.Map<Order>(order),
            _mapper.Map<List<OrderDetail>>(orderDetails)
        );
    }
}
