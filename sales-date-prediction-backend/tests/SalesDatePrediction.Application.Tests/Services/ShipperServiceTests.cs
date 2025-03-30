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

namespace SalesDatePrediction.Application.Tests.Services;

public class ShipperServiceTests
{
    private readonly Mock<IShipperRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ShipperService _shipperService;

    public ShipperServiceTests()
    {
        _repositoryMock = new Mock<IShipperRepository>();
        _mapperMock = new Mock<IMapper>();
        _shipperService = new ShipperService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedShippers()
    {
        // Arrange
        var shippers = new List<Shipper>
        {
            new Shipper { Shipperid = 1, CompanyName = "Company A" },
            new Shipper { Shipperid = 2, CompanyName = "Company B" }
        };

        var shipperDtos = new List<ShipperDto>
        {
            new ShipperDto { Shipperid = 1, CompanyName = "Company A" },
            new ShipperDto { Shipperid = 2, CompanyName = "Company B" }
        };

        _repositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(shippers);

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<ShipperDto>>(shippers))
            .Returns(shipperDtos);

        // Act
        var result = await _shipperService.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(shipperDtos);

        _repositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ShipperDto>>(shippers), Times.Once);
    }
}
