using SalesDatePrediction.Application.DTOs;
namespace SalesDatePrediction.Application.Interfaces;

public interface IProductService
{
   Task<IEnumerable<ProductDto>> GetAllAsync();
}