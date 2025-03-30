using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
}
