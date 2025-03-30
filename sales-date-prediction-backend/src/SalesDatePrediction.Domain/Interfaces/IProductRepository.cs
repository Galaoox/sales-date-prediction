using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
}
