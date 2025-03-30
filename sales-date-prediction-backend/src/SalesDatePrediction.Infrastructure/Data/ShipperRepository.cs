
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Domain.Models;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.Data;

public class ShipperRepository : IShipperRepository
{
    private readonly ApplicationDbContext _context;

    public ShipperRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shipper>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<Shipper>($"SELECT shipperid as Shipperid, companyname as Companyname FROM Sales.Shippers")
            .ToListAsync();
    }

}