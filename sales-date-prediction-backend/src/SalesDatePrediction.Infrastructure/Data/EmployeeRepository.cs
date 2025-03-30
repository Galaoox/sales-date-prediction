using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Infrastructure.Data;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<Employee>($"SELECT empid as Empid,  CONCAT(firstname,' ', lastname) AS Fullname FROM StoreSample.HR.Employees")
            .ToListAsync();
    }
}
