using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Domain.Models;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.Data;

public class CustomerRepository 
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }


}
