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

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _repositoryMock = new Mock<IEmployeeRepository>();
        _mapperMock = new Mock<IMapper>();
        _employeeService = new EmployeeService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedEmployees()
    {
        // Arrange
        var employees = new List<Employee>
        {
            new Employee { Empid = 1, Fullname = "John Doe" },
            new Employee { Empid = 2, Fullname = "Jane Doe" }
        };

        var employeeDtos = new List<EmployeeDto>
        {
            new EmployeeDto { Empid = 1, Fullname = "John Doe" },
            new EmployeeDto { Empid = 2, Fullname = "Jane Doe" }
        };

        _repositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(employees);

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<EmployeeDto>>(employees))
            .Returns(employeeDtos);

        // Act
        var result = await _employeeService.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(employeeDtos);

        _repositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<IEnumerable<EmployeeDto>>(employees), Times.Once);
    }
}
