
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Database
            .SqlQuery<Product>($"SELECT productid as Productid, productname as Productname FROM Production.Products")
            .ToListAsync();
    }



}

