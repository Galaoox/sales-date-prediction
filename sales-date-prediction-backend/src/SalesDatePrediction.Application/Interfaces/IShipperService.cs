
using SalesDatePrediction.Application.DTOs;

namespace SalesDatePrediction.Application.Interfaces;

public interface IShipperService
{
    Task<IEnumerable<ShipperDto>> GetAllAsync();
}