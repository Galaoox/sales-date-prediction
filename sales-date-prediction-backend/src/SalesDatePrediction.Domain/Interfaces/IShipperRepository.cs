using SalesDatePrediction.Domain.Models;

namespace SalesDatePrediction.Domain.Interfaces;

public interface IShipperRepository
{
    Task<IEnumerable<Shipper>> GetAllAsync();
}
