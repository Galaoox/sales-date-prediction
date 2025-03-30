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

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Productid = 1, Productname = "Product A" },
            new Product { Productid = 2, Productname = "Product B" }
        };

        var productDtos = new List<ProductDto>
        {
            new ProductDto { Productid = 1, Productname = "Product A" },
            new ProductDto { Productid = 2, Productname = "Product B" }
        };

        _productRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(products);

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<ProductDto>>(products))
            .Returns(productDtos);

        // Act
        var result = await _productService.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(productDtos);

        _productRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ProductDto>>(products), Times.Once);
    }
}
