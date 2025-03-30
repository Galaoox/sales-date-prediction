
using SalesDatePrediction.Application.DTOs;

namespace SalesDatePrediction.Application.Interfaces;
public interface IEmployeeService
{
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
}


