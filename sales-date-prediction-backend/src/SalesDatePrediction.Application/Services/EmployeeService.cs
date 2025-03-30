
using AutoMapper;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Application.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var results =  await _repository.GetAllAsync();
        Console.WriteLine("NO falla despues de la query");
        return _mapper.Map<IEnumerable<EmployeeDto>>(results);

    }
}