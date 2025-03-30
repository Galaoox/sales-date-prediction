using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Services;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Domain.Models;
using Xunit;

namespace SalesDatePrediction.Application.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mapperMock = new Mock<IMapper>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetOrdersByCustomerAsync_ShouldReturnPaginatedOrders()
        {
            // Arrange
            int customerId = 1;
            int totalCount = 2;

            var orderSummaries = new List<OrderSummary>
    {
        new OrderSummary
        {
            OrderId = 1,
            RequiredDate = DateTime.UtcNow,
            ShippedDate = DateTime.UtcNow.AddDays(-2),
            ShipName = "Test Ship",
            ShipAddress = "123 Test Street",
            ShipCity = "Test City"
        },
        new OrderSummary
        {
            OrderId = 2,
            RequiredDate = DateTime.UtcNow,
            ShippedDate = DateTime.UtcNow.AddDays(-3),
            ShipName = "Test Ship 2",
            ShipAddress = "456 Another Street",
            ShipCity = "Test City 2"
        }
    };

            var mappedOrders = new List<ClientOrderDto>
    {
        new ClientOrderDto
        {
            Orderid = 1,
            Shipname = "Test Ship",
            Shipcity = "Test City",
            Shipaddress = "123 Test Street",
            Requireddate = orderSummaries[0].RequiredDate,
            Shippeddate = orderSummaries[0].ShippedDate
        },
        new ClientOrderDto
        {
            Orderid = 2,
            Shipname = "Test Ship 2",
            Shipcity = "Test City 2",
            Shipaddress = "456 Another Street",
            Requireddate = orderSummaries[1].RequiredDate,
            Shippeddate = orderSummaries[1].ShippedDate
        }
    };

            _orderRepositoryMock
                .Setup(repo => repo.GetByCustomerIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .ReturnsAsync(orderSummaries.AsEnumerable());

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<ClientOrderDto>>(orderSummaries))
                .Returns(mappedOrders);

            _orderRepositoryMock
                .Setup(repo => repo.CountByCustomerIdAsync(customerId))
                .ReturnsAsync(totalCount);

            // Act
            var result = await _orderService.GetOrdersByCustomerAsync(customerId);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEquivalentTo(mappedOrders);
            result.TotalCount.Should().Be(totalCount);

            _orderRepositoryMock.Verify(repo => repo.GetByCustomerIdAsync(
                customerId,
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()), Times.Once);
            _orderRepositoryMock.Verify(repo => repo.CountByCustomerIdAsync(customerId), Times.Once);
            // Corrected line: Verify mapping with orderSummaries instead of orders
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ClientOrderDto>>(orderSummaries), Times.Once);
        }

        [Fact]
        public async Task GetOrderPredictionsAsync_ShouldReturnPaginatedPredictions()
        {
            // Arrange
            string search = "Company A";
            int totalCount = 1;

            var orderPredictions = new List<CustomerOrderPrediction>
            {
                new CustomerOrderPrediction { CustomerId = 1, CompanyName = "Company A", LastOrderDate = DateTime.UtcNow, NextPredictedOrder = DateTime.UtcNow.AddDays(30) }
            };

            _orderRepositoryMock
                .Setup(repo => repo.GetOrderPredictionsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((orderPredictions, totalCount));

            var mappedPredictions = new List<CustomerOrderPredictionDto>
            {
                new CustomerOrderPredictionDto { CustomerId = 1, CompanyName = "Company A", LastOrderDate = DateTime.UtcNow, NextPredictedOrder = DateTime.UtcNow.AddDays(30) }
            };

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<CustomerOrderPredictionDto>>(orderPredictions))
                .Returns(mappedPredictions);

            // Act
            var result = await _orderService.GetOrderPredictionsAsync(search);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEquivalentTo(mappedPredictions);
            result.TotalCount.Should().Be(totalCount);

            _orderRepositoryMock.Verify(repo => repo.GetOrderPredictionsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<CustomerOrderPredictionDto>>(orderPredictions), Times.Once);
        }

        [Fact]
        public async Task CreateOrderWithDetails_ShouldCallRepository()
        {
            // Arrange
            var orderDto = new OrderDto { Orderid = 1, Custid = 1, Empid = 1, Shipperid = 1, Orderdate = DateTime.UtcNow, Requireddate = DateTime.UtcNow.AddDays(5), Shippeddate = DateTime.UtcNow.AddDays(2), Shipname = "Test", Shipaddress = "Address", Shipcity = "City", Shipcountry = "Country", Freight = 10 };

            var orderDetailsDto = new List<OrderDetailDto>
            {
                new OrderDetailDto { Productid = 1, Unitprice = 10, Quantity = 2, Discount = 0 }
            };

            var mappedOrder = new Order { Orderid = 1, Custid = 1, Empid = 1, Shipperid = 1, Orderdate = DateTime.UtcNow, Requireddate = DateTime.UtcNow.AddDays(5), Shippeddate = DateTime.UtcNow.AddDays(2), Shipname = "Test", Shipaddress = "Address", Shipcity = "City", Shipcountry = "Country" };

            var mappedOrderDetails = new List<OrderDetail>
            {
                new OrderDetail { Orderid = 1, Productid = 1, Unitprice = 10, Quantity = 2, Discount = 0 }
            };

            _mapperMock.Setup(m => m.Map<Order>(orderDto)).Returns(mappedOrder);
            _mapperMock.Setup(m => m.Map<List<OrderDetail>>(orderDetailsDto)).Returns(mappedOrderDetails);

            _orderRepositoryMock.Setup(repo => repo.CreateOrderWithDetails(mappedOrder, mappedOrderDetails)).Returns(Task.CompletedTask);

            // Act
            await _orderService.CreateOrderWithDetails(orderDto, orderDetailsDto);

            // Assert
            _orderRepositoryMock.Verify(repo => repo.CreateOrderWithDetails(mappedOrder, mappedOrderDetails), Times.Once);
        }
    }
}
